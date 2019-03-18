using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeHexes : MonoBehaviour 
{
/*

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
                int[] neighbor = 
                {
                current[0] + HexFX.hexDirection[i, 0],
                current[1] + HexFX.hexDirection[i, 1] 
                };
                
                if 
                (
                !HexFX.ContainsCoordinates(visited,neighbor) && 
                !HexFX.ContainsCoordinates(frontier, neighbor)
                )
                {
                    if (HexFX.DistanceInt(origin, neighbor) <= radius)
                    {
                        frontier.Enqueue(neighbor);
                    }
       
                }
                
            }
            
        }


    }
*/
}
    


