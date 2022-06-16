using System.Collections.Generic;
using UnityEngine;
 
 
public class ChatDemo : MonoBehaviour
{
    public ChatPanelManager chat;
    public Sprite head1;
    public Sprite head2;

    private int count;
    private List<string> message = new List<string>();
    
    void Start()
    {
        chat.Init();
        chat.SetHead(head1, true);
        chat.SetHead(head2, false);
        chat.SetTitle("慢慢来");
        message.Add("永恒之星");
        message.Add("永恒之星永恒之星");
        message.Add("永恒之星永恒之星永恒之星");
        message.Add("永恒之星永恒之星永恒之星永恒之星");
        message.Add("永恒之星永恒之星永恒之星永恒之星永恒之星");
        message.Add("永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星");
        message.Add("永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星");
        message.Add("永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星");
        message.Add("永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星");
        message.Add("永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星");
    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           chat.AddBubble(message[count], Random.Range(0, 2) > 0);
           count++;
           if (count > message.Count - 1)
           {
               count = 0;
           }
        }
    }
}
