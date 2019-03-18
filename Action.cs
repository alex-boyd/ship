using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action
{
    public abstract void Execute(Unit actor, Unit subject = null);
}
