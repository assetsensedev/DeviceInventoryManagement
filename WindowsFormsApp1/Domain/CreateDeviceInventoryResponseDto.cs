using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Domain
{
    public class CreatedBy
    {
        public int id { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime lastModifiedOn { get; set; }
        public int createdBy { get; set; }
        public bool deleted { get; set; }
        public int lastModifiedBy { get; set; }
        public bool offline { get; set; }
        public int orgId { get; set; }
        public string username { get; set; }
    }

    public class DeviceInventory
    {
        public int id { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime lastModifiedOn { get; set; }
        public CreatedBy createdBy { get; set; }
        public bool deleted { get; set; }
        public LastModifiedBy lastModifiedBy { get; set; }
        public bool offline { get; set; }
        public Org org { get; set; }
        public string appKey { get; set; }
        public string deviceCode { get; set; }
        public string networkKey { get; set; }
    }

    public class LastModifiedBy
    {
        public int id { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime lastModifiedOn { get; set; }
        public int createdBy { get; set; }
        public bool deleted { get; set; }
        public int lastModifiedBy { get; set; }
        public bool offline { get; set; }
        public int orgId { get; set; }
        public string username { get; set; }
    }

    public class Org
    {
        public int id { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime lastModifiedOn { get; set; }
        public int createdBy { get; set; }
        public bool deleted { get; set; }
        public int lastModifiedBy { get; set; }
        public bool offline { get; set; }
        public int org { get; set; }
        public string description { get; set; }
        public int level { get; set; }
        public string name { get; set; }
    }

    public class CreateDeviceInventoryResponseDto
    {
        public DeviceInventory DeviceInventory { get; set; }
    }

}
