using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WinForms = System.Windows.Forms;
using TPIS.Project;
using TPIS.TPISCanvas;
using System.Windows.Media;

namespace TPIS
{
    /// <summary>
    /// NewProject.xaml 的交互逻辑
    /// </summary>
    public partial class NewProject : Window
    {
        MainWindow mainwin = (MainWindow)Application.Current.MainWindow;

        public NewProject()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 新建
        /// </summary>
        private void Create(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.Parse(canvas_width.Text) <= 0 || int.Parse(canvas_height.Text) <= 0)
                {
                    MessageBox.Show("画布像素只能为正整数！", "提示", MessageBoxButton.OKCancel);
                    return;
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show("画布像素只能为正整数！", "提示", MessageBoxButton.OKCancel);
                return;
            }
            if (string.IsNullOrWhiteSpace(proj_name.Text) && string.IsNullOrWhiteSpace(proj_location.Text))
                MessageBox.Show("项目名和储位置不能为空！", "提示", MessageBoxButton.OKCancel);
            else if (string.IsNullOrWhiteSpace(proj_name.Text))
                MessageBox.Show("项目名不能为空！", "提示", MessageBoxButton.OKCancel);
            else if (string.IsNullOrWhiteSpace(proj_location.Text))
                MessageBox.Show("存储位置不能为空！", "提示", MessageBoxButton.OKCancel);
            else
            {
                mainwin.AddProject(proj_name.Text + ".tpis", int.Parse(canvas_width.Text), int.Parse(canvas_height.Text));
                Close();
            }
            //mainwin.ActiveProject(mainwin.projectList.projects.Count-1);
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 浏览文件夹
        /// </summary>
        private void FolderBrowse(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog m_Dialog = new WinForms.FolderBrowserDialog();
            WinForms.DialogResult result = m_Dialog.ShowDialog();
            if (result == WinForms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
            proj_location.Text = m_Dir;
        }

        private void canvas_width_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
