using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Limbo.Net
{
    public class NetDispatcher : MonoBehaviour
    {
        private Queue<string> logQueue;
        private Queue<string> logErrorQueue;
        private Queue<string> logWarningQueue;
        private Queue<Exception> logExceptionQueue;
        private Queue<NetMsg> receiveDataQueue;
        public Queue<byte[]> sendDataQueue;
        private bool quitFlag = false;

        public void Log(string msg)
        {
            if (logQueue != null)
            {
                logQueue.Enqueue(msg);
            }
        }

        public void LogError(string msg)
        {
            if (logErrorQueue != null)
            {
                logErrorQueue.Enqueue(msg);
            }
        }

        public void LogWarning(string msg)
        {
            if (logWarningQueue != null)
            {
                logWarningQueue.Enqueue(msg);
            }
        }

        public void LogException(Exception msg)
        {
            if (logExceptionQueue != null)
            {
                logExceptionQueue.Enqueue(msg);
            }
        }

        public void Receive(byte[] msg)
        {
            if (receiveDataQueue != null)
            {
                receiveDataQueue.Enqueue(NetCoding.Deserialize(msg));
            }
        }

        public void Send(NetMsg msg)
        {
            if (sendDataQueue != null)
            {
                sendDataQueue.Enqueue(NetCoding.Serialize(msg));
            }
        }

        public void Quit()
        {
            quitFlag = true;
        }

        void Start()
        {
            logQueue = new Queue<string>();
            logErrorQueue = new Queue<string>();
            logWarningQueue = new Queue<string>();
            logExceptionQueue = new Queue<Exception>();
            receiveDataQueue = new Queue<NetMsg>();
            sendDataQueue = new Queue<byte[]>();
            quitFlag = false;
        }

        void Update()
        {
            if (logQueue != null && logQueue.Count > 0)
            {
                for (int i = 0; i < logQueue.Count; i++)
                {
                    string msg = logQueue.Dequeue();
                    Debug.Log(msg);
                }
            }
            if (logErrorQueue != null && logErrorQueue.Count > 0)
            {
                for (int i = 0; i < logErrorQueue.Count; i++)
                {
                    string msg = logErrorQueue.Dequeue();
                    Debug.LogError(msg);
                }
            }
            if (logWarningQueue != null && logWarningQueue.Count > 0)
            {
                for (int i = 0; i < logWarningQueue.Count; i++)
                {
                    string msg = logWarningQueue.Dequeue();
                    Debug.LogWarning(msg);
                }
            }
            if (logExceptionQueue != null && logExceptionQueue.Count > 0)
            {
                for (int i = 0; i < logExceptionQueue.Count; i++)
                {
                    Exception msg = logExceptionQueue.Dequeue();
                    Debug.LogException(msg);
                }
            }
            if (receiveDataQueue != null && receiveDataQueue.Count > 0)
            {
                for (int i = 0; i < receiveDataQueue.Count; i++)
                {
                    NetMsg msg = receiveDataQueue.Dequeue();
                    dispatch(msg);
                }
            }
            if (quitFlag)
            {
                quit();
            }
        }

        void dispatch(NetMsg message)
        {
            string msgName = message.MessageName;
            Type handler = Type.GetType("Limbo.Net." + msgName +
                "Handler, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=nul");
            if (message.MessageBody == null && message.MessageBody.Count <= 0)
            {
                handler.InvokeMember("Default", BindingFlags.Default, null, null, null);
            }
            else
            {
                foreach (string methodName in message.MessageBody.Keys)
                {
                    BaseMsg bmsg = message.MessageBody[methodName];
                    List<object> args = new List<object>();
                    if (bmsg.MsgType == VariableType.BOOL)
                    {
                        args.Add(bmsg.BoolVal);
                    }
                    else if (bmsg.MsgType == VariableType.BYTE)
                    {
                        args.Add(bmsg.ByteVal);
                    }
                    else if (bmsg.MsgType == VariableType.BYTEARR)
                    {
                        args.Add(bmsg.ByteArrVal);
                    }
                    else if (bmsg.MsgType == VariableType.DOUBLE)
                    {
                        args.Add(bmsg.DoubleVal);
                    }
                    else if (bmsg.MsgType == VariableType.INT)
                    {
                        args.Add(bmsg.IntVal);
                    }
                    else if (bmsg.MsgType == VariableType.LIST)
                    {
                        args.Add(bmsg.ListVal);
                    }
                    else if (bmsg.MsgType == VariableType.MAP)
                    {
                        args.Add(bmsg.MapVal);
                    }
                    else if (bmsg.MsgType == VariableType.SET)
                    {
                        args.Add(bmsg.SetVal);
                    }
                    else if (bmsg.MsgType == VariableType.STR)
                    {
                        args.Add(bmsg.StrVal);
                    }
                    handler.InvokeMember(methodName, BindingFlags.Default, null, null, args.ToArray());
                }
            }
        }

        void quit()
        {
            Application.runInBackground = false;
            Time.timeScale = 0;
            Application.Quit();
        }
    }
}