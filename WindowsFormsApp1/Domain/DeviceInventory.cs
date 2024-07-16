using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceInventory
{
    

    public class DeviceInventory
    {
        public string deviceCode { get; set; }
        public DeviceType deviceType { get; set; }
        public DeviceProfile deviceProfile { get; set; }
    }

    public class DeviceProfile
    {
        public int id { get; set; }
    }

    public class DeviceType
    {
        public int id { get; set; }
    }

    public class CreateDeviceInventoryDto
    {
        public DeviceInventory DeviceInventory { get; set; }
    }

}
