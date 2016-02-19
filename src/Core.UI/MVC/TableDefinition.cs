using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eSIS.Core.UI.MVC
{
    public enum TableTypeEnum
    {
        Readonly = 0,
        Select = 1,
        View = 2,
        Edit = 3,
        ViewEdit = 4,
        ViewHistory = 5
    }

    internal class TableDefinition<T>
    {
        private List<object> _recordsToDisplay;

        public TableDefinition()
        {
            Name = "<<Not Set>>";
            SearchFormID = "Search";
            Properties = new List<PropertyInfo>();
        }

        public TableTypeEnum Type { get; set; }
        public Func<T, object> PropertySelector { get; set; }
        public List<T> Data { get; set; }
        public IDictionary<string, object> Attributes { get; set; }
        public string Name { get; set; }
        public string SearchFormID { get; set; }
        public List<PropertyInfo> Properties { get; set; }

        public List<object> RecordsToDisplay()
        {
            if (_recordsToDisplay == null && PropertySelector != null)
            {
                _recordsToDisplay = PropertySelector != null
                    ? Data.Select(PropertySelector).ToList()
                    : Data as List<object>;
            }
            else if (_recordsToDisplay == null)
            {
                var foo = Data as IEnumerable;
                _recordsToDisplay = foo.OfType<object>().ToList();
            }

            return _recordsToDisplay;
        }
    }
}
