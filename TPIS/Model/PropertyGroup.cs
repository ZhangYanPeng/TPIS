using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model
{
    public class PropertyGroup
    {
        public ObservableCollection<Property> Properties { get; set; } //该类属性字典
        public string Flag { get; set; }

        public PropertyGroup()
        {
            Properties = new ObservableCollection<Property>();
        }

        public void Add(Property property)
        {
            Properties.Add(property);
        }
    }
}
