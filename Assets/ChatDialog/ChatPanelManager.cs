using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class ChatPanelManager : MonoBehaviour
{
    public GameObject leftBubblePrefab;
    public GameObject rightBubblePrefab;
 
    private ScrollRect scrollRect;
    private Scrollbar scrollbar;
    public RectTransform content;
    public Text titleText;
    public GameObject sendField;
    private Text sendFieldText;
    public Button sendButton;
    public GameObject popup;
    public GameObject selectItem;

    private AudioSource audioSource;
    private Queue<GameObject> selectItems = new Queue<GameObject>();

    private Sprite myHead;
    private Sprite otherHead;

    private string chatTitle;
    private string toSendMessage;

    private SelectData selectData;
    private int selectIndex;
 
    [SerializeField] 
    private float stepVertical; //上下两个气泡的垂直间隔
    private float lastPos; //上一个气泡最下方的位置

    private Action receiveCallback;

    private ChatSerial chatSerial;
    private int chatId;
 
    void OnEnable()
    {
        MessageCenter.Instance.RegisterMessageCallback(OnReceiveMessage);
    }

    void OnDisable()
    {
        MessageCenter.Instance.UnregisterMessageCallback(OnReceiveMessage);
    }

    private void OnReceiveMessage(ChatData chatData)
    {
        if(chatData.id == 0)
        {
            AddBubble(chatData.message, true);
        }
        else
        {
            AddBubble(chatData.message, false);
            PlayNewMessageAudio();
        }
        chatSerial.AddChat(chatData);
    }

    public void Init(int id)
    {
        InitView();
        LoadOrCreateChat(id);
    }

    private void InitView()
    {
        scrollRect = GetComponentInChildren<ScrollRect>();
        scrollbar = GetComponentInChildren<Scrollbar>();
        sendFieldText = sendField.GetComponentInChildren<Text>();
        sendField.GetComponent<Button>().onClick.AddListener(OnInputFieldClick);
        sendButton.onClick.AddListener(OnSendClick);
        audioSource = GetComponent<AudioSource>();
        lastPos = 0;
    }

    private void LoadOrCreateChat(int id)
    {
        chatId = id;
        ChatSerial serial = ChatManager.Instance.LoadChatSerial(id);
        if(serial != null)
        {
            chatSerial = serial;
            ChatData[] chatDatas = chatSerial.LoadAllChats();
            foreach (var chat in chatDatas)
            {
                if(chat.id == 0)
                {
                    AddBubble(chat.message, true);
                }
                else
                {
                    AddBubble(chat.message, false);
                }
            }
        }
        else
        {
            chatSerial = new ChatSerial(id);
        }
    }

    private void OnSendClick()
    {
        if(string.IsNullOrEmpty(toSendMessage))
        {
            return;
        }
        SendMessage();
        // CallOnSelect();
    }

    // void CallOnSelect()
    // {
    //     selectData.OnSelect(selectIndex);
    // }

    private void OnInputFieldClick()
    {
        popup.SetActive(true);
        string[] selectTexts = selectData.GetSelectTexts();
        for(int i = 0; i < selectTexts.Length; i++)
        {
            int index = i;
            var newItem = Instantiate(selectItem, popup.transform);
            newItem.GetComponent<Button>().onClick.AddListener(() => OnSelectItem(index));
            newItem.GetComponentInChildren<Text>().text = selectTexts[i];
            newItem.SetActive(true);
            selectItems.Enqueue(newItem);
        }
    }

    private void OnSelectItem(int index)
    {
        while(selectItems.Count > 0)
        {
            var item = selectItems.Dequeue();
            GameObject.Destroy(item);
        }
        popup.SetActive(false);
        AddMessageText(selectData.GetSelectText(index));
        selectIndex = index;
    }

    // public void SetTitle(string title)
    // {
    //     chatTitle = title;
    //     titleText.text = chatTitle;
    // }

    // public void SetHead(Sprite head, bool isMy)
    // {
    //     if(isMy)
    //     {
    //         myHead = head;
    //     }
    //     else
    //     {
    //         otherHead = head;
    //     }
    // }

    // public void SetSelectData(SelectData data)
    // {
    //     this.selectData = data;
    // }

    private void AddMessageText(string message)
    {
        if(string.IsNullOrEmpty(message))
        {
            return;
        }
        toSendMessage = message;
        sendFieldText.text = message;
    }

    private void SendMessage()
    {
        MessageCenter.Instance.SendMessage(new ChatData(){id = chatId, message = toSendMessage});
        toSendMessage = null;
        sendFieldText.text = null;
    }

    // public void SetReceiveCallback(Action callback)
    // {
    //     receiveCallback = callback;
    // }

    // public void LoadMessage(string message, bool isMy)
    // {
    //     AddBubble(message, isMy);
    // }

    // public void ReceiveMessage(string message, float delay)
    // {
    //     if(string.IsNullOrEmpty(message))
    //     {
    //         return;
    //     }
    //     StartCoroutine(WaitReceiveMessage(message, delay));
    // }

    // IEnumerator WaitReceiveMessage(string message, float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     AddBubble(message, false);
    //     PlayNewMessageAudio();
    //     if(receiveCallback != null)
    //     {
    //         receiveCallback();
    //     }
    // }

    private void PlayNewMessageAudio()
    {
        audioSource.Play();
    }
 
    private void AddBubble(string message, bool isMy)
    {
        GameObject newBubble = isMy ? Instantiate(rightBubblePrefab, content) : Instantiate(leftBubblePrefab, content);
        
        Text msgText = newBubble.transform.Find("Bubble").GetComponentInChildren<Text>();
        msgText.text = message;

        Image bubbleImage = newBubble.transform.Find("Head").GetComponent<Image>();
        bubbleImage.sprite = isMy ? myHead : otherHead;

        RectTransform rect = newBubble.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, lastPos);

        RefreshLastPos(newBubble);
    }

    void RefreshLastPos(GameObject bubble)
    {
        RectTransform rect = bubble.transform.Find("Bubble").GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        Canvas.ForceUpdateCanvases();
        Debug.Log(rect.sizeDelta);
        lastPos -= rect.sizeDelta.y + stepVertical;

        //更新content的长度
        if (-lastPos > this.content.rect.height)
        {
            this.content.sizeDelta = new Vector2(this.content.rect.width, -lastPos);
        }

        scrollRect.verticalNormalizedPosition = 0;//使滑动条滚轮在最下方
    }
}

public class SelectData
{
    private List<string> selectTexts = new List<string>();
    private Action<int> selectCallback;
    
    public void AddSelectText(string text)
    {
        selectTexts.Add(text);
    }

    public string[] GetSelectTexts()
    {
        if(selectTexts == null)
        {
            return null;
        }
        return selectTexts.ToArray();
    }
    
    public string GetSelectText(int index)
    {
        if(selectTexts == null || selectTexts.Count == 0)
        {
            return null;
        }
        return selectTexts[index];
    }

    // public void SetCallback(Action<int> callback)
    // {
    //     selectCallback = callback;
    // }

    // public void OnSelect(int index)
    // {
    //     if(selectCallback != null)
    //     {
    //         selectCallback(index);
    //     }
    // }
}