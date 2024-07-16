using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Domain
{
    public class GetDeviceProfileResponseDto
    {
        public string message { get; set; }

        public Dictionary<int, DeviceProfileDetails> DeviceProfiles { get; set; }
    }

    public class DeviceProfileDetails
    {
        public string ProfileName { get; set; }

        public int DeviceTypePresent { get; set; }
    }
}
