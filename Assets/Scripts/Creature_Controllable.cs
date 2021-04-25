using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_Controllable: Creature
{
    public override void Start() 
    {
        bPlayer = true;
        base.Start();
    }

    public override void Update() 
    {
        base.Update();
        ReadInputs();
        DoInput();
    }

    void DoInput()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void ReadInputs()
    {

        if (Input.GetButtonDown("1key"))
        {
        }
        if (Input.GetButtonDown("2key"))
        { 
        }
        if (Input.GetButtonDown("3key"))
        {
        }
    }

}
