using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.Project;

namespace TPIS.Views.Tool
{
    /// <summary>
    /// GasWin.xaml 的交互逻辑
    /// </summary>
    public partial class CoalWin : Window
    {
        public ObservableCollection<Coal> CoalLib;
        public CoalWin()
        {
            InitializeComponent();
            CoalLib = LoadData();
            CoalView.ItemsSource = CoalLib;
            CoalView.Items.Refresh();
        }

        /// <summary>
        /// 加载气体库
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Coal> LoadData()
        {
            string path = "CoalLib";
            if (File.Exists("CoalLib"))
            {
                FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] data = new byte[fileStream.Length];
                fileStream.Read(data, 0, data.Length);
                fileStream.Close();
                object obj = CommonFunction.DeserializeWithBinary(data);
                return (ObservableCollection < Coal > )obj;
            }
            else
            {
                return new ObservableCollection<Coal>();
            }
        }

        /// <summary>
        /// 保存煤种库
        /// </summary>
        public void SaveCoalLib(object sender, RoutedEventArgs e)
        {
            string path = "CoalLib";
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            byte[] data = CommonFunction.SerializeToBinary(CoalLib);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(data);
            bw.Close();
            fs.Close();
            MessageBox.Show("保存成功！");
        }
        
        /// <summary>
        /// 新增煤种
        /// </summary>
        public void AddCoal(object sender, RoutedEventArgs e)
        {
            CoalLib.Add(new Coal());
            CoalView.ItemsSource = CoalLib;
            CoalView.Items.Refresh();
        }

        /// <summary>
        /// 删除煤种
        /// </summary>
        public void DeleteCoal(object sender, RoutedEventArgs e)
        {
            CoalLib.Remove((Coal)CoalView.SelectedItem);
            CoalView.ItemsSource = CoalLib;
            CoalView.Items.Refresh();
        }

        /// <summary>
        /// 选中退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseCoal(object sender, RoutedEventArgs e)
        {
            Coal coal = (Coal)CoalView.SelectedItem;
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                foreach(ObjectBase obj in mainwin.GetCurrentProject().Objects)
                {
                    if(obj is TPISComponent && obj.isSelected)
                    {
                        TPISComponent c = obj as TPISComponent;
                        if(c.eleType == TPISNet.EleType.CoalSource)
                        {
                            foreach(PropertyGroup pg in c.PropertyGroups)
                            {
                                if(pg.Flag == "燃料性质")
                                {
                                    foreach (Property p in pg.Properties)
                                    {
                                        switch (p.DicName)
                                        {
                                            case "C" : p.ShowValue = coal.P_C.ToString();break;
                                            case "H": p.ShowValue = coal.P_H.ToString();break;
                                            case "O": p.ShowValue = coal.P_O.ToString();break;
                                            case "N": p.ShowValue = coal.P_N.ToString();break;
                                            case "S": p.ShowValue = coal.P_S.ToString();break;
                                            case "A": p.ShowValue = coal.P_A.ToString();break;
                                            case "M": p.ShowValue = coal.P_M.ToString();break;
                                            case "V": p.ShowValue = coal.P_V.ToString();break;
                                            case "LHV": p.ShowValue = coal.P_LHV.ToString(); break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Close();
        }
    }
}
