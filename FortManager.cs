using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortManager : MonoBehaviour 
{
    public static FortManager instance = null;

    public GameObject fortParent;

    public GameObject fortPrefab;

    public Fort currentFortScript;

    public GameObject currentFort;

    public List<Fort> forts = new List<Fort>();

    public int radius = 8;

    private void Awake()
    {
        //singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {

        fortParent = new GameObject();
        fortParent.name = "fortParent";
       
    }

    public void AddFort(Hex h)
    {
       //check to see if there are nearby forts that would cramp her style
       foreach(Fort f in forts)
       {
           int d = HexFX.DistanceInt(f.position.coordinates,h.coordinates); 

           if(d < radius)
           {
               return;
           } 
       }

        currentFort = Instantiate
        (
            fortPrefab, 
            fortParent.transform,
            true
        );

       currentFortScript = currentFort.GetComponent<Fort>();

       currentFort.transform.position = h.waypoint.position;
       currentFort.transform.rotation = h.waypoint.rotation; 

       currentFortScript.position = h;

       forts.Add(currentFortScript);

       h.fort = currentFortScript;
       currentFort = null;
    }
    
}
