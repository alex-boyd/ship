using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fort : Unit 
{
    public GameObject fortGO;

    private void Start()
    {
        fortGO = this.gameObject;
    }

    public void Interact(Ship s)
    {
          FortAction fa = new FortAction();
          fa.fort = this;
          s.QueueAction(fa);
    }

}

