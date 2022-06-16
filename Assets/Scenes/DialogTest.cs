using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class DialogTest : MonoBehaviour
{
    public DialogManager dialogManager;
    
    // Start is called before the first frame update
    void Start()
    {
        List<DialogData> dialogs = new List<DialogData>();
        DialogData dialogData = new DialogData("你好啊");
        dialogData.SelectList.Add("No", "好你妹啊");
        dialogData.SelectList.Add("Yes", "好好啊");
        dialogData.Callback = ()=> Check_Correct();
        dialogs.Add(dialogData);
        dialogManager.Show(dialogs);
    }

    private void Check_Correct()
    {
        if(dialogManager.Result == "Correct")
        {
            var dialogTexts = new List<DialogData>();

            dialogTexts.Add(new DialogData("You are right."));

            dialogManager.Show(dialogTexts);
        }
        else if (dialogManager.Result == "Wrong")
        {
            var dialogTexts = new List<DialogData>();

            dialogTexts.Add(new DialogData("You are wrong."));

            dialogManager.Show(dialogTexts);
        }
        else
        {
            var dialogTexts = new List<DialogData>();

            dialogTexts.Add(new DialogData("Right. You don't have to get the answer."));

            dialogManager.Show(dialogTexts);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
