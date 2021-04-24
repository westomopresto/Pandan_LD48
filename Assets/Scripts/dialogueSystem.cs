using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueSystem : MonoBehaviour
{
    public int curPage;
    public string[] pages;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dialogueClear()
    {
        setText("...");
        curPage = 0;
        pages = null;
    }

    void dialogueEnded()
    {
        dialogueClear();
    }

    void dialogueStart()
    {

    }

    void setText(string text)
    {
        Debug.Log(text);
    }

    public void dialogueAdvance()
    {
        if (pages.Length < curPage)
        {
            int page = curPage;
            curPage++;
            setText(pages[page]);
            return;
        }
        else
        {
            dialogueEnded();
        }
    }
}
