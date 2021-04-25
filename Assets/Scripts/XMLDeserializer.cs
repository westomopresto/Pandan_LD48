using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XMLDeserializer : MonoBehaviour
{
    public string characterFile = "assets/XML/denoise.xml";
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