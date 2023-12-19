using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceInventory
{
    public class CreateDeviceInventoryDto
    {
        public DeviceInventory DeviceInventory { get; set; }
    }

    public class DeviceInventory
    {
        public string deviceCode { get; set; }
    }

}
