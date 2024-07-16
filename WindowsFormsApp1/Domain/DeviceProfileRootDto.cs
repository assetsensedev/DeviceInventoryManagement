using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Domain
{
  

    public class AdrAlgorithm
    {
        public int id { get; set; }
        public string name { get; set; }
        public int category { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string purpose { get; set; }
        public int position { get; set; }
        public object parent { get; set; }
        public bool systemDefined { get; set; }
        public bool @default { get; set; }
    }

    public class DeviceRegion
    {
        public int id { get; set; }
        public string name { get; set; }
        public int category { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string purpose { get; set; }
        public int position { get; set; }
        public object parent { get; set; }
        public bool systemDefined { get; set; }
        public bool @default { get; set; }
    }

    public class DeviceType
    {
        public int id { get; set; }
        public string name { get; set; }
        public int category { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string purpose { get; set; }
        public int position { get; set; }
        public object parent { get; set; }
        public bool systemDefined { get; set; }
        public bool @default { get; set; }
    }

    public class MacVersion
    {
        public int id { get; set; }
        public string name { get; set; }
        public int category { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string purpose { get; set; }
        public int position { get; set; }
        public object parent { get; set; }
        public bool systemDefined { get; set; }
        public bool @default { get; set; }
    }

    public class RegionalParamRevision
    {
        public int id { get; set; }
        public string name { get; set; }
        public int category { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string purpose { get; set; }
        public int position { get; set; }
        public object parent { get; set; }
        public bool systemDefined { get; set; }
        public bool @default { get; set; }
    }

    public class DeviceProfileRootDto
    {
        public int id { get; set; }
        public object createdOn { get; set; }
        public object lastModifiedOn { get; set; }
        public bool deleted { get; set; }
        public object createdBy { get; set; }
        public object lastModifiedBy { get; set; }
        public object org { get; set; }
        public bool offline { get; set; }
        public string profileName { get; set; }
        public DeviceType deviceType { get; set; }
        public MacVersion macVersion { get; set; }
        public AdrAlgorithm adrAlgorithm { get; set; }
        public DeviceRegion deviceRegion { get; set; }
        public RegionalParamRevision regionalParamRevision { get; set; }
        public bool flushQueueOnActivate { get; set; }
        public int xpectedUplinkInterval { get; set; }
        public int deviceReqFreq { get; set; }
        public bool deviceSupportOTAA { get; set; }
        public bool deviceSupportClassB { get; set; }
        public bool deviceSupportClassC { get; set; }
        public string payloadCodec { get; set; }
        public int modelId { get; set; }
    }
}
