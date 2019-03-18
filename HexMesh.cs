using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMesh : MonoBehaviour
{
    [Header("Setup")]
    public Material highlightMaterial;
    public Material flashMaterial;
    public Material waterMaterial;
    public Material landMaterial;

    private Renderer hexRenderer;
    private Material defaultMaterial;
    private Hex parentHex;
    private bool flashing = false;
    private bool hasMouseExited = true;


    // Use this for initialization
    void Start()
    {
        parentHex = this.transform.parent.GetComponent<Hex>();

        hexRenderer = this.GetComponent<Renderer>();

        defaultMaterial = hexRenderer.material;
    }

    public void OnMouseOver()
    {
        
        if (hasMouseExited)
        {
            //pathing selection highlights

                if (parentHex.water && !flashing)
                {
                    hexRenderer.material = highlightMaterial;
                }
                else if (!parentHex.water && !flashing)
                {
                    //hexRenderer.material = flashMaterial;
                }
        }

    }

    public void OnMouseDown()
    {
        if (parentHex.water)
        {
            hexRenderer.material = flashMaterial;
        }
        else if (!parentHex.water)
        {
        }

        flashing = true;

    }

    public void OnMouseExit()
    {
        if (parentHex.water)
        {
        hexRenderer.material = waterMaterial;
        flashing = false;
        }
        else if (!parentHex.water)
        {
            //hexRenderer.material = landMaterial;
            //flashing = false;
        }

        hasMouseExited = true;
    }

    public void OnMouseUpAsButton()
    {
    
        if (parentHex.water)
        {
            hexRenderer.material = waterMaterial;
        }
        else if (!parentHex.water)
        {
            hexRenderer.material = defaultMaterial;
        }

        parentHex.ReceiveClick();
        flashing = false;

        hasMouseExited = false;


    }

    public void SetWater()
    {
        defaultMaterial = waterMaterial;
        this.GetComponent<Renderer>().material = waterMaterial;
    }
}
