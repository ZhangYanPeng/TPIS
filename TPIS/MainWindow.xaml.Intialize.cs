using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TPIS.Model;
using TPIS.Model.Common;
using TPIS.Project;
using TPIS.Views.Modules;
using TPISNet;

namespace TPIS
{
    public partial class MainWindow : Window
    {
        static public int GRID_WIDTH = 4;

        /// <summary>
        /// 加载所有元件类型
        /// </summary>
        private void loadComponentType()
        {
            TypeList = new List<BaseType>();
            Interface inface = new Interface();

            int id = 0;
            foreach (string key in Interface.EleTypeGroup.Keys)
            {
                BaseType bt = new BaseType();
                bt.Name = key;
                TypeList.Add(bt);

                List<EleType> eleTypes = Interface.EleTypeGroup[key];
                foreach(EleType eleType in eleTypes)
                {
                    id++;
                    Element element = CommonTypeService.LoadElement(eleType);
                    ComponentType ct = new ComponentType { Id = id, PicPath = "pack://SiteofOrigin:,,," + Interface.GetPNGstr(eleType), Name = element.DProperty["Name"].data_string, Type = eleType, IsChecked = false };
                    ct.Width = element.Nwidth[0]*10;
                    ct.Height = element.Nheight[0]*10;
                    bt.ComponentTypeList.Add(ct);
                }
            }
            this.ElementList.ItemsSource = TypeList;
            this.ElementList.Items.Refresh();
        }

        //建立工作空间文件夹
        #region
        private void InitWorkSpace()
        {
            string directoryPath = @".\WorkSpace";
            if (!Directory.Exists(directoryPath))//如果路径不存在
            {
                Directory.CreateDirectory(directoryPath);//创建一个路径的文件夹
            }
        }

        internal ProjectItem GetRelateProject(Port port)
        {
            foreach(ProjectItem pi in ProjectList.projects)
            {
                foreach(ObjectBase obj in pi.Objects)
                {
                    if(obj is TPISComponent)
                    {
                        if( ((TPISComponent)obj).Ports.Contains(port) )
                            return pi;
                    }
                }
            }
            return null;
        }
        #endregion

        #region 初始化工具栏数值
        //字号
        ObservableCollection<double> FontSize = new ObservableCollection<double> {5, 5.5, 6.5, 7.5, 8, 9, 10, 10.5,
            11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

        public ObservableCollection<double> LineTicknessType = new ObservableCollection<double> { 1, 2, 3, 4, 5, 6 };

        private void InitTextCombox()
        {
            ToolBar toolBar = Resources["TPISTextToolBar"] as ToolBar;
            foreach (object item in toolBar.Items)
            {
                if (item is ComboBox)
                {
                    if (((ComboBox)item).Name == "Fonsize")
                    {
                        ((ComboBox)item).ItemsSource = FontSize;
                    }
                    if (((ComboBox)item).Name == "LineTickness")
                    {
                        ((ComboBox)item).ItemsSource = LineTicknessType;
                    }
                }
            }
        }
        #endregion
    }

}
