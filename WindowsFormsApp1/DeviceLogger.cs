using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public static class  DeviceLogger
    {
        public static Logger logger;
        static DeviceLogger()
        {
            logger = LogManager.GetLogger("");
            logger.Debug("Started Logging");
        }
    }
}
