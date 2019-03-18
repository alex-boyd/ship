using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour 
{
    public static TerrainManager instance = null;

    public Vector3 seed;

    public float seedScale;

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
        seed = new Vector3(Random.value, Random.value); 
        seedScale = 1000;
        seedScale *= Random.value;
        seed *= seedScale;
    }


    public void RollTerrain (Hex h)
    {
        float landThreshold = 0.35f;
        float fortThreshold = 0.12f;
        float noiseDensity = 1f;
        float offset = 10000;

        float waterHeightOffset= 1f;
        float landHeightMultiplier = 1.5f;

        int[] origin = {0,0};

        Vector3 newPosition = h.transform.position;

        Vector3 c = HexFX.HexCoordsToCartesian(h.coordinates);
        c *= noiseDensity;
        c.x += offset;
        c.z += offset;
        c += seed;

        //check for origin so the ship starts on water
        if (HexFX.CoordEquals(h.coordinates, origin))
        {
            //water
            h.water = true;
            h.meshScript.SetWater();
            newPosition.y -= waterHeightOffset;
            h.outline.SetActive(true);

            h.transform.position = newPosition;
            return;
        }

        if (Mathf.PerlinNoise(c.x, c.z) < landThreshold)
        {
            //land
            float heightBump = landHeightMultiplier * Random.value;
            newPosition.y += heightBump;
            h.outline.SetActive(false);

            h.transform.position = newPosition;

            if(Mathf.PerlinNoise(c.x,c.z) < fortThreshold)
            {
                //if(//has at least one water neighbor

                FortManager.instance.AddFort(h);


            }

        } else
        {
            //water
            h.water = true;
            h.meshScript.SetWater();
            newPosition.y -= waterHeightOffset;
            h.outline.SetActive(true);

            h.transform.position = newPosition;

        }
         
    }
}
