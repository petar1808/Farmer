using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Seedings
{
    public class SelectionListModel
    {
        public SelectionListModel(
           int value,
           string name)
        {
            this.Value = value;
            this.Name = name;
        }
        public int Value { get; set; }
        public string Name { get; set; }
    }
}
