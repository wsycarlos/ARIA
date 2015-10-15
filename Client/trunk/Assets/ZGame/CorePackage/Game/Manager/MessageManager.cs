
using CLRSharp;
using Game.Core;
using Thrift.Transport;
using UnityEngine;

namespace Game.Manager
{
    /// <summary>
    /// 功能概述：
    /// 中继器，负责向外部代码通信
    /// </summary>
    public class MessageManager : ExtensionScripter
    {
        private static MessageManager _instance;

        public static MessageManager Instance
        {
            get { return _instance; }
        }

        public override void Awake()
        {
            _instance = this;
            _extensionName = "Game.Proxy.MessageProxy";
            extensionName = null;
            base.Awake();

            Messenger.AddListener(TTransportException.ThriftExceptionType.IOException.ToString(), OnThriftIOException);
            Messenger.AddListener(TTransportException.ThriftExceptionType.WebException.ToString(), OnThriftWebException);
        }

        private void OnThriftWebException()
        {
            MemberCall("ThriftWebException", methodctor);
        }

        private void OnThriftIOException()
        {
            MemberCall("ThriftIOException", methodctor);
        }
    }
}
