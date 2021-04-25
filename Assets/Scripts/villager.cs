using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villager : MonoBehaviour
{
    public TextAsset[] xmlfile;
    public int dialogState = 0;
    public bool mindRead = false;
    public bool killer = false;

    public TextAsset fetchFile()
    {
        TextAsset tA = xmlfile[dialogState];
        return tA;
    }
}
