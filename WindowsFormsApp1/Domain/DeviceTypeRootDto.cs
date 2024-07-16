using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Domain
{
    
    public class Category
    {
        public int id { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime lastModifiedOn { get; set; }
        public int createdBy { get; set; }
        public bool deleted { get; set; }
        public int lastModifiedBy { get; set; }
        public bool offline { get; set; }
        public int orgId { get; set; }
        public bool enableAlpabeticalSort { get; set; }
        public bool @internal { get; set; }
        public List<Lookup> lookups { get; set; }
        public string name { get; set; }
    }

    public class Lookup
    {
        public int id { get; set; }
        public int category { get; set; }
        public string code { get; set; }
        public bool @default { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public int position { get; set; }
        public string purpose { get; set; }
        public bool systemDefined { get; set; }
    }

    public class DeviceTypeRootDto
    {
        public Category Category { get; set; }
    }
}
