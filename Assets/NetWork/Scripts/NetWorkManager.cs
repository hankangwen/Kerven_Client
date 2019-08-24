using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetWorkManager : MonoBehaviour
{
    // public Text msgText;
    GameStart gameStart;
    void Start()
    {
        gameStart = GetComponent<GameStart>();
    }

    public void SendMsg(string msg)
    {
        gameStart.SendMsg(msg);
    }

    public void SendMsgWithClientName(string msg, string name)
    {
        string newMsg = name + ":" + msg;
        gameStart.SendMsg(newMsg);
    }

    public bool CheckHasMsg(){
        return MsgPool.Instance.CheckHasMsg();
    }

    public string GetAndRemoveMsg(){
        return MsgPool.Instance.GetAndRemoveMsg();
    }
}
