using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XMLDeserializer : MonoBehaviour
{
    public string name;
    public string[] texts;

//done init
    private void Start()
    {
        Char a = XMLOp.Deserialize<Char>("assets/XML/denoise.xml");
        texts = a.texts;
        name = a.name;
    }
}