using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int questID;
    private QuestManager manager;

    public string startText, completedText;

    public bool needsItem;
    public string itemNeeded;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(needsItem && manager.itemCollected.Equals(itemNeeded))
        {
            manager.itemCollected = null;
            CompletedQuest();
        }
    }

    public void StartQuest()
    {
        manager = FindObjectOfType<QuestManager>();
        manager.ShowQuestText(startText);
    }

    public void CompletedQuest()
    {
        manager.ShowQuestText(completedText);
        manager.questCompleted[questID] = true;
        gameObject.SetActive(false);
    }
}
