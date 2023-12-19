using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeviceInventory.Domain
{
    public class ErrorMessageDto
    {
        public string id { get; set; }
        public string timestamp { get; set; }
        public string statusCode { get; set; }
        public string status { get; set; }
        public string errorCode { get; set; }
        public string message { get; set; }
    }
}
