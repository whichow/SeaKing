using System;

public class MessageCenter
{
    public delegate void ReceiveMessage(ChatData chatData);
    private ReceiveMessage receiveMessageHandle;

    private static MessageCenter instance;

    public static MessageCenter Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new MessageCenter();
            }
            return instance;
        }
    }

    private MessageCenter()
    {

    }

    public void SendMessage(ChatData chatData)
    {
        receiveMessageHandle.Invoke(chatData);
    }

    public void RegisterMessageCallback(ReceiveMessage onReceiveMessage)
    {
        receiveMessageHandle += onReceiveMessage;
    }

    public void UnregisterMessageCallback(ReceiveMessage onReceiveMessage)
    {
        receiveMessageHandle -= onReceiveMessage;
    }
}