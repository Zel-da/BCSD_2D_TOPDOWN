using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkPanel;
    public Image portrainImg;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

    public void Action(GameObject scanObj)
    {

            scanObject = scanObj;
            ObjectData objData = scanObject.GetComponent<ObjectData>();
            Talk(objData.id, objData.isNpc);
            //talkText.text = "이것의 이름은 " + scanObject.name + "이라고 한다.";

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int qusetTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData =  talkManager.GetTalk(id+ qusetTalkIndex, talkIndex);

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        if(isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            portrainImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portrainImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portrainImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}