/**
 * Autogenerated by Thrift Compiler (0.9.3)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace Limbo.Net
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class NetMsg : TBase
  {
    private Dictionary<string, BaseMsg> _MessageBody;

    public string MessageName { get; set; }

    public Dictionary<string, BaseMsg> MessageBody
    {
      get
      {
        return _MessageBody;
      }
      set
      {
        __isset.MessageBody = true;
        this._MessageBody = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool MessageBody;
    }

    public NetMsg() {
    }

    public NetMsg(string MessageName) : this() {
      this.MessageName = MessageName;
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool isset_MessageName = false;
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.String) {
                MessageName = iprot.ReadString();
                isset_MessageName = true;
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.Map) {
                {
                  MessageBody = new Dictionary<string, BaseMsg>();
                  TMap _map13 = iprot.ReadMapBegin();
                  for( int _i14 = 0; _i14 < _map13.Count; ++_i14)
                  {
                    string _key15;
                    BaseMsg _val16;
                    _key15 = iprot.ReadString();
                    _val16 = new BaseMsg();
                    _val16.Read(iprot);
                    MessageBody[_key15] = _val16;
                  }
                  iprot.ReadMapEnd();
                }
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
        if (!isset_MessageName)
          throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("NetMsg");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        field.Name = "MessageName";
        field.Type = TType.String;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(MessageName);
        oprot.WriteFieldEnd();
        if (MessageBody != null && __isset.MessageBody) {
          field.Name = "MessageBody";
          field.Type = TType.Map;
          field.ID = 2;
          oprot.WriteFieldBegin(field);
          {
            oprot.WriteMapBegin(new TMap(TType.String, TType.Struct, MessageBody.Count));
            foreach (string _iter17 in MessageBody.Keys)
            {
              oprot.WriteString(_iter17);
              MessageBody[_iter17].Write(oprot);
            }
            oprot.WriteMapEnd();
          }
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("NetMsg(");
      __sb.Append(", MessageName: ");
      __sb.Append(MessageName);
      if (MessageBody != null && __isset.MessageBody) {
        __sb.Append(", MessageBody: ");
        __sb.Append(MessageBody);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
