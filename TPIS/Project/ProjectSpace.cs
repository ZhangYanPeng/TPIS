using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Project
{
    public class ProjectSpace : INotifyPropertyChanged
    {
        public ObservableCollection<ProjectItem> projects;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ProjectSpace()
        {
            projects = new ObservableCollection<ProjectItem>();
            LogStr = "";
            logs = new List<string>();
        }

        private string logStr;
        public string LogStr {
            get
            {
                return logStr;
            }
            set {
                logStr = value;
                OnPropertyChanged("LogStr");
            }
        }
        public List<string> logs;
        public static int MAX_LOG_SIZE = 500;
        

        public void AddLog(ObservableCollection<string> ls)
        {
            foreach (string str in ls)
            {
                logs.Add(str);
            }
            while (logs.Count > MAX_LOG_SIZE)
                logs.RemoveAt(0);
            string tstr = "";
            foreach (string str in logs)
            {
                tstr = tstr + str + "\r\n";
            }
            LogStr = tstr;
        }
    }
}
