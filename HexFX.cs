using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexFX : MonoBehaviour
{

    static float root3over2 = Mathf.Sqrt(3) / 2;

    public static int[,] hexDirection = 
    { 
        { 0, 1 }, 
        { 1, 0 }, 
        { 1, -1 }, 
        { 0, -1 }, 
        { -1, 0 }, 
        { -1, 1 } 
    };

    public static float[,] hexCartesian =
    {

        {0, 1},
        {root3over2, 0.5f},
        {root3over2, -0.5f},
        {0, -1},
        {-root3over2, -0.5f},
        {-root3over2, 0.5f},
    };

    public static Vector3 HexCoordChange(int direction)
    {
        Vector3 d = new Vector3();

        direction = direction % 6;

        d.x = hexDirection[direction, 0];
        d.y = hexDirection[direction, 1];

        d.z = -(d.x + d.y);

        return d;
    }

    public static Vector3 DirToCartesian(int direction)
    {
        Vector3 d = new Vector3();

        direction = direction % 6;

        d.x = hexCartesian[direction, 0];
        d.y = hexCartesian[direction, 1];

        d.z = 0;

        d = d.normalized;

        return d;
    }

    public static int[] ZStrip(Vector3 c)
    {
        int[] a = { (int)c.x, (int)c.y };
        return a;
    }

    public static int Distance(Vector3 ca, Vector3 cb)
    {
        return (int)Mathf.Max(
            (int)cb.x - (int)ca.x,
            (int)cb.y - (int)ca.y,
            (int)cb.z - (int)ca.z
        );
    }

    public static int DistanceInt(int[] a, int[] b)
    {
        int az = -(a[0] + a[1]);
        int bz = -(b[0] + b[1]);

        return Mathf.Max
        (
        Mathf.Abs(b[0] - a[0]),
        Mathf.Abs(b[1] - a[1]),
        Mathf.Abs(bz - az)
        );
    }

    public static Vector3 HexCoordsToCartesian(int[] coords)
    {
        Vector3 displacement = new Vector3(0,0,0);

        displacement.x = coords[0] * HexFX.hexCartesian[1,0];
        displacement.z = (coords[0] * HexFX.hexCartesian[1,1]) + coords[1];

        return displacement;
        
    }

    //functions to see if a container has a certain hex in it

    public static bool CoordEquals(int[] x, int[] y)
    {

        return (x[0] == y[0] && x[1] == y[1]);

    }


    public static bool ContainsCoordinates(List<int[]> a, int[] b)
    {

        foreach (int[] item in a)
        {
            if (CoordEquals(item, b))
            {
                return true;
            }

        }

        return false;
    }

    public static bool ContainsCoordinates(Queue<int[]> a, int[] b)
    {

        foreach (int[] item in a)
        {
            if (CoordEquals(item, b))
            {
                return true;
            }

        }

        return false;
    }

    public static bool ContainsCoordinates(List<Hex> a, int[] b)
    {

        foreach (Hex item in a)
        {
            if (CoordEquals(item.coordinates, b))
            {
                return true;
            }

        }

        return false;
    }

    public static bool ContainsCoordinates(Queue<Hex> a, int[] b)
    {

        foreach (Hex item in a)
        {
            if (CoordEquals(item.coordinates, b))
            {
                return true;
            }

        }

        return false;
    }

    public static void RemoveCoordinates(List<int[]> a, int[] b)
    {
        for(int i = 0; i < a.Count; i++)
        {
            if(CoordEquals(a[i],b))
            {
               a.RemoveAt(i); 
               return;
            }
        }

    }



}

