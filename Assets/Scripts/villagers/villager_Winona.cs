using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static gameSystem;

public class villager_Winona : villager
{
    public override void Start()
    {
        base.Start();
        quests = new string[]{"allVillagers1", "winona", "winona2", "winona3"};
        curQid = 0;
    } 
    public override void Speak()
    {
        base.Speak();
    }
}
