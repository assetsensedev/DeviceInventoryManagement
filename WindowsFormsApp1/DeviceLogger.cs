using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceInventory
{
    public static class  DeviceLogger
    {
        public static Logger MainLogger;
        public static Logger ActivityLogger;
        static DeviceLogger()
        {
            MainLogger = LogManager.GetLogger("mainLog");
            MainLogger.Debug("Started Logging");

            ActivityLogger = LogManager.GetLogger("activityLog");
        }


    }
}
