using System.Collections.Generic;
using UnityEngine;
 
 
public class ChatDemo : MonoBehaviour
{
    public ChatPanelManager cpm;
    private int count;
    private List<string> dialogue = new List<string>();
    void Start()
    {
        cpm.Init();
        dialogue.Add("永恒之星");
        dialogue.Add("永恒之星永恒之星");
        dialogue.Add("永恒之星永恒之星永恒之星");
        dialogue.Add("永恒之星永恒之星永恒之星永恒之星");
        dialogue.Add("永恒之星永恒之星永恒之星永恒之星永恒之星");
        dialogue.Add("永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星");
        dialogue.Add("永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星");
        dialogue.Add("永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星");
        dialogue.Add("永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星");
        dialogue.Add("永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星永恒之星");
    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           cpm.AddBubble(dialogue[count],Random.Range(0,2)>0);
           count++;
           if (count > dialogue.Count-1)
           {
               count = 0;
           }
        }
    }
}
