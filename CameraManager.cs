using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance = null;

    public Transform focus = null;

    public Transform cameraParent = null;

    //follow
    public Vector3 offset;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

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

    private void LateUpdate()
    {
        Vector3 target = focus.position + offset;

        cameraParent.position = Vector3.SmoothDamp
        (
        cameraParent.position,
        target,
        ref velocity,
        smoothTime
        );
    }

    public void Initialize()
    {
        focus = new GameObject().transform;
        SetFocus(ShipManager.instance.currentShip.transform);
        offset = cameraParent.position - focus.position;
    }

    public void SetFocus(Transform f)
    {
        focus = f;
    }

    public Transform GetCamera()
    {
        if(cameraParent == null)
        {
            //sorry i just wanted it to fit in 80 chars q.q
            Transform cp; 
            cp = GameObject.FindGameObjectWithTag("CameraParent").transform;
            cameraParent = cp; 
        }

        return cameraParent;

    }

} 
