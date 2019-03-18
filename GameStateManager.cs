using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour 
{
    public bool initialized = false;
    public bool showCoords = false;

    public static GameStateManager instance = null;

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
	// Use this for initialization
    void Start ()
    {
        RandomManager.instance.Initialize();
        TerrainManager.instance.Initialize();
        FortManager.instance.Initialize();
        HexManager.instance.Initialize(); 
        ChunkManager.instance.Initialize();
    }


    public void LateInitialize()
    {
        if(!initialized)
        {
            ShipManager.instance.Initialize();
            CameraManager.instance.Initialize();
            initialized = true;
        }
    }
	
	// Update is called once per frame
    void Update ()
    {
	    ShipManager.instance.ShipUpdate();	
    }

  
}
