using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TPIS.Model;
using TPISNet;

namespace TPIS
{
    public partial class MainWindow : Window
    {
        private void loadComponentType()
        {

            BaseType bt = new BaseType();
            bt.Name = "元件簇";

            int id = 0;
            foreach (EleType eleType in Enum.GetValues(typeof(EleType)))
            {
                Element e = CommonFunction.NewOriginElement(eleType, 0);
                id++;
                if (e == null)
                    continue;
                Console.WriteLine(e.PNGstr[0]);
                ComponentType ct = new ComponentType { Id = id, PicPath = e.PNGstr[0], Name = e.DProperty["Name"].data_string };
                
                bt.ComponentTypeList.Add(ct);
            }
            

            TypeList = new List<BaseType>();
            TypeList.Add(bt);

            this.ElementList.ItemsSource = TypeList;
            this.ElementList.Items.Refresh();
        }
        
    }

    

}
