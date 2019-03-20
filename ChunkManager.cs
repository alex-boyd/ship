using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour 
{
    public Chunk currentChunk;
    public bool queued = false;

    public static ChunkManager instance = null;
    
    public List<Chunk> visitedChunks = new List<Chunk>();

    public List<Chunk> loadedChunks = new List<Chunk>();

    //area to move them to?


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
        int[] originCoordinates = {0,0};
        Chunk c = new Chunk(originCoordinates);
        c.Load();
        visitedChunks.Add(c);
        loadedChunks.Add(c);
        ChunkUpdate(c);
    }

    public void ChunkUpdate(Chunk current)
    {
        currentChunk = current;

        //load new chunks for the chunk you are entering
        for(int i = 0; i < 6; i++)
        {
            int j = ((i - 1) + 6 ) % 6;
            int[] coords = new int[2]; 
            coords[0] = current.origin[0];
            coords[1] = current.origin[1];

            coords[0] += HexFX.hexDirection[i,0] * HexManager.instance.radius;
            coords[1] += HexFX.hexDirection[i,1] * HexManager.instance.radius;
            coords[0]+=HexFX.hexDirection[j,0]*(HexManager.instance.radius+1);
            coords[1]+=HexFX.hexDirection[j,1]*(HexManager.instance.radius+1);

            Chunk c = GetChunkLoaded(coords);
            if(c == null)
            {
                LoadChunk(coords);
                c = GetChunkLoaded(coords);
            }
           
            current.neighbors.Add(c); 


        }

        //unload distant old chunks
        Chunk[] loaded = new Chunk[loadedChunks.Count];
        loadedChunks.CopyTo(loaded);

        /*
        foreach(Chunk c in loaded)
        {
            if(!current.neighbors.Contains(c) && c != current)
            {
                //UnloadChunk(c);                
                continue;
            }

        }
        */

        
        //HexManager.instance.RepairHexReferences();



    }

    private void Update()
    {
        if
        (
        queued && 
        !HexManager.instance.initializingHexes &&
        ShipManager.instance.currentShipScript.position.chunk != currentChunk
        )
        {
            ChunkUpdate(ShipManager.instance.currentShipScript.position.chunk);
            queued = false;
        }
    }

    public void QueueChunkUpdate()
    {
        queued = true;
    }

    public void LoadChunk(int[] origin)
    {
        Chunk c = GetChunkVisited(origin);

        if(c == null)
        {
            //make a new chunk at this origin
            c = new Chunk(origin);
        }
        
        loadedChunks.Add(c);
        c.Load();

    }

    /*
    public void UnloadChunk(Chunk c)
    {
        c.Unload();
        loadedChunks.Remove(c);

    }
*/

    public Chunk GetChunkVisited(int[] origin)
    {
        Chunk[] visited = new Chunk[visitedChunks.Count];
        visitedChunks.CopyTo(visited);

        foreach(Chunk c in visited)
        {
            if(HexFX.CoordEquals(origin, c.origin))
            {
                return c;
            }

        }

        return null;
    }
    
    public Chunk GetChunkLoaded(int[] origin)
    {
        Chunk[] loaded = new Chunk[loadedChunks.Count];
        loadedChunks.CopyTo(loaded);

        foreach(Chunk c in loaded)
        {
            if(HexFX.CoordEquals(origin, c.origin))
            {
                return c;
            }

        }

        return null;
    }

    public void BuildGraph()
    {
        foreach(Chunk c in loadedChunks)
        {
            if(c.initialized)
            {
                HexManager.instance.BuildGraph(c.edges);
            }
            else
            {
                HexManager.instance.BuildGraph(c.hexes);
                c.initialized = true;
            }
        }
    }

}
