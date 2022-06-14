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
 
    [SerializeField] 
    private float stepVertical; //上下两个气泡的垂直间隔
    [SerializeField] 
    private float stepHorizontal; //左右两个气泡的水平间隔
    [SerializeField]
    private float maxTextWidth;//文本内容的最大宽度
 
    private float lastPos; //上一个气泡最下方的位置
    private float halfHeadLength;//头像高度的一半
 
    public void Init()
    {
        scrollRect = GetComponentInChildren<ScrollRect>();
        scrollbar = GetComponentInChildren<Scrollbar>();
        content = transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        lastPos = 0;
    }
 
    public void AddBubble(string message, bool isMy)
    {
        GameObject newBubble = isMy ? Instantiate(rightBubblePrefab, content) : Instantiate(leftBubblePrefab, content);
        //设置气泡内容
        Text text = newBubble.GetComponentInChildren<Text>();
        text.text = message;

        RectTransform rect = newBubble.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, lastPos);

        StartCoroutine(RefreshLastPos(newBubble));
    }

    IEnumerator RefreshLastPos(GameObject bubble)
    {
        yield return new WaitForEndOfFrame();
        Image bubbleImage = bubble.transform.Find("Bubble").GetComponentInChildren<Image>();
        RectTransform rect = bubbleImage.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        Debug.Log(rect.sizeDelta.y);

        lastPos -= rect.sizeDelta.y + stepVertical;

        //更新content的长度
        if (-lastPos > this.content.rect.height)
        {
            this.content.sizeDelta = new Vector2(this.content.rect.width, -lastPos);
        }

        scrollRect.verticalNormalizedPosition = 0;//使滑动条滚轮在最下方
    }
}