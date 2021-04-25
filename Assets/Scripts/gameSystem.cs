using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameSystem : MonoBehaviour
{
    public GameObject mainMenuObject;
    public enum gameState {mainmenu, intro, pause, dialogue, village, brain, end};
    public gameState curGameState;
    public string[] scenes;

    public GameObject dialogueObject;
    public dialogueSystem diagSys;
    public GameObject player;

    public int PsychicPower = 0;

    public class Quest
    {
        public string Qname;
        public int progress;
        public int goal;
        public bool complete;

        public Quest(string newName, int newProgress, int newGoal, bool newComplete)
        {
            Qname = newName;
            progress = newProgress;
            goal = newGoal;
            complete = newComplete;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        uiCheck();
        diagSys = dialogueObject.GetComponent<dialogueSystem>();
        dialogueSystem.onDialogEndEvent += onDialogEnd;
        dialogueSystem.onDialogStartEvent += onDialogStart;
        QuestInitialize();
    }
    public List<Quest> AllQuests = new List<Quest>();
    void QuestInitialize()
    {
        AllQuests.Add( new Quest("winona1", 0, 1, false));
        AllQuests.Add( new Quest("allVillagers1", 0, 5, false));
        AllQuests.Add( new Quest("winona2", 0, 1, false));
        AllQuests.Add( new Quest("winona3", 0, 1, false));
        foreach (var item in AllQuests)
        {
            Debug.Log(item.Qname);
        }
    }
    public void updateQuest(string questName, int newProgress)
    {
        AllQuests.Find(i => i.Qname == questName);
        Quest thisQuest = AllQuests.Find(delegate(Quest i) { return i.Qname == questName; });
        if (thisQuest != null)
        {
            thisQuest.progress = newProgress;
            if (thisQuest.progress >= thisQuest.goal)
            {
                thisQuest.complete = true;
                Debug.Log(thisQuest.Qname+" completed!");
            }
        }     
    }
    public Quest fetchQuest(string questName)
    {
        AllQuests.Find(i => i.Qname == questName);
        Quest thisQuest = AllQuests.Find(delegate(Quest i) { return i.Qname == questName; });
        if (thisQuest != null)
        {
            return thisQuest;
        }    
            return null; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeState(gameState gs)
    {
        curGameState = gs;
    }

    public void startGame()
    {
        if(curGameState == gameState.mainmenu)
            changeState(gameState.intro);
            loadAdditiveLevel(scenes[0]);
            mainMenuObject.SetActive(false);
            diagSys.loadDialog(diagSys.startingText);
            diagSys.dialogueStart();
    }

    public void loadAdditiveLevel(string level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }

    void uiCheck()
    {
        switch (curGameState)
        {
            case gameState.mainmenu:
                dialogueObject.SetActive(false);
                break;
        }

    }

    void onDialogEnd()
    {
        dialogueObject.SetActive(false);
        changeState(gameState.village);
    }
    void onDialogStart()
    {
        dialogueObject.SetActive(true);
        changeState(gameState.dialogue);
    }
}
