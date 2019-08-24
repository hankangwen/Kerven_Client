using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatUIManager : MonoBehaviour
{
    public InputField inputName;
    public InputField inputMsg;
    public Button sendBtn;

    NetWorkManager netWorkManager;

    void Start()
    {
        netWorkManager = GameObject.Find("GameStart").GetComponent<NetWorkManager>();
        sendBtn.onClick.AddListener(SendBtnClick);
    }

    private void SendBtnClick()
    {
        netWorkManager.SendMsgWithClientName(inputMsg.text, inputName.text);
    }
}
