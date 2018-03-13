using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Control
    {
        public string ControlId { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public bool IsVisible { get; set; }
        public bool IsReadOnly { get; set; }
        public int Order { get; set; }
    }
}
