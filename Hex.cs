using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hex : MonoBehaviour
{
    [Header("Attributes")]
    public int[] coordinates = new int[2];
    public bool water = false;
    public bool occupied = false;

    public Fort fort = null;
    private GameObject coordCanvas;
    private Text coordText;

    public HexMesh meshScript;

    public Transform waypoint;

    //private transform getchild 0 for each part if u move it
    private Transform mesh;// = this.transform.GetChild(1);
    public GameObject outline;

    public List<Hex> neighbors; 

    public Chunk chunk;


    private void Awake()
    {
        waypoint = this.transform.GetChild(0);
        mesh = this.transform.GetChild(1);
        outline = this.transform.GetChild(2).gameObject;
        meshScript = mesh.GetComponent<HexMesh>();

    }

    public void SetCoordinates(int a, int b)
    {
        coordinates[0] = a;
        coordinates[1] = b;
        //x+y+z = 0e
        //coordinates.z = -(a + b);
    }

    public int[] GetCoordinates()
    {
        return coordinates;
    }

    public void HideMesh()
    {
        //transform.GetChild(1).gameObject.SetActive(false);
        mesh.gameObject.SetActive(false);
    }

    public void ReceiveClick()
    {
        Ship cs = ShipManager.instance.currentShipScript;
        //movmeent 
        if(water)
        {
            ShipManager.instance.MoveTo(this,cs);

        } else 
        {
            Hex best = PathfindingManager.instance.BestWaterNeighbor(this, cs);

            if(best != null)
            {
                ShipManager.instance.MoveTo(best, cs);

                if(fort != null)
                {
                    fort.Interact(cs);

                }

            }

        }

    }

    public void Unload()
    {
        if(fort != null)
        {
            Destroy(fort.gameObject);
        }

        Destroy(this.gameObject);
    }

    public string CoordsToString()
    {
        return coordinates[0] + ", " + coordinates[1];
    }
 

    public Transform getWaypoint()
    {
        return transform.GetChild(2);
    }

    public void SetNeighbors() 
    {
        neighbors = new List<Hex>();

        Collider[] colliders = Physics.OverlapSphere
        (
            transform.position,
            HexManager.instance.hexDetectDistance
        );


        foreach(Collider c in colliders)
        {
            Hex h = c.transform.parent.gameObject.GetComponent<Hex>();

            if(h != null)
            {
               neighbors.Add(h); 
            }

        }

    }
}



