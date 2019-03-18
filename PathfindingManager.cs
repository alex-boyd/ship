using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour 
{
    public static PathfindingManager instance = null;

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

    public Queue<Hex> PathBetween(Hex start, Hex end)
    {
        Hex current;
        Queue<Hex> path = new Queue<Hex>();
        Queue<Hex> frontier = new Queue<Hex>();
        List<Hex> visited = new List<Hex>();
        Dictionary<Hex, int> distance = new Dictionary<Hex, int>();

        frontier.Enqueue(end);
        distance.Add(end, 0);


        //build distance graph

        int shortestDistance = int.MaxValue;

        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();
            visited.Add(current);

            //check each neighbor and give it a distance value
            for (int i = 0; i < current.neighbors.Count; i++)
            {
                Hex neighbor = current.neighbors[i];

                //make them n and s so its less than 80
                
                //record shortest distance if you've reached the end
                if 
                (
                neighbor.coordinates[0] == start.coordinates[0] &&
                neighbor.coordinates[1] == start.coordinates[1]
                )
                {
                    if(distance[current] < shortestDistance)
                    {
                        shortestDistance = distance[current];
                    }
                }

                //enqueue and add to visited and distance if this neighbors new
                if 
                (
                neighbor.water && 
                distance[current] <= shortestDistance &&
                !frontier.Contains(neighbor) &&
                !visited.Contains(neighbor)
                ) 
                {
                    frontier.Enqueue(neighbor);

                   //visited.Add(neighbor);

                    if (!distance.ContainsKey(neighbor))
                    {
                        distance.Add(neighbor, distance[current] + 1);
                    }
                        
                }

            }

        }

        //find a path through distance graph
        current = start;
        int nextDistance = distance[current] - 1;

        while (current != end)//(nextDistance > 0)
        {
            nextDistance = distance[current] - 1;

            List<Hex> options = new List<Hex>();

            foreach (Hex potentialTarget in current.neighbors)
            {
                if(distance.ContainsKey(potentialTarget))
                {
                    if (distance[potentialTarget] == nextDistance)
                    {
                        options.Add(potentialTarget);
                    }
                }

            }

            //pick an option based on heuristics we can add later
            //path.Enqueue(options[(int)Mathf.Floor(Random.value *options.Count
            if(options.Count <= 0)
            {
                Debug.Log("crapped the bed!");
                return path;

            } else 
            {
                Hex chosen = options[0];
                path.Enqueue(chosen);
                current = chosen;
            }


        }

        return path;
    }
    
    public Hex BestWaterNeighbor(Hex l, Ship s)
    {
        int minDistance = 2147483647; 

        Hex bestPick = null;

        foreach (Hex h in l.neighbors)
        {
           if(h.water)
           {
               Hex current = s.position;

               int d = PathBetween(current, h).Count;

               if (d < minDistance)
               {
                   minDistance = d; 
                   bestPick = h; 
               }
           }

        }

        return bestPick;

    }
/*
    public MoveAction WrapHex(Hex h)
    {
        MoveAction ma = new MoveAction();
        ma.destination = h;
        return ma;
    }
    */
    /*
    private void clearData()
    {
        frontier = new Queue<Hex>();
        visited = new List<Hex>();
        distance = new Dictionary<Hex, int>();

    }

*/
}
/*



 */
