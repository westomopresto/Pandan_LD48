using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XMLDeserializer : MonoBehaviour
{
    public TextAsset characterFile;
    public string Ourname;
    public string[] Ourtexts;

//done init
    public void fetch()
    {
        Char a = XMLOp.Deserialize<Char>(characterFile);
        Ourtexts = a.texts;
        Ourname = a.name;
    }
}