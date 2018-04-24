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
        //包含各个元件的属性的初始化
        public static ObservableCollection<PropertyGroup> InitComponentResult(EleType eleType)
        {
            Element element = CommonTypeService.LoadElement(eleType);
            ObservableCollection<PropertyGroup> PropertyGroups = new ObservableCollection<PropertyGroup>();
            foreach (string key in element.DPResult.Keys)
            {
                TPISNet.Property property = element.DPResult[key];

                ObservableCollection<SelMode> SelModes = new ObservableCollection<SelMode>();
                foreach (CalMode cm in element.LCalMode)
                {
                    if (cm.LDProperty.Contains(property))
                    {
                        SelModes.Add(TransSelMode(cm.Mode));
                    }
                }
                if (SelModes.Count == 0)
                {
                    SelModes = null;
                }

                bool check = false;
                foreach (PropertyGroup pg in PropertyGroups)
                {
                    if (pg.Flag == property.GroupFlag)
                    {
                        check = true;
                        Property p = InitProperty(key, property, SelModes);
                        pg.Properties.Add(p);
                        break;
                    }
                }
                if (!check)
                {
                    Property p = InitProperty(key, property, SelModes);
                    PropertyGroup baseGroup = new PropertyGroup() { Flag = property.GroupFlag };
                    baseGroup.Properties.Add(p);
                    PropertyGroups.Add(baseGroup);
                }
            }
            return PropertyGroups;
        }
        
    }
}
