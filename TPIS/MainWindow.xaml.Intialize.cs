using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TPIS.Model;
using TPIS.Model.Common;

namespace TPIS
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 加载所有元件类型
        /// </summary>
        private void loadComponentType()
        {
            TypeList = new List<BaseType>();
            int id = 0;
            foreach (EleType eleType in Enum.GetValues(typeof(EleType)))
            {
                id++;
                string[] info = CommonTypeService.GetComponentType(eleType);//元件簇名，图片路径，元件名
                if (info == null)
                    continue;

                ComponentType ct = new ComponentType { Id = id, PicPath = info[1], Name = info[2], Type = eleType, IsChecked = false };

                bool check = true;
                foreach (BaseType bt in TypeList)
                {
                    if (!check)
                        break;
                    if (bt.Name == info[0])
                    {
                        check = false;
                        bt.ComponentTypeList.Add(ct);
                        break;
                    }
                }
                if (check)
                {
                    BaseType bt = new BaseType();
                    bt.Name = info[0];
                    bt.ComponentTypeList.Add(ct);
                    TypeList.Add(bt);
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
        #endregion
    }

}
