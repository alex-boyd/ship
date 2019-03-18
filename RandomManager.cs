using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager : MonoBehaviour 
{
    public static RandomManager instance = null;
 
    public int seed;

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
        seed = (int)System.DateTime.Now.Ticks; 
        Random.InitState(seed);

    }

    public void RandomInt()
    {

    }

}
