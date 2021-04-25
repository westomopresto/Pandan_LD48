using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Text;

[XmlRoot("Char")]
public class Char
    {
        [XmlElement("name")]
        public string name;

        [XmlArray("texts"), XmlArrayItem("text")]
        public string[] texts;
    }

public class XMLOp
{
    public static void Serialize(object item, string path)
    {
        XmlSerializer serializer = new XmlSerializer(item.GetType());
        StreamWriter writer = new StreamWriter(path);
        serializer.Serialize(writer.BaseStream, item);
        writer.Close();
    }
    public static T Deserialize<T>(TextAsset tA)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(T));
        MemoryStream mStrm= new MemoryStream( Encoding.UTF8.GetBytes( tA.text ) );
		StreamReader reader = new StreamReader(mStrm); 
        T deserialized = (T)serializer.Deserialize(mStrm);
		reader.Close();
		return deserialized;
	}
}

public class XMLSerializer : MonoBehaviour 
{
/*  
    private void Start() 
    {
        Char Denoise = new Char();
        Denoise.name = "Denoise";
        Denoise.texts = new string[]{"The name is Denoise.", "I'm the village psychic."};

        XMLOp.Serialize(Denoise, "assets/XML/denoise.xml");
    } 
*/
}