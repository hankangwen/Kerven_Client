﻿/************************************************************
    文件：GameStart.cs
	作者：Plane
    QQ ：1785275942
    日期：2018/10/29 5:18
	功能：PESocket客户端使用示例
*************************************************************/

using Protocol;
using UnityEngine;

public class GameStart : MonoBehaviour {
    PENet.PESocket<ClientSession, NetMsg> skt = null;
    private bool isAppIsQuit = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start() {
        skt = new PENet.PESocket<ClientSession, NetMsg>();
        skt.StartAsClient(IPCfg.srvIP, IPCfg.srvPort);

        skt.SetLog(true, (string msg, int lv) => {
            if (isAppIsQuit) return;
            switch (lv) {
                case 0:
                    msg = "Log:" + msg;
                    Debug.Log(msg);
                    break;
                case 1:
                    msg = "Warn:" + msg;
                    Debug.LogWarning(msg);
                    break;
                case 2:
                    msg = "Error:" + msg;
                    Debug.LogError(msg);
                    break;
                case 3:
                    msg = "Info:" + msg;
                    Debug.Log(msg);
                    break;
            }
        });
    }

    public void SendMsg(string msg) {
        skt.session.SendMsg(new NetMsg
        {
            msgType = MsgType.BroadcastAll,
            text = msg
        }) ;
    }

    public void SendMsgWithMsgType(string msg, int type){
        skt.session.SendMsg(new NetMsg
        {
            msgType = HandleMsgType(type),
            text = msg
        }) ;
    }

    private MsgType HandleMsgType(int type){
        switch (type) {
            case 0:
                return MsgType.BroadcastAll;
            case 1:
                return MsgType.BroadcastAllWithCurClient;
            case 2:
                return MsgType.Store;
            default:
                return MsgType.Default;
        }
    }

    private void OnApplicationQuit(){
        isAppIsQuit = true;
        if (skt != null){
            skt.Close();
        }
    }
}