using System.Collections;
using System.Collections.Generic;

public class MsgPool
{
    private static MsgPool _instance = new MsgPool();
    public static MsgPool Instance
    {
        get { return _instance; }
    }

    List<string> msgPool = new List<string>();
    public void AddMsg(string msg)
    {
        msgPool.Add(msg);
    }

    public string GetAndRemoveMsg()
    {
        string msg;
        int index = msgPool.Count-1;
        msg = msgPool[index];
        msgPool.RemoveAt(index);
        return msg;
    }

    public bool CheckHasMsg()
    {
        return (msgPool.Count > 0);
    }
}
