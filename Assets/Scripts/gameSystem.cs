using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameSystem : MonoBehaviour
{
    public GameObject mainMenuObject;
    public enum gameState {mainmenu, pause, dialogue, village, brain, end};
    public gameState curGameState;
    public string[] scenes;

    public GameObject dialogueObject;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        uiCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        if(curGameState == gameState.mainmenu)
            loadAdditiveLevel(scenes[0]);
            mainMenuObject.SetActive(false);
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
}
