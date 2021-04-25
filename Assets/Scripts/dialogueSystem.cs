using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class dialogueSystem : MonoBehaviour
{
    public XMLDeserializer xmld;
    public Text dialogBox;
    public Text nameBox;
    public int curPage = 0;
    public string[] pages;
    public string characterName;

    public TextAsset startingText;

    public delegate void dialogAction();
    public static event dialogAction onDialogEndEvent;
    public static event dialogAction onDialogStartEvent;

    // Start is called before the first frame update
    void Start()
    {
        loadDialog(startingText);
        dialogueStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dialogueClear()
    {
        setText("...");
        curPage = 0;
    }

    public void dialogueEnded()
    {
        if(onDialogEndEvent != null)
            onDialogEndEvent();
            dialogueClear();
    }

    public void loadDialog(TextAsset characterFile){
        xmld.characterFile = characterFile;
        xmld.fetch();
    }

    public void dialogueStart()
    {
        pages = xmld.Ourtexts;
        characterName = xmld.Ourname;
        setText(xmld.Ourtexts[0]);
        if(onDialogStartEvent != null)
            onDialogStartEvent();
    }

    void setText(string text)
    {
        dialogBox.text = text;
        nameBox.text = characterName;
    }

    public void dialogueAdvance()
    {
        if (curPage+1 == pages.Length)
        {
            dialogueEnded();
        }
        else
        {
            curPage++;
            int page = curPage;
            setText(pages[page]);
        }
    }
}
