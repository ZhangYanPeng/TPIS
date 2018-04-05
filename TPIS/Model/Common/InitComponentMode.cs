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
            Element element = CommonTypeService.LoadElement(eleType);
            ObservableCollection<SelMode> Modes = new ObservableCollection<SelMode>();
            foreach( CalMode cm in element.LCalMode)
            {
                Modes.Add(TransSelMode(cm.Mode));
            }
            if(Modes.Count == 0)
                Modes.Add(SelMode.None);
            return Modes;
        }
        
        internal static SelMode TransSelMode(TPISNet.SelMode sm)
        {
            switch (sm)
            {
                case TPISNet.SelMode.插值模式: return SelMode.InterMode;
                case TPISNet.SelMode.计算模式: return SelMode.CalMode;
                case TPISNet.SelMode.设计模式: return SelMode.DesignMode;
            }
            return SelMode.None;
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
