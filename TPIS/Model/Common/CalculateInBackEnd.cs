﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using TPIS.Project;
using TPIS.Views;
using TPISNet;

namespace TPIS.Model.Common
{
    public class CalculateInBackEnd
    {
        public Net BackEnd;

        public CalculateInBackEnd(ProjectItem project)
        {
            BackEnd = new Net();
            //传入参数
            Init(project);
        }

        public CalculateInBackEnd(Net backend)
        {
            BackEnd = backend;
        }

        public ProjectItem Calculate(ProjectItem project, Action<List<double>> updateMonitorData, CancellationToken token)
        {
            //传入参数
            BackEnd.MaxIter = project.MaxIter;
            BackEnd.WaterStand = project.WaterStand;
            BackEnd.GasStand = project.GasStand;

            Random rd = new Random();
            for (int i = 1; i < 120; i++)
            {
                double t = ((double)rd.Next(100)) / 10 - 5;
                updateMonitorData(new List<double>(){t});
                token.ThrowIfCancellationRequested();
                Thread.Sleep(1000);
            }

            Init(project);
            token.ThrowIfCancellationRequested();
            BackEnd.Solve();
            token.ThrowIfCancellationRequested();
            GetGlobalResult(project);
            token.ThrowIfCancellationRequested();
            return GetResult(project);
        }

        //初始化 Project 的属性
        public void GetGlobalResult(ProjectItem project)
        {
            ObservableCollection<PropertyGroup> PropertyGroups = new ObservableCollection<PropertyGroup>();
            foreach (string key in BackEnd.DPResult.Keys)
            {
                TPISNet.Property property = BackEnd.DPResult[key];
                bool check = false;
                foreach (PropertyGroup pg in PropertyGroups)
                {
                    if (pg.Flag == property.GroupFlag)
                    {
                        check = true;
                        Property p = CommonTypeService.InitProperty(key, property, new ObservableCollection<SelMode>() { SelMode.None });
                        p.SelectProperty(SelMode.None,true);
                        pg.Properties.Add(p);
                        break;
                    }
                }
                if (!check)
                {
                    Property p = CommonTypeService.InitProperty(key, property, new ObservableCollection<SelMode>() { SelMode.None });
                    PropertyGroup baseGroup = new PropertyGroup() { Flag = property.GroupFlag };
                    p.SelectProperty(SelMode.None,true);
                    //属性传入值
                    if (p.Type == P_Type.ToSetAsDouble)
                    {
                        if (BackEnd.DPResult[p.DicName].Data_string != "")
                            p.IsKnown = true;
                        p.valNum = BackEnd.DPResult[p.DicName].Data;
                        p.UnitNum = 0;
                        p.ShowValue = BackEnd.DPResult[p.DicName].Data_string;
                    }
                    else if (p.Type == P_Type.ToSetAsString)
                        p.valStr = BackEnd.DPResult[p.DicName].Data_string;
                    else if (p.Type == P_Type.ToSelect)
                    {
                        string unit = BackEnd.DPResult[p.DicName].SelectList[BackEnd.DPResult[p.DicName].SIndex];
                        for (int i = 0; i < (p.Units).Count<string>(); i++)
                        {
                            if (p.Units[i] == unit)
                                p.UnitNum = i;
                        }
                    }
                    else if (p.Type == P_Type.ToCal)
                    {
                        p.valNum = BackEnd.DPResult[p.DicName].Data;
                        p.ShowValue = BackEnd.DPResult[p.DicName].Data_string;
                    }
                    baseGroup.Properties.Add(p);
                    PropertyGroups.Add(baseGroup);
                }
            }
            project.ResultGroup = PropertyGroups;
        }

        private ProjectItem GetResult(ProjectItem project)
        {
            foreach (ObjectBase obj in project.Objects)
            {
                if (obj is TPISComponent)
                {
                    TPISComponent component = obj as TPISComponent;
                    Element element = Interface.GetElement(BackEnd, component.No);
                    //传属性
                    foreach (PropertyGroup pg in component.ResultGroups)
                    {
                        foreach (Property p in pg.Properties)
                        {
                            //属性传入值
                            if (p.Type == P_Type.ToSetAsDouble)
                            {
                                if (element.DPResult[p.DicName].Data_string != "")
                                    p.IsKnown = true;
                                p.valNum = element.DPResult[p.DicName].Data;
                                p.UnitNum = 0;
                                p.ShowValue = element.DPResult[p.DicName].Data_string;
                            }
                            else if (p.Type == P_Type.ToSetAsString)
                                p.valStr = element.DPResult[p.DicName].Data_string;
                            else if (p.Type == P_Type.ToSelect)
                            {
                                string unit = element.DPResult[p.DicName].SelectList[element.DPResult[p.DicName].SIndex];
                                for (int i = 0; i < (p.Units).Count<string>(); i++)
                                {
                                    if (p.Units[i] == unit)
                                        p.UnitNum = i;
                                }
                            }
                            else if (p.Type == P_Type.ToCal)
                            {
                                p.valNum = element.DPResult[p.DicName].Data;
                                p.ShowValue = element.DPResult[p.DicName].Data_string;
                            }
                        }
                    }
                    foreach(Port p in component.Ports)
                    {
                        foreach(String key in element.IOPoints.Keys)
                        {
                            Nozzle n = element.IOPoints[key] as Nozzle;
                            if (p.DicName == key)
                            {
                                foreach (Property pr in p.Results)
                                {
                                    //属性传入值
                                    if (pr.Type == P_Type.ToSetAsDouble)
                                    {
                                        if (n.DProperty[pr.DicName].Data_string != "")
                                            pr.IsKnown = true;
                                        pr.valNum = n.DProperty[pr.DicName].Data;
                                        pr.UnitNum = 0;
                                        pr.ShowValue = n.DProperty[pr.DicName].Data_string;
                                    }
                                    else if (pr.Type == P_Type.ToSetAsString)
                                        pr.valStr = n.DProperty[pr.DicName].Data_string;
                                    else if (pr.Type == P_Type.ToSelect)
                                    {
                                        string unit = n.DProperty[pr.DicName].SelectList[n.DProperty[pr.DicName].SIndex];
                                        for (int i = 0; i < (pr.Units).Count<string>(); i++)
                                        {
                                            if (pr.Units[i] == unit)
                                                pr.UnitNum = i;
                                        }
                                    }
                                    else if (pr.Type == P_Type.ToCal)
                                    {
                                        pr.valNum = n.DProperty[pr.DicName].Data;
                                        pr.ShowValue = n.DProperty[pr.DicName].Data_string;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            project.Logs = BackEnd.warnning;
            return project;
        }

        public void Init(ProjectItem project)
        {
            BackEnd.elements.Clear();
            BackEnd.Pipes.Clear();
            //传入参数
            foreach (PropertyGroup pg in project.PropertyGroup)
            {
                foreach (Property p in pg.Properties)
                {
                    //属性传入值
                    if (p.Type == P_Type.ToSetAsDouble)
                        BackEnd.DProperty[p.DicName].Data = p.valNum;
                    else if (p.Type == P_Type.ToSetAsString)
                        BackEnd.DProperty[p.DicName].Data_string = p.valStr;
                    else if (p.Type == P_Type.ToSelect)
                    {
                        string unit = (p.Units)[p.UnitNum];
                        BackEnd.DProperty[p.DicName].SIndex = BackEnd.DProperty[p.DicName].SelectList.IndexOf(unit);
                    }
                }
            }

            foreach (ObjectBase obj in project.Objects)
            {
                if (obj is TPISComponent)
                {
                    TPISComponent component = obj as TPISComponent;
                    Interface.Add(BackEnd, component.No, component.eleType);
                    Element element = Interface.GetElement(BackEnd, component.No);
                    //传属性
                    foreach (PropertyGroup pg in component.PropertyGroups)
                    {
                        foreach (Property p in pg.Properties)
                        {
                            //属性传入值
                            if (p.Type != P_Type.ToLine)
                            {
                                if (p.IsKnown)
                                {
                                    element.DProperty[p.DicName].status = P_Status.WeakSet;
                                }
                                else
                                {
                                    element.DProperty[p.DicName].status = P_Status.Unknown;
                                }
                            }
                            if (p.Type == P_Type.ToSetAsDouble)
                                element.DProperty[p.DicName].Data = p.valNum;
                            else if (p.Type == P_Type.ToSetAsString)
                                element.DProperty[p.DicName].Data_string = p.valStr;
                            else if (p.Type == P_Type.ToSelect)
                            {
                                string unit = (p.Units)[p.UnitNum];
                                element.DProperty[p.DicName].SIndex = element.DProperty[p.DicName].SelectList.IndexOf(unit);
                            }
                            else if (p.Type == P_Type.ToLine)
                            {
                                element.DLines[p.DicName] = p.Curve;
                            }
                        }
                    }
                    foreach(Port p in component.Ports)
                    {
                        if (p.Type == NodType.DefIn)
                            element.IOPoints[p.DicName].nodtype = TPISNet.NodType.Inlet;
                        else if(p.Type == NodType.DefOut)
                            element.IOPoints[p.DicName].nodtype = TPISNet.NodType.Outlet;
                        if (p.link != null)
                            element.IOPoints[p.DicName].IsLinked = true;
                        else
                            element.IOPoints[p.DicName].IsLinked = false;
                    }
                }
            }

            foreach (ObjectBase obj in project.Objects)
            {
                if (obj is TPISComponent)
                {
                    TPISComponent component = obj as TPISComponent;
                    foreach (Port port in component.Ports)
                    {
                        if (port.type == NodType.Inlet || port.type == NodType.DefIn)
                        {
                            if (port.link != null)
                            {
                                foreach (ObjectBase tobj in project.Objects)
                                {
                                    if (tobj is TPISComponent)
                                    {
                                        TPISComponent tcomponent = tobj as TPISComponent;
                                        if (tcomponent.Ports.Contains(port.link.InPort))
                                        {
                                            TPISLine line = port.link;
                                            Pipe pipe = new Pipe(line.No);
                                            pipe.init(component.No, line.OutPort.DicName, BackEnd.elements[component.No].IOPoints[line.OutPort.DicName], tcomponent.No, line.InPort.DicName, BackEnd.elements[tcomponent.No].IOPoints[line.InPort.DicName]);
                                            BackEnd.Pipes.Add(port.link.No, pipe);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
