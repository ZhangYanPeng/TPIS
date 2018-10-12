using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TPIS.Model;
using TPIS.Model.Common;
using TPIS.Project;

namespace TPIS.Views
{
    public partial class CalWindow : Window
    {
        public ProjectItem project { get; set; }
        Task<ProjectItem> task;
        CancellationTokenSource cancellationTokenSource;

        #region 开始结束计算
        private void StartCalBtn(object sender, RoutedEventArgs e)
        {
            List<double> data = new List<double>();
            //清空监视变量
            foreach (MonitorData md in LMonitors)
            {
                md.ClearData();
            }
            ReDrawFig();

            if (!MaxIter_Check())
                return;
            CalculateResult();
        }

        private void EndCalBtn(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource.Cancel();
            task = null;
            project.CalculateState = false;
        }
        #endregion

        #region 计算
        public void CalculateResult()
        {
            try
            {
                //设置结果
                bool RTextExist = false;
                for (int i = 0; i < project.Objects.Count; i++)
                {
                    if (project.Objects[i] is TPISText)
                    {
                        TPISText rtext = (TPISText)project.Objects[i];
                        if (rtext.IsResult == true)
                        {
                            RTextExist = true;
                        }
                    }
                }
                if (!RTextExist)
                {
                    project.AddResultText((int)(project.Canvas.V_width - 200), 20, 20, "");
                }

                SetCalState(true);
                cancellationTokenSource = new CancellationTokenSource();
                ProjectItem pi = (ProjectItem)project.Clone();
                task = new Task<ProjectItem>(() => CalculateCurrent(pi, cancellationTokenSource.Token), cancellationTokenSource.Token);
                task.Start();
                Task cwt = task.ContinueWith(t => {
                    pi = t.Result;
                    if (pi != null)
                    {
                        //获取元件结果
                        foreach (ObjectBase obj in project.Objects)
                        {
                            foreach (ObjectBase objr in pi.Objects)
                            {
                                if (obj is TPISComponent && objr is TPISComponent && obj.No == objr.No)
                                {
                                    ((TPISComponent)obj).ResultGroups = ((TPISComponent)objr).ResultGroups;
                                    //获取接口计算结果
                                    foreach (Port port in ((TPISComponent)obj).Ports)
                                    {
                                        foreach (Port portr in ((TPISComponent)objr).Ports)
                                        {
                                            if (port.DicName == portr.DicName)
                                                port.Results = portr.Results;
                                        }
                                    }
                                    ((TPISComponent)obj).OnCaculateFinished();
                                }
                            }
                        }
                        //获取系统结果
                        project.ResultGroup = pi.ResultGroup;
                        project.Logs = pi.logs;
                        project.CalculateState = false;
                        //展示结果
                        for (int i = 0; i < project.Objects.Count; i++)
                        {
                            if (project.Objects[i] is TPISText)
                            {
                                TPISText rtext = (TPISText)project.Objects[i];
                                if (rtext.IsResult == true)
                                {
                                    RTextExist = true;
                                    String result = "";
                                    int rw = 0, rh = 0;
                                    foreach (PropertyGroup pg in project.resultGroup)
                                    {
                                        foreach (Property p in pg.Properties)
                                        {
                                            if (p.ShowValue == "")
                                                continue;
                                            rh = rh + 25;
                                            rw = Math.Max(rw, (p.Name.Length + p.ShowValue.Length + 1) * 20);
                                            result += p.Name + ":" + p.ShowValue + "\r\n";
                                        }
                                    }
                                    rtext.Text = result;
                                    rtext.Position.V_width = rw;
                                    rtext.Position.V_height = rh;
                                    if (rtext.Position.V_x + rtext.Position.V_width > project.Canvas.V_width)
                                        rtext.Position.V_x = project.Canvas.V_width - rtext.Position.V_width;
                                }
                            }
                        }
                        project.OnCaculateFinished();
                    }
                });
            }
            catch
            {
                SetCalState(true);
                return;
            }
        }

        public ProjectItem CalculateCurrent(object data, CancellationToken token)
        {
            try
            {
                ProjectItem pi = data as ProjectItem;
                token.ThrowIfCancellationRequested();
                pi.RebuildLink();
                token.ThrowIfCancellationRequested();
                CalculateInBackEnd cal = new CalculateInBackEnd(pi.BackEnd);
                cal.InitMonitors(LMonitors);
                token.ThrowIfCancellationRequested();
                pi = cal.Calculate(pi, UpdateMonitorData, token);
                token.ThrowIfCancellationRequested();
                return pi;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        private void SetCalState(bool v)
        {
            if (v)
            {
                project.CalculateState = true;
            }
            else
            {
                project.CalculateState = false;
            }
        }
    }
}
