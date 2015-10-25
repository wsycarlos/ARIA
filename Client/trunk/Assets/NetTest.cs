using UnityEngine;
using Limbo.Net;
using System.Collections.Generic;

public class NetTest : MonoBehaviour
{
    void Start()
    {
        Debug.Log("hi");
        NetMsg msg = new NetMsg();
        msg.MessageName = "1024";
        msg.MessageBody = new Dictionary<string, BaseMsg>();
        BaseMsg bmsg = new BaseMsg();
        bmsg.DoubleVal = 1027;
        msg.MessageBody.Add("helloworld", bmsg);

        byte[] arr = NetCoding.Serialize(msg);
        Debug.Log(arr);
        NetMsg newmsg = NetCoding.Deserialize(arr);
        Debug.Log(newmsg.MessageName);
        Debug.Log(newmsg.MessageBody["helloworld"].DoubleVal);
    }
}