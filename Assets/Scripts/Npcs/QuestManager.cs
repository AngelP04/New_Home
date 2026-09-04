using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest[] quests;
    public bool[] questCompleted;

    public string itemCollected;
    // Start is called before the first frame update
    void Start()
    {
        questCompleted = new bool[quests.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowQuestText(string questText)
    {
        string[] dialogLines = new string[]
        {
            questText
        };
        DialogManager.instance.currentDialogLine = 0;
        DialogManager.instance.ShowDialog(dialogLines);
    }
}
