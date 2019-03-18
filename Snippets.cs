using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snippets : MonoBehaviour {

	/*


    public void BuildHexGraph(List<Hex> toBuild)
    {
        //after all hexes are added, build the graph by giving each hex references to its neighbors
        int aaa = 0;
        int bbb = 0;
        int ccc = 0;

        //step through all hexes
        for (int i = 0; i < toBuild.Count; i++)
        {
            Hex current = toBuild[i];

            List<int[]> possibleNeighbors = new List<int[]>();
            //search for each neighboring hex and add a reference

            //compile list of neighboring coordinates
            for (int j = 0; j < 6; j++)
            {
                int[] neighbor = { current.coordinates[0] + HexFX.hexDirection[j,0], current.coordinates[1] + HexFX.hexDirection[j,1] };
                possibleNeighbors.Add(neighbor);
            }

            //remove repeats
            foreach(Hex h in current.neighbors)
            {
                if(HexFX.ContainsCoordinates(possibleNeighbors, h.coordinates))
                {
                    aaa++;
                    possibleNeighbors.Remove(h.coordinates);

                }

            }


            //search all hexes again for the neighbor references
            int k = 0;
            int count = 0;
            int max = possibleNeighbors.Count;
            while(count < max && k < toBuild.Count)
            {
                Hex searching = toBuild[k];

                if(HexFX.ContainsCoordinates(possibleNeighbors, searching.coordinates))
                {
                    bbb++;
                    HexFX.RemoveCoordinates(possibleNeighbors, searching.coordinates);
                    current.neighbors.Add(searching);
                    count++;
                }

                k++;

            }

            if(possibleNeighbors.Count > 0)
            {
                k = 0;
                count = 0;
                max = possibleNeighbors.Count;

                while(count < max && k < hexes.Count)
                {
                    Hex searching = hexes[k];

                    if(HexFX.ContainsCoordinates(possibleNeighbors, searching.coordinates))
                    {
                        ccc++;
                        HexFX.RemoveCoordinates(possibleNeighbors, searching.coordinates);
                        current.neighbors.Add(searching);
                        count++;
                    }

                    k++;

                }

            }

        }

    }
public class InitializeHexes : MonoBehaviour 
{

    public static void initializeHexes(int radius, int[] origin, Chunk c)
    {
        //queue to explore and visited list
        Queue<int[]> frontier = new Queue<int[]>();
        List<int[]> visited = new List<int[]>();

        //push origin
        frontier.Enqueue(origin);
        
        //search loop
        while (frontier.Count > 0)
        {
            //add hex being inspected to the game and the visited list
            int[] current = frontier.Dequeue();
            visited.Add(current);
            HexManager.instance.AddHex(current, c);

            //evaluate each neighbor
            for (int i = 0; i < 6; i++ )
            {
                int[] neighbor = { current[0] + HexFX.hexDirection[i, 0], current[1] + HexFX.hexDirection[i, 1] };
                
                if (!HexFX.ContainsCoordinates(visited,neighbor) && !HexFX.ContainsCoordinates(frontier, neighbor))
                {
                    //can i just make the graph here?
                    if (HexFX.DistanceInt(origin, neighbor) <= radius)
                    {
                        frontier.Enqueue(neighbor);
                    }
       
                }
                
            }
            
        }


    }

}
    




// other version lmao

        InitializeHexes.initializeHexes(radius, originCoordinates);

        for(int i = 0; i < 6; i++)
        {
            int j = ((i - 1) + 6 ) % 6;
            int[] coords = originCoordinates;

            coords[0] = HexFX.hexDirection[i,0] * radius;
            coords[1] = HexFX.hexDirection[i,1] * radius;
            coords[0] += HexFX.hexDirection[j,0] * (radius + 1);
            coords[1] += HexFX.hexDirection[j,1] * (radius + 1);

            InitializeHexes.initializeHexes(radius, coords);
        }
        //after all hexes are added, build the graph by giving each hex references to its neighbors

        //step through all hexes
        for (int i = 0; i < HexManager.instance.hexes.Count; i++)
        {
            Hex current = HexManager.instance.hexes[i];

            List<int[]> possibleNeighbors = new List<int[]>();
            //search for each neighboring hex and add a reference

            //compile list of neighboring coordinates
            for (int j = 0; j < 6; j++)
            {
                int[] neighbor = { current.coordinates[0] + HexFX.hexDirection[j,0], current.coordinates[1] + HexFX.hexDirection[j,1] };
                possibleNeighbors.Add(neighbor);
            }

            //search all hexes again for the neighbor references
            for (int j = 0; j < HexManager.instance.hexes.Count; j++)
            {
                Hex searching = HexManager.instance.hexes[j];
                                          
                if(HexFX.ContainsCoordinates(possibleNeighbors, searching.coordinates))
                {
                    current.neighbors.Add(searching);
                }
            }

        }

        //now find shortest path

        Hex bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        foreach (Hex potentialTarget in start.neighbors)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        //Debug.Log(string.Format("current: {0},{1}", bestTarget.coordinates[0], bestTarget.coordinates[1]));


    //check for destination
                if (neighbor.coordinates[0] == start.coordinates[0] && neighbor.coordinates[1] == start.coordinates[1])
                {
                    //you're done!
                    //return path;
                }



    //record shortest distance if you've reached the endto avoid making too big a graph
                if (neighbor.coordinates[0] == start.coordinates[0] && neighbor.coordinates[1] == start.coordinates[1])
                {
                    if(distance[current] < shortestDistance)
                    {
                        shortestDistance = distance[current];
                    }
                }





        //find the end
        for (int i = 0; i < path.Count; i++)
        {
            current = path[i];

            if (current.coordinates[0] == end.coordinates[0] && current.coordinates[1] == end.coordinates[1])
            {
                frontier.Enqueue(current);

                distance.Add(current, 0);


                break;

            }
        }

public class Ship : Unit {

    //ship this is attached to
    public GameObject shipGO;

    //this ships plans
    public Queue<Action> actionQueue = new Queue<Action>();
    public Queue<Hex> path = null;

    //state booleans
    public bool moving;
    public bool idle;

    //movement
    public Hex target = null;
    public Hex destination = null;
    public Transform waypoint;
    private float waypointTolerance = 0.15f;
    public float speed;
    private Vector3 velocity = Vector3.zero; 
    
    //member functions

    public void SetPath(Queue<Hex> newPath)
    {
        path = newPath;
        if(newPath.Count > 0)
        {
            moving = true;

        } else {

            moving = false;  
        }
        target = null;
    }

    public void QueueAction(Action a)
    {
      //  path = null;
        actionQueue.Enqueue(a);
    }

    private void Start()
    {
        shipGO = this.gameObject;
        waypoint = shipGO.transform.GetChild(0);

        speed = 0.11f;
        idle = true;
        moving = false;
    }
    
    public void UpdateMovement()
    {
        if (target == null && path.Count > 0)
        {
           //get the next target
            target = path.Dequeue();

        }
    }

    public void Move()
    {
        if(moving)// && path != null)
        {
            //see if youve arrived
            float d = Vector3.Distance
            (
            waypoint.position,
            target.waypoint.position
            );

            if (d < waypointTolerance)
            {
                position = target;
                
                if (path.Count > 0 && !TurnManager.instance.danger) 
                {
                    target = path.Dequeue();

                } else 
                {
                    target = null;
                    moving = false;
                    idle = true;

                }
            
            }

            if(target != null)
            {
              //lerp to the target 
              Vector3 sd = Vector3.SmoothDamp
              (
              shipGO.transform.position,
              target.waypoint.position,
              ref velocity,
              speed
              );

//            transform.rotation = Quaternion.LookRotation(sd);

              shipGO.transform.position = sd;

            }

        }

    }
    
    public void Act()
    {
        if(idle && actionQueue.Count > 0)
        {
            Action a = actionQueue.Dequeue();
            a.Execute(this);
           //else if fortaction
           //a.Execute(this, a.fort);
        }
    }

    public void ClearQueue()
    {
        actionQueue = new Queue<Action>();

    }
}
    */
}
