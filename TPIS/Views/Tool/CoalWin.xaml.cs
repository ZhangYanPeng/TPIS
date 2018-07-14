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
        public void SaveGasLib()
        {
            string path = "GasLib";
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            byte[] data = CommonFunction.SerializeToBinary(CoalLib);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(data);
            bw.Close();
            fs.Close();
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

        private void Coal_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
