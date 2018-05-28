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
        }
        
    }
}
