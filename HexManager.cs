using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexManager : MonoBehaviour 
{
    //QUEUE THE DESTRUCTION OF HEXES AS WELL


    //parameters
    public float hexDistance;
    public float hexDetectDistance;
     
    //drag n drop
    public GameObject hexPrefab;
    public Material landMaterial;
    public Material waterMaterial;

    //contains hexes
    private GameObject hexParent;
    public List<Hex> hexes = new List<Hex>();

    //reference to absolute origin
    public Hex origin = null;

    //initialization """threading"""
    public bool initializingHexes = false;
    public int hexesPerFrame = 300;

    public List<Chunk> chunksToInitialize = new List<Chunk>();
    public List<Hex> hexesToUnload = new List<Hex>();

    //search for generation
    private Queue<int[]> frontier = new Queue<int[]>();
    private List<int[]> visited = new List<int[]>();
    public int radius = 6; 

    //singleton instance
    public static HexManager instance = null;

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
        hexParent = new GameObject();
        hexParent.name = "HexParent";
        
        hexesPerFrame = int.MaxValue;

        hexDistance = 8 * Mathf.Sqrt(3);
        hexDetectDistance = 10f;
    }	
            
    public void AddHex(int[] coords, Chunk c)
    {
        Vector3 spawnPosition = HexFX.HexCoordsToCartesian(coords);
        spawnPosition = spawnPosition * hexDistance;

        GameObject newHex = Instantiate
        (
        hexPrefab, 
        spawnPosition, 
        this.transform.rotation, 
        hexParent.transform
        );

        Hex newHexScript = newHex.GetComponent<Hex>();
        newHexScript.SetCoordinates(coords[0], coords[1]);
        newHexScript.chunk = c;
        c.hexes.Add(newHexScript);
        TerrainManager.instance.RollTerrain(newHexScript);

        if(radius == HexFX.DistanceInt(coords, c.origin))
        {
            c.edges.Add(newHexScript);
        }

        hexes.Add(newHexScript);

    }

    private void Update()
    {
        if(initializingHexes)
        {
            int hexesInitialized = 0;

            while(hexesInitialized < hexesPerFrame && frontier.Count > 0)
            {
                //add hex being inspected to the game and the visited list
                int[] current = frontier.Dequeue();
                visited.Add(current);
                Chunk c = GetChunkForCoords(current);
                AddHex(current, c);

                //evaluate each neighbor
                for (int i = 0; i < 6; i++ )
                {
                    int[] neighbor = 
                    { 
                        current[0] + HexFX.hexDirection[i, 0],
                        current[1] + HexFX.hexDirection[i, 1] 
                    };
                    
                    Chunk cc = GetChunkForCoords(neighbor);

                    if 
                    (
                    cc != null && 
                    !HexFX.ContainsCoordinates(visited,neighbor) && 
                    !HexFX.ContainsCoordinates(frontier, neighbor)
                    )
                    {
                        frontier.Enqueue(neighbor);
                    }
                    
                }

                hexesInitialized++;

            }

            if(!(frontier.Count > 0))
            {
                initializingHexes = false;
                chunksToInitialize = new List<Chunk>();
                visited = new List<int[]>();
                frontier = new Queue<int[]>();


                ChunkManager.instance.BuildGraph();
                RepairHexReferences();

                hexesPerFrame = 50;

                GameStateManager.instance.LateInitialize();


            }

        }

    }

    public void InitializeHexes(Chunk c)
    {
        frontier.Enqueue(c.origin);
        chunksToInitialize.Add(c); 
        initializingHexes = true;
    }

    public Hex GetOrigin()
    {
        if(origin == null)
        {
            for (int i = 0; i < hexes.Count; i++)
            {
                if (hexes[i].coordinates[0]==0 && hexes[i].coordinates[1]==0)
                {
                    origin = hexes[i];
                }
            }
        } 

        return origin;

    }

    public void BuildGraph(List<Hex> toBuild)
    {
        foreach(Hex h in toBuild)
        {
            h.SetNeighbors();

        }

    }

    public void RepairHexReferences()
    {
        Hex[] hexesCopy = new Hex[hexes.Count];
        hexes.CopyTo(hexesCopy);

        foreach(Hex h in hexesCopy)
        {
            if(h==null)
            {
                hexes.Remove(h);
                continue;

            } else 
            {
                Hex[] neighborsCopy = new Hex[h.neighbors.Count];
                h.neighbors.CopyTo(neighborsCopy);

                foreach(Hex i in neighborsCopy)
                {
                    if(i==null)
                    {
                        h.neighbors.Remove(i);
                    }

                }

            }

        }

    }

    public Chunk GetChunkForCoords(int[] coords)
    {
        foreach(Chunk c in chunksToInitialize)
        {
            if(HexFX.DistanceInt(coords, c.origin) <= radius)
            {
                return c;
            }

        }

        return null;

    }
    
}
