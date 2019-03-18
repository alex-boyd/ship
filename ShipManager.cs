using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance = null;

    public GameObject shipParent;

    public GameObject shipPrefab;

    public Ship currentShipScript;

    public GameObject currentShip;

    public List<Ship> ships = new List<Ship>();

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

        shipParent = new GameObject();
        shipParent.name = "shipParent";
    
        currentShip = Instantiate
        (
            shipPrefab, 
            HexManager.instance.GetOrigin().waypoint.position, 
            HexManager.instance.GetOrigin().waypoint.rotation, 
            shipParent.transform
        );

       currentShipScript = currentShip.GetComponent<Ship>();

       currentShipScript.position = HexManager.instance.GetOrigin();

       ships.Add(currentShipScript);
       
    }

    public void MoveTo(Hex target, Ship s)
    {
        if(s.position != target && s.destination != target)
        {
            s.ClearQueue();
            s.destination = target;
            Queue<Hex> path = PathfindingManager.instance.PathBetween(s.position, target);

            foreach (Hex h in path)
            {
                MoveAction ma = new MoveAction();
                ma.destination = h; 
                s.QueueAction(ma);
            }
        
            if (!s.moving)
            {
                s.Act();
            }
        }
    }   
    
    public void ShipUpdate()
    {
        foreach (Ship s in ships)
        {
            //s.Act();
            s.Move();
        }
    }
}
