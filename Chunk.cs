using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk //: MonoBehaviour 
{
    public int[] origin;
    //biome?
    //List<Change> changes = new List<Change>();
    public List<Hex> hexes = new List<Hex>();
    public List<Hex> edges = new List<Hex>();

    public bool initialized;

    public List<Chunk> neighbors = new List<Chunk>();

    public Chunk(int[] o)
    {
        origin = o; 
        initialized = false;
    }

    public void Load()
    {
        HexManager.instance.InitializeHexes(this);
    }
/*
    public void Unload()
    {
        
        HexManager.instance.UnloadHexes(hexes);
    }
    */
}
