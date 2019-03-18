using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action 
{
    public Hex destination;

    override public void Execute(Unit actor, Unit subject = null)
    {
        var  a = (Ship)actor;

        if (a != null) 
        {
            a.idle = false;
            a.moving = true;
            a.SetTarget(destination);

        } else 
        {
            Debug.Log("Not a Ship exception!");
        }
    }

}
