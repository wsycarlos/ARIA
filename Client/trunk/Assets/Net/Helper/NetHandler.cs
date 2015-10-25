using WebSocket4Net;
using System.Threading;

namespace Limbo.Net
{
    public class NetHandler
    {
        private WebSocket _socketInstance;

        private Thread _netThread;

        private string _serverAddress;

        private bool _looping;

        private NetDispatcher _dispatcher;

        public WebSocket socketInstance
        {
            get
            {
                return _socketInstance;
            }
        }

        public NetHandler(string address, NetDispatcher dispatcher)
        {
            _serverAddress = address;
            _dispatcher = dispatcher;
            _looping = true;
            _netThread = new Thread(Start);
            _netThread.Start();
        }

        void Start()
        {
            Connect();
            Looping();
        }

        void Connect(int times=3)
        {
            if (times > 0)
            {
                try
                {
                    _socketInstance = new WebSocket(_serverAddress);

                    _socketInstance.Opened += _socketInstance_Opened;
                    _socketInstance.Closed += _socketInstance_Closed;

                    _socketInstance.DataReceived += _socketInstance_DataReceived;
                    _socketInstance.Error += _socketInstance_Error;

                    _socketInstance.Open();
                }
                catch (System.Exception e)
                {
                    _dispatcher.LogException(e);
                    _dispatcher.LogWarning("Connect Failed! Try again 3 seconds later!");
                    Connect(times - 1);
                }
            }
            else
            {
                _dispatcher.Quit();
            }
        }

        void Looping()
        {
            while (_looping)
            {
                if (_dispatcher.sendDataQueue != null && _dispatcher.sendDataQueue.Count > 0)
                {
                    for (int i = 0; i < _dispatcher.sendDataQueue.Count; i++)
                    {
                        byte[] msg = _dispatcher.sendDataQueue.Dequeue();
                        _socketInstance.Send(msg, 0, msg.Length);
                    }
                }
            }
        }

        public void Stop()
        {
            _looping = false;
            if (_socketInstance != null)
            {
                _socketInstance.Close();
                _socketInstance = null;
            }
        }

        void _socketInstance_Opened(object sender, System.EventArgs e)
        {
            _dispatcher.Log("--------------WebSocket Open-------------");
        }

        void _socketInstance_Closed(object sender, System.EventArgs e)
        {
            _dispatcher.Log("--------------WebSocket Close-------------");
        }
        
        void _socketInstance_DataReceived(object sender, DataReceivedEventArgs e)
        {
            _dispatcher.Receive(e.Data);
        }

        void _socketInstance_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            _dispatcher.LogError("--------------WebSocket Error-------------");
            _dispatcher.LogException(e.Exception);
            _dispatcher.LogError("-----------WebSocket Error End------------");
        }
    }
}