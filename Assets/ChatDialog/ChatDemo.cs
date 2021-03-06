using System.Collections.Generic;
using UnityEngine;
 
 
public class ChatDemo : MonoBehaviour
{
    public ChatPanelManager chat;
    public Sprite head1;
    public Sprite head2;
    
    void Start()
    {
        ChatSerial chatSerial = new ChatSerial(5);
        chatSerial.AddChat(new ChatData(){id = 5, message = "dddddddddddd"});
        chatSerial.AddChat(new ChatData(){id = 0, message = "cccccccccccc"});
        chatSerial.AddChat(new ChatData(){id = 5, message = "vvvvvvvvv"});
        chatSerial.AddChat(new ChatData(){id = 0, message = "rrrrrrrrrr"});
        chatSerial.AddChat(new ChatData(){id = 5, message = "nnnnnnnnnnn"});
        chatSerial.AddChat(new ChatData(){id = 0, message = "pppppppppppp"});
        chatSerial.AddChat(new ChatData(){id = 5, message = "dddddddddddd"});
        ChatManager.Instance.SaveChatSerial(chatSerial);

        chat.Init(5);
        MessageCenter.Instance.SendMessage(new ChatData(){id = 5, message = "sdfsdfsdf"});
        MessageCenter.Instance.SendMessage(new ChatData(){id = 0, message = "hfghfghfgh"});
        // chat.SetHead(head1, true);
        // chat.SetHead(head2, false);
        // chat.SetTitle("慢慢来");

        // chat.LoadMessage("发士大夫士大夫", false);
        // chat.LoadMessage("发士大夫士大夫", false);
        // chat.LoadMessage("发士大夫士大夫", true);
        // chat.LoadMessage("发士大夫士大夫发士大夫士大夫发士大夫士大夫发士大夫士大夫", false);
        // chat.LoadMessage("发士大夫士大夫", true);
        // chat.LoadMessage("发士大夫士大夫", true);
        // chat.LoadMessage("发士大夫士大夫", false);
        // chat.LoadMessage("发士大夫士大夫", false);
        // chat.LoadMessage("发士大夫士大夫", true);
        // chat.LoadMessage("发士大夫士大夫", false);
        // chat.LoadMessage("发士大夫士大夫", true);
        // chat.LoadMessage("发士大夫士大夫", false);
        // chat.LoadMessage("发士大夫士大夫", true);
        // chat.LoadMessage("发士大夫士大夫", false);
        // chat.LoadMessage("发士大夫士大夫", true);
        // chat.LoadMessage("发士大夫士大夫", false);
        // chat.LoadMessage("发士大夫士大夫", true);
        // chat.LoadMessage("发士大夫士大夫", false);
        // chat.LoadMessage("发士大夫士大夫", true);
        // chat.LoadMessage("发士大夫士大夫", false);
        // chat.LoadMessage("发士大夫士大夫", true);

        // chat.ReceiveMessage("警方死哦地方", 0f);

        // SelectData data = new SelectData();
        // data.AddSelectText("我就法律手段");
        // data.AddSelectText("囧马铃薯淀粉");
        // data.SetCallback(OnSelectItem);
        // chat.SetSelectData(data);
        // chat.SetReceiveCallback(OnReceiveMessage);
    }

    private void OnReceiveMessage()
    {
        
    }

    private void OnSelectItem(int index)
    {
        // chat.ReceiveMessage("警方死哦地方", 1.5f);
    }
}
