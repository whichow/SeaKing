
using System.Collections.Generic;

public class ChatManager
{
    private List<ChatSerial> chatSerials;

    private static ChatManager instance;
    public static ChatManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new ChatManager();
            }
            return instance;
        }
    }

    private ChatManager()
    {
        chatSerials = new List<ChatSerial>();
    }
    
    public void SaveChatSerial(ChatSerial chatSerial)
    {
        for (int i = 0; i < chatSerials.Count; i++)
        {
            ChatSerial serial = chatSerials[i];
            if (serial.Id == chatSerial.Id)
            {
                serial = chatSerial;
                return;
            }
        }
        chatSerials.Add(chatSerial);
    }

    public ChatSerial LoadChatSerial(int id)
    {
        foreach (var chatSerial in chatSerials)
        {
            if(chatSerial.Id == id)
            {
                return chatSerial;
            }
        }
        return null;
    }
}