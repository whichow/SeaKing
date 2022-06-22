using System.Collections.Generic;

public struct ChatData
{
    public int id;
    public string message;
}

public class ChatSerial
{
    public int Id
    {
        get;
        private set;
    }

    private List<ChatData> chats;

    public ChatSerial(int id)
    {
        chats = new List<ChatData>();
        Id = id;
    }

    public void AddChat(ChatData chatData)
    {
        chats.Add(chatData);
    }

    public void AddChat(int id, string message)
    {
        ChatData chat = new ChatData(){ 
            id = id, 
            message = message 
        };
        chats.Add(chat);
    }

    public ChatData[] LoadAllChats()
    {
        return chats.ToArray();
    }
}