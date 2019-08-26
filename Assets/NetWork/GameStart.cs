/************************************************************
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
            text = msg
        });
    }

    private void OnApplicationQuit(){
        isAppIsQuit = true;
        if (skt != null){
            skt.Close();
        }
    }
}