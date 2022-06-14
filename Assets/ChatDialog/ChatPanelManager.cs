using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class ChatPanelManager : MonoBehaviour
{
    public GameObject leftBubblePrefab;
    public GameObject rightBubblePrefab;
 
    private ScrollRect scrollRect;
    private Scrollbar scrollbar;
    
    private RectTransform content;

    private Sprite myHead;
    private Sprite otherHead;

    private string chatName;
 
    [SerializeField] 
    private float stepVertical; //上下两个气泡的垂直间隔
 
    private float lastPos; //上一个气泡最下方的位置
 
    public void Init()
    {
        scrollRect = GetComponentInChildren<ScrollRect>();
        scrollbar = GetComponentInChildren<Scrollbar>();
        content = transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        lastPos = 0;
    }

    public void SetName(string name)
    {
        chatName = name;
        transform.Find("Header").GetComponentInChildren<Text>().text = chatName;
    }

    public void SetHead(Sprite head, bool isMy)
    {
        if(isMy)
        {
            myHead = head;
        }
        else
        {
            otherHead = head;
        }
    }
 
    public void AddBubble(string message, bool isMy)
    {
        GameObject newBubble = isMy ? Instantiate(rightBubblePrefab, content) : Instantiate(leftBubblePrefab, content);
        //设置气泡内容
        Text msgText = newBubble.transform.Find("Bubble").GetComponentInChildren<Text>();
        msgText.text = message;

        Image bubbleImage = newBubble.transform.Find("Head").GetComponent<Image>();
        if(isMy)
        {
            bubbleImage.sprite = myHead;
        }
        else
        {
            bubbleImage.sprite = otherHead;
        }

        RectTransform rect = newBubble.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, lastPos);

        StartCoroutine(RefreshLastPos(newBubble));
    }

    IEnumerator RefreshLastPos(GameObject bubble)
    {
        yield return new WaitForEndOfFrame();
        Image bubbleImage = bubble.transform.Find("Bubble").GetComponentInChildren<Image>();
        RectTransform rect = bubbleImage.GetComponent<RectTransform>();

        lastPos -= rect.sizeDelta.y + stepVertical;

        //更新content的长度
        if (-lastPos > this.content.rect.height)
        {
            this.content.sizeDelta = new Vector2(this.content.rect.width, -lastPos);
        }

        scrollRect.verticalNormalizedPosition = 0;//使滑动条滚轮在最下方
    }
}