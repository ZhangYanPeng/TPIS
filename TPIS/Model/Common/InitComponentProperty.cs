﻿using CurvesData;
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
        public static ObservableCollection<PropertyGroup> InitComponentProperty(EleType eleType)
        {
            Element element = CommonTypeService.LoadElement(eleType);
            ObservableCollection<PropertyGroup> PropertyGroups = new ObservableCollection<PropertyGroup>();
            foreach (string key in element.DProperty.Keys)
            {
                TPISNet.Property property = element.DProperty[key];

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
                        if (p != null)
                            pg.Properties.Add(p);
                        break;
                    }
                }
                if (!check)
                {
                    Property p = InitProperty(key, property, SelModes);
                    PropertyGroup baseGroup = new PropertyGroup() { Flag = property.GroupFlag };
                    if (p != null)
                        baseGroup.Properties.Add(p);
                    PropertyGroups.Add(baseGroup);
                }
            }

            PropertyGroup curveGroup = new PropertyGroup() { Flag = "曲线" };

            //初始化curve
            foreach (string key in element.DLines.Keys)
            {
                Curves curve = element.DLines[key];
                Property p = InitCurve(key, curve);
                curveGroup.Properties.Add(p);
            }
            PropertyGroups.Add(curveGroup);

            return PropertyGroups;
        }

        private static Property InitCurve(string key, Curves curve)
        {
            Property p = new Property(key, curve.Name, curve.units.ToArray(), curve.tips);
            return p;
        }

        public static Property InitProperty(string key, TPISNet.Property property, ObservableCollection<SelMode> sm)
        {
            bool units;
            if (property.SelectList.Count > 0)
                units = true;
            else
                units = false;

            if (property.type == TPISNet.P_Type.ToSetAsDouble || property.type == TPISNet.P_Type.ToCal)
            {
                Property p = new Property(key, property.Data_string, property.Name, property.Data, TransformUnit(property.Unit, units), P_Type.ToSetAsDouble, sm, property.Tips);
                return p;
            }
            else if (property.type == TPISNet.P_Type.ToSetAsString)
            {
                Property p = new Property(key, property.Data_string, property.Name, property.data_string, TransformUnit(property.Unit, units), P_Type.ToSetAsString, sm, property.Tips);
                return p;
            }
            else if (property.type == TPISNet.P_Type.NotNeed || property.type == TPISNet.P_Type.Notshow)
            {
                return null;
            }
            else
            {
                Property p = new Property(key, property.Data_string, property.Name, property.data_string, TransformUnit(property.Unit, units), P_Type.ToSelect, sm, property.Tips);
                return p;
            }
        }

        internal static string[] TransformUnit(string u, bool several)
        {
            if (several)
            {
                foreach (string[] units in Units.ListAllUnitsEnum())
                {
                    if (units.Contains(u))
                        return units;
                }
            }
            else
            {
                foreach (string[] units in Units.ListAllUnits())
                {
                    if (units.Contains(u))
                        return units;
                }
            }
            return Units.NA;
        }

        //初始化 Project 的属性
        public static ObservableCollection<PropertyGroup> InitProject()
        {
            Net BackEnd = new Net();
            ObservableCollection<PropertyGroup> PropertyGroups = new ObservableCollection<PropertyGroup>();
            foreach (string key in BackEnd.DProperty.Keys)
            {
                TPISNet.Property property = BackEnd.DProperty[key];
                bool check = false;
                foreach (PropertyGroup pg in PropertyGroups)
                {
                    if (pg.Flag == property.GroupFlag)
                    {
                        check = true;
                        Property p = InitProperty(key, property, new ObservableCollection<SelMode>() { SelMode.None });
                        pg.Properties.Add(p);
                        break;
                    }
                }
                if (!check)
                {
                    Property p = InitProperty(key, property, new ObservableCollection<SelMode>() { SelMode.None });
                    PropertyGroup baseGroup = new PropertyGroup() { Flag = property.GroupFlag };
                    baseGroup.Properties.Add(p);
                    PropertyGroups.Add(baseGroup);
                }
            }
            return PropertyGroups;
        }
    }
}
