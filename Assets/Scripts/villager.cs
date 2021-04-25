using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static gameSystem;

public class villager : MonoBehaviour
{
    public TextAsset[] xmlfile;
    public int dialogState = 0;
    public bool mindRead = false;
    public bool killer = false;

    public bool questObjective = false;
    public string[] quests = {"winona1", "winona2", "winona3"};
    public int curQid = 0;
    public bool hasNewQuest = false;
    public bool questTurnin = false;

    //visual quest stuff
    public GameObject qMark;
    public GameObject EMark;
    public GameObject buttons;

    private GameObject player;
    private gameSystem gS;

    public TextAsset fetchFile()
    {
        TextAsset tA = xmlfile[dialogState];
        return tA;
    }

    public Quest currentQuest;

    void checkQuests()
    {
        if(quests.Length > 0)
        {
            currentQuest = gS.fetchQuest(quests[curQid]);
            if(currentQuest != null){
                if(currentQuest.complete == true){
                    Debug.Log(currentQuest.Qname+" was completed");
                    curQid += 1;
                    if(curQid >= quests.Length)
                    {
                        curQid -=1;
                        if(curQid < 0)
                            curQid = 0;
                    }     
                }
            }
        }
        EMark.SetActive(hasNewQuest);
        qMark.SetActive(questTurnin);
    }

    public virtual void Start() 
    {
        gS = GameObject.Find("GameSystem").GetComponent<gameSystem>();
        if(gS != null)
            Debug.Log(gS.name);
            player = gS.player;
        checkQuests();
    }

    public virtual void Speak()
    {
        /* if(questObjective == true)
        {
            if(curQid == 1)
                giveQuestCredit(curQid, 1);
        } */
        checkQuests();
        loadVillagerDiag();
    }

    public void giveQuestCredit(int id, int amount)
    {
        gS.updateQuest(quests[id], amount);
    }

    public void ReadMind()
    {
        if(mindRead == false && gS.PsychicPower > 0)
        {
            gS.PsychicPower = gS.PsychicPower - 1;
            mindRead = true;
            Debug.Log("You Read "+xmlfile[dialogState].name+"'s mind");
        }
        else
        {
            Debug.Log("You've already read this mind, or you have no Psychic Power");
        }
    }

    public void loadVillagerDiag()
    {
        gS.diagSys.loadDialog(xmlfile[dialogState]);
        gS.diagSys.dialogueStart();
    }

    void FixedUpdate() 
    {
        if(player != null)
        {
            var dist = Vector3.Distance(player.transform.position, transform.position);
            //Debug.Log(dist);
            if(dist <= 4.75)
                buttons.SetActive(true);
            else
                buttons.SetActive(false);
        }
    }
}
