using System;

namespace vietlabs
{
    public class vlbType {
        public static Type WindZoneT { get { return "UnityEngine.WindZone".GetTypeByName("UnityEngine"); } }
        public static Type WindZoneModeT { get { return "UnityEngine.WindZoneMode".GetTypeByName("UnityEngine"); } }
    }    
}

