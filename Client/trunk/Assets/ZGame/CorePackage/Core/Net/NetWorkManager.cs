using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thrift.Protocol;
using Thrift.Transport;
using UnityEngine;

namespace Net
{
    public class NetWorkManager : MonoBehaviour
    {
        private static NetWorkManager _instance;

        public static NetWorkManager Instance
        {
            get { return _instance; }
        }

        void Awake()
        {
            _instance = this;
        }

        public TProtocol Connect(string url, int port)
        {
            string requestUrl = "";
            if (port > 0)
            {
                requestUrl = string.Format("http://{0}:{1}/", url, port);
            }
            else
            {
                requestUrl = string.Format("http://{0}/", url);
            }

            TTransport transport = new THttpClient(new Uri(requestUrl));
            TProtocol protocol = new TBinaryProtocol(transport);
            return protocol;
        }
    }
}
