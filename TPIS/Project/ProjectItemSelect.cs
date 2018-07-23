using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TPIS.Model;
using TPIS.Model.Common;

namespace TPIS.Project
{
    public partial class ProjectItem : INotifyPropertyChanged, ISerializable
    {
        public ObservableCollection<ObjectBase> selectedObjects;
        public ObservableCollection<ObjectBase> SelectedObjects
        {
            get { return selectedObjects; }
            set
            {
                selectedObjects = value;
                OnPropertyChanged("SelectedObjects");
            }
        }

        #region 获取已选中对象
        public void ReSelect(TPISComponent component)
        {
            ObservableCollection<TPISLine> lines = new ObservableCollection<TPISLine>();
            component.isSelected = true;
            foreach (Port port in component.Ports)
            {
                if (port.link != null && !port.link.IsSelected)
                    lines.Add(port.link);
            }
            foreach (ObjectBase obj in Objects)
            {
                if (obj.isSelected && obj is TPISComponent)
                    if ((TPISComponent)obj != component)
                        foreach (Port port in ((TPISComponent)obj).Ports)
                            if (port.link != null && !port.link.IsSelected)
                                foreach (TPISLine line in lines)
                                    if (line.No == port.link.No)
                                        port.link.IsSelected = true;
            }
        }

        public void GetSelectedObjects()
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            SelectedObjects = new ObservableCollection<ObjectBase>();
            bool flag = mainwin.GetCurrentProject().IsViewWindowsOpen;
            if (SelectedObjects != null)
                SelectedObjects.Clear();
            foreach (ObjectBase obj in Objects)
            {
                if (obj.isSelected)
                    SelectedObjects.Add(obj);
            }
        }
        #endregion

        #region 选择操作
        public void SelectAll()
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            List<TPISComponent> components = new List<TPISComponent>();
            List<TPISText> texts = new List<TPISText>();
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    components.Add((TPISComponent)obj);
                }
                if (obj is TPISText)
                {
                    texts.Add((TPISText)obj);
                }
            }
            Select(components, texts);
            //GetSelectedObjects();
            //bool flag = mainwin.GetCurrentProject().IsViewWindowsOpen;
            //if (!flag)
            //    mainwin.GetCurrentProject().GetSelectedObjects();
            UpdateText();
        }

        internal void Select(List<TPISComponent> components, List<TPISText> texts)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (components.Count > 0)
            {
                if (mainwin.PropertyContent.ItemsSource != components[0].PropertyGroups)
                {
                    BindingPropertyWindow(components[0]);
                }
            }
            else
            {
                BindingPropertyWindow(null);
            }

            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                    ((TPISComponent)obj).IsSelected = false;
                if (obj is TPISLine)
                    ((TPISLine)obj).IsSelected = false;
                if (obj is TPISText)
                    ((TPISText)obj).IsSelected = false;
                obj.isSelected = false;
            }

            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    if (components.Contains(obj as TPISComponent))
                    {
                        if (!((TPISComponent)obj).IsSelected)
                        {
                            ((TPISComponent)obj).IsSelected = true;
                            foreach (Port p in ((TPISComponent)obj).Ports)
                            {
                                if (p.CrossNo <= 0)
                                {
                                    foreach (ObjectBase objt in Objects)
                                    {
                                        if (objt.No == p.CrossNo && objt is ResultCross)
                                            objt.isSelected = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (obj is TPISText)
                {
                    if (texts.Contains(obj as TPISText))
                    {
                        if (!((TPISText)obj).IsSelected)
                        {
                            ((TPISText)obj).IsSelected = true;
                        }
                    }
                }
                else if (obj is TPISLine)
                {
                    Boolean checkin = false, checkout = false;
                    foreach (TPISComponent cp in components)
                    {
                        if (cp.Ports.Contains(((TPISLine)obj).InPort))
                        {
                            checkin = true;
                        }
                        if (cp.Ports.Contains(((TPISLine)obj).OutPort))
                        {
                            checkout = true;
                        }
                        if (checkin && checkout)
                        {
                            break;
                        }
                    }
                    if (checkin && checkout)
                    {
                        ((TPISLine)obj).IsSelected = true;
                    }
                    else
                    {
                        ((TPISLine)obj).IsSelected = false;
                    }
                }
            }
            //GetSelectedObjects();
        }

        private void BindingPropertyWindow(TPISComponent component)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (component == null)
            {
                mainwin.PropertyMode.DataContext = null;

                Binding PropertyBinding = new Binding();
                PropertyBinding.Source = this;
                PropertyBinding.Path = new PropertyPath("PropertyGroup");
                mainwin.PropertyContent.SetBinding(ListBox.ItemsSourceProperty, PropertyBinding);

                Binding ResultBinding = new Binding();
                ResultBinding.Source = this;
                ResultBinding.Path = new PropertyPath("ResultGroup");
                mainwin.ResultWindow.SetBinding(ListBox.ItemsSourceProperty, ResultBinding);

                mainwin.PortResults.ItemsSource = null;
                mainwin.PortResults.Items.Refresh();
                return;
            }
            else { 
                BindingOperations.ClearBinding(mainwin.PropertyMode, ComboBox.SelectedIndexProperty);

                mainwin.PropertyMode.ItemsSource = component.Mode;
                mainwin.PropertyMode.Items.Refresh();

                //mainwin.PropertyContent.ItemsSource = component.PropertyGroups;
                //mainwin.PropertyContent.Items.Refresh();

                Binding PropertyBinding = new Binding();
                PropertyBinding.Source = component;
                PropertyBinding.Path = new PropertyPath("PropertyGroups");
                mainwin.PropertyContent.SetBinding(ListBox.ItemsSourceProperty, PropertyBinding);

                mainwin.PropertyMode.SelectedIndex = component.selectedMode;
                Binding modeBinding = new Binding();
                modeBinding.Source = component;
                modeBinding.Path = new PropertyPath("SelectedMode");
                mainwin.PropertyMode.SetBinding(ComboBox.SelectedIndexProperty, modeBinding);


                Binding ResultBinding = new Binding();
                ResultBinding.Source = component;
                ResultBinding.Path = new PropertyPath("ResultGroups");
                mainwin.ResultWindow.SetBinding(ListBox.ItemsSourceProperty, ResultBinding);

                Binding PortBinding = new Binding();
                PortBinding.Source = component;
                PortBinding.Path = new PropertyPath("Ports");
                mainwin.PortResults.SetBinding(ItemsControl.ItemsSourceProperty, PortBinding);

                //mainwin.ResultWindow.ItemsSource = component.ResultGroups;
                //mainwin.ResultWindow.Items.Refresh();
                //mainwin.PortResults.ItemsSource = component.Ports;
                //mainwin.PortResults.Items.Refresh();
                UpdateText();
            }
        }

        internal void Select(ObjectBase objectBase)
        {
            foreach (ObjectBase obj in Objects)
            {
                if (obj is ResultCross)
                    obj.isSelected = false;
            }

            foreach (ObjectBase obj in Objects)
            {
                if (obj is ResultCross)
                {
                    if (objectBase == obj)
                    {
                        ((ResultCross)obj).isSelected = true;
                    }
                }
                else if (obj is TPISText)
                {
                    if (objectBase == obj)
                    {
                        ((TPISText)obj).IsSelected = true;
                    }
                    else
                    {
                        ((TPISText)obj).IsSelected = false;
                    }
                }
                else if (obj is TPISComponent)
                {
                    if (objectBase == obj)
                    {
                        ((TPISComponent)obj).IsSelected = true;
                        foreach (Port p in ((TPISComponent)obj).Ports)
                        {
                            if (p.CrossNo <= 0)
                            {
                                foreach (ObjectBase objt in Objects)
                                {
                                    if (objt.No == p.CrossNo && objt is ResultCross)
                                        objt.isSelected = true;
                                }
                            }
                        }
                        //设置属性框显示该属性
                        BindingPropertyWindow(obj as TPISComponent);
                    }
                    else
                    {
                        ((TPISComponent)obj).IsSelected = false;
                        foreach (Port p in ((TPISComponent)obj).Ports)
                        {
                            if (p.CrossNo >= 0)
                            {
                                foreach (ObjectBase objt in Objects)
                                {
                                    if (objt.No == p.CrossNo && objt is ResultCross)
                                        objt.isSelected = false;
                                }
                            }
                        }
                    }
                }
                else if (obj is TPISLine)
                {
                    //是连线，同上
                    if (objectBase == obj)
                    {
                        ((TPISLine)obj).IsSelected = true;
                    }
                    else
                    {
                        ((TPISLine)obj).IsSelected = false;
                    }
                }
            }
            //GetSelectedObjects();
            UpdateText();
        }

        internal void Select()
        {
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                    ((TPISComponent)obj).IsSelected = false;
                if (obj is TPISLine)
                    ((TPISLine)obj).IsSelected = false;
                if (obj is TPISText)
                    ((TPISText)obj).IsSelected = false;
                obj.isSelected = false;
            }
            BindingPropertyWindow(null);
        }
        #endregion
    }
}
