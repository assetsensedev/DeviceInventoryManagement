using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Domain
{
    public class GetDeviceTypeResponseDto
    {
        public string message { get; set; }

        public Dictionary<string,int> DeviceTypes { get; set; }
    }
}
