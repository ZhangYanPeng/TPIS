using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Project
{
    public class ProjectSpace
    {
        public ObservableCollection<ProjectItem> projects;

        public ProjectSpace()
        {
            projects = new ObservableCollection<ProjectItem>();
        }
    }
}
