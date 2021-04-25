using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villager : MonoBehaviour
{
    public TextAsset[] xmlfile;
    public int dialogState = 0;
    public bool mindRead = false;
    public bool killer = false;

    public bool questObjective = false;
    public string[] quests = {"winona1", "winona2", "winona3"};
    private int curQid = 0;
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

    void checkQuests()
    {
        EMark.SetActive(hasNewQuest);
        qMark.SetActive(questTurnin);
    }

    void Start() 
    {
        gS = GameObject.Find("GameSystem").GetComponent<gameSystem>();
        if(gS != null)
            Debug.Log(gS.name);
            player = gS.player;
        checkQuests();
    }

    public void Speak()
    {
        loadVillagerDiag();
        if(questObjective == true)
        {
            if(curQid == 0)
                giveQuestCredit(curQid, 1);
                curQid = 1;
                dialogState = 1;
        }
    }

    void giveQuestCredit(int id, int amount)
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
        var dist = Vector3.Distance(player.transform.position, transform.position);
        //Debug.Log(dist);
        if(dist <= 4.75)
            buttons.SetActive(true);
        else
            buttons.SetActive(false);
    }
}
