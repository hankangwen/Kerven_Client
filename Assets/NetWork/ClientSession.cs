using PENet;
using Protocol;
using UnityEngine;

public class ClientSession : PESession<NetMsg>
{
    protected override void OnConnected() {
    }

    protected override void OnReciveMsg(NetMsg msg) {
        PETool.LogMsg("Server Response:" + msg.text);
        MsgPool.Instance.AddMsg(msg.text);
    }

    protected override void OnDisConnected() {
    }
}