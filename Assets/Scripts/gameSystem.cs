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
    private dialogueSystem diagSys;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        uiCheck();
        diagSys = dialogueObject.GetComponent<dialogueSystem>();
        dialogueSystem.onDialogEndEvent += onDialogEnd;
        dialogueSystem.onDialogStartEvent += onDialogStart;
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
            dialogueObject.SetActive(true);
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
        changeState(gameState.dialogue);
    }
}
