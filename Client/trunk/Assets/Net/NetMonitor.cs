using UnityEngine;
using System.Collections;

namespace Limbo.Net
{
    [RequireComponent(typeof(NetDispatcher))]
    public class NetMonitor : MonoBehaviour
    {
        private string ServerAdress = "ws://socketinmono-wsycarlos.rhcloud.com:8000";

        private static NetMonitor mInstance = null;
        
        private NetHandler handler;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            if (mInstance == null)
            {
                mInstance = this;
                mInstance.handler = new NetHandler(ServerAdress, GetComponent<NetDispatcher>());
            }
        }

        void OnDestroy()
        {
            if (handler != null)
            {
                handler.Stop();
            }
            if (mInstance != null)
            {
                mInstance = null;
            }
        }

        public static void Send(NetMsg message)
        {
            if (mInstance != null)
            {
                mInstance.GetComponent<NetDispatcher>().Send(message);
            }
        }
    }
}