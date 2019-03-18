using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortAction : Action 
{
    public Fort fort = null;
    override public void Execute(Unit actor, Unit subject = null)
    {
        var  a = (Ship)actor;
        if (a != null) 
        {
            a.idle = true;
            Debug.Log("Im a fort!! ;3");

        } else 
        {
            Debug.Log("Not a Ship exception!");
        }

    }


}
