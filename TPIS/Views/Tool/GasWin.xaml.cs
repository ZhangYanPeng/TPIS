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
    public partial class GasWin : Window
    {
        public ObservableCollection<Gas> CasLib;
        public GasWin()
        {
            InitializeComponent();
            CasLib = LoadData();
            GasView.ItemsSource = CasLib;
            GasView.Items.Refresh();
        }

        /// <summary>
        /// 加载气体库
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Gas> LoadData()
        {
            string path = "CasLib";
            if (File.Exists("CasLib"))
            {
                FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                //byte[] data = new byte[fileStream.Length];
                //fileStream.Read(data, 0, data.Length);
                //fileStream.Close();
                object obj = CommonFunction.DeserializeWithBinary(fileStream);
                return (ObservableCollection<Gas>)obj;
            }
            else
            {
                return new ObservableCollection<Gas>();
            }
        }

        /// <summary>
        /// 保存气体种类库
        /// </summary>
        public void SaveGasLib(object sender, RoutedEventArgs e)
        {
            string path = "CasLib";
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            byte[] data = CommonFunction.SerializeToBinary(CasLib);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(data);
            bw.Close();
            fs.Close();
            MessageBox.Show("保存成功！");
        }

        /// <summary>
        /// 新增气体种类
        /// </summary>
        public void AddGas(object sender, RoutedEventArgs e)
        {
            CasLib.Add(new Gas());
            GasView.ItemsSource = CasLib;
            GasView.Items.Refresh();
        }

        /// <summary>
        /// 删除气体种类
        /// </summary>
        public void DeleteGas(object sender, RoutedEventArgs e)
        {
            CasLib.Remove((Gas)GasView.SelectedItem);
            GasView.ItemsSource = CasLib;
            GasView.Items.Refresh();
        }

        /// <summary>
        /// 选中退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseGas (object sender, RoutedEventArgs e)
        {
            Gas gas = (Gas)GasView.SelectedItem;
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                foreach (ObjectBase obj in mainwin.GetCurrentProject().Objects)
                {
                    if (obj is TPISComponent && obj.isSelected)
                    {
                        TPISComponent c = obj as TPISComponent;
                        if (c.eleType == TPISNet.EleType.GasSoource)
                        {
                            foreach (PropertyGroup pg in c.PropertyGroups)
                            {
                                if (pg.Flag == "气体性质")
                                {
                                    foreach (Property p in pg.Properties)
                                    {
                                        switch (p.DicName)
                                        {
                                            case "N2": p.ShowValue = gas.N2.ToString(); break;
                                            case "O2": p.ShowValue = gas.O2.ToString(); break;
                                            case "CO2": p.ShowValue = gas.CO2.ToString(); break;
                                            case "H2O": p.ShowValue = gas.H2O.ToString(); break;
                                            case "CO": p.ShowValue = gas.CO.ToString(); break;
                                            case "H2S": p.ShowValue = gas.H2S.ToString(); break;
                                            case "H2": p.ShowValue = gas.H2.ToString(); break;
                                            case "He": p.ShowValue = gas.He.ToString(); break;
                                            case "Ar": p.ShowValue = gas.Ar.ToString(); break;
                                            case "SO2": p.ShowValue = gas.SO2.ToString(); break;
                                            case "CH4": p.ShowValue = gas.CH4.ToString(); break;
                                            case "C2H6": p.ShowValue = gas.C2H6.ToString(); break;
                                            case "C3H8": p.ShowValue = gas.C3H8.ToString(); break;
                                            case "C4H10": p.ShowValue = gas.C4H10.ToString(); break;
                                            case "C5H12": p.ShowValue = gas.C5H12.ToString(); break;
                                            case "LHV": p.ShowValue = gas.LHV_KJ_Nm3.ToString(); break;
                                            case "HHV": p.ShowValue = gas.HHV_KJ_Nm3.ToString(); break;
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
