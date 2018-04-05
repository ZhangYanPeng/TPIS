using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPIS.Project;
using TPISNet;

namespace TPIS.Model.Common
{
    public class CalculateInBackEnd
    {
        public static Interface BackEnd;

        public static ProjectItem Calculate(ProjectItem project)
        {
            //传入参数
            Init(project);
            BackEnd.PNet.Solve();
            return GetResult(project);
        }

        private static ProjectItem GetResult(ProjectItem project)
        {
            foreach (ObjectBase obj in project.Objects)
            {
                if (obj is TPISComponent)
                {
                    TPISComponent component = obj as TPISComponent;
                    BackEnd.Add(component.No, component.eleType);
                    Element element = BackEnd.GetElement(component.No);
                    //传属性
                    foreach (PropertyGroup pg in component.ResultGroups)
                    {
                        foreach (Property p in pg.Properties)
                        {
                            //属性传入值
                            if (p.Type == P_Type.ToSetAsDouble)
                                p.valNum = element.DPResult[p.DicName].Data;
                            else if (p.Type == P_Type.ToSetAsString)
                                p.valStr = element.DPResult[p.DicName].Data_string;
                            else if (p.Type == P_Type.ToSelect)
                            {
                                string unit = element.DPResult[p.DicName].SelectList[element.DPResult[p.DicName].SIndex];
                                for( int i=0; i<(p.Units).Count<string>(); i++)
                                {
                                    if (p.Units[i] == unit)
                                        p.UnitNum = i;
                                }
                            }
                        }
                    }
                }
            }
            return project;
        }

        public static void Init(ProjectItem project)
        {
            BackEnd = new Interface();
            BackEnd.PNet = new Net();
            //传入参数
            foreach (ObjectBase obj in project.Objects)
            {
                if(obj is TPISComponent)
                {
                    TPISComponent component = obj as TPISComponent;
                    BackEnd.Add(component.No, component.eleType);
                    Element element = BackEnd.GetElement(component.No);
                    //传属性
                    foreach(PropertyGroup pg in component.PropertyGroups)
                    {
                        foreach(Property p in pg.Properties)
                        {
                            //属性传入值
                            if (p.Type == P_Type.ToSetAsDouble)
                                element.DProperty[p.DicName].Data = p.valNum;
                            else if (p.Type == P_Type.ToSetAsString)
                                element.DProperty[p.DicName].Data_string = p.valStr;
                            else if (p.Type == P_Type.ToSelect)
                            {
                                string unit = (p.Units)[p.UnitNum];
                                element.DProperty[p.DicName].SIndex = element.DProperty[p.DicName].SelectList.IndexOf(unit);
                            }
                        }
                    }
                }
            }
        }
    }
}
