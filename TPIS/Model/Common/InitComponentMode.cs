using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPISNet;

namespace TPIS.Model.Common
{
    public static partial class CommonTypeService
    {
        //包含各个元件的模式的初始化
        public static ObservableCollection<SelMode> InitComponentMode(EleType eleType)
        {
            return null;
        }

        

        private static ObservableCollection<SelMode> InitTurbinMode()
        {
            ObservableCollection<SelMode> Modes = new ObservableCollection<SelMode>();
            Modes.Add(SelMode.DesignMode);
            Modes.Add(SelMode.CalMode);
            Modes.Add(SelMode.InterMode);
            return Modes;
        }
        
        private static ObservableCollection<SelMode> InitCFBMode()
        {
            ObservableCollection<SelMode> Modes = new ObservableCollection<SelMode>();
            Modes.Add(SelMode.None);
            return Modes;
        }
    }
}
