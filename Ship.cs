using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Unit 
{
    //ship this is attached to
    public GameObject shipGO;

    //this ships plans
    public Queue<Action> actionQueue = new Queue<Action>();

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

    public float smoothSpeed = 500f;

    //public int chunkCounter = 0;

    
    //member functions

    public void SetTarget(Hex t)
    {
        if(t != null)
        {
            moving = true;
            target = t;
            velocity = Vector3.zero;

            if(t.chunk != position.chunk)
            {
                if(HexManager.instance.initializingHexes)
                {
                    ChunkManager.instance.QueueChunkUpdate();

                } else
                {
                    ChunkManager.instance.ChunkUpdate(t.chunk);
                }
            }

            position = target;
            
        }

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
 /*   
    public void UpdateMovement()
    {
        if (target == null && path.Count > 0)
        {
           //get the next target
            target = path.Dequeue();

        }
    }
*/
    public void Move()
    {
        if(target != null)
        {
            //see if youve arrived
            float d = Vector3.Distance
            (
            waypoint.position,
            target.waypoint.position
            );

            if (d < waypointTolerance)
            {
                
                if (actionQueue.Count > 0 && !TurnManager.instance.danger) 
                {
                    var a = actionQueue.Dequeue();
                    a.Execute(this);

                } else 
                {
                    destination = null;
                    target = null;
                    moving = false;
                    idle = true;

                    
                    return;
                }

            }

            //lerp to the target 
            Vector3 sd = Vector3.SmoothDamp
            (
            shipGO.transform.position,
            target.waypoint.position,
            ref velocity,
            speed
            );

            Quaternion heading = Quaternion.LookRotation
            (
            sd - shipGO.transform.position
            );
            
            transform.position = sd;

            transform.rotation = Quaternion.RotateTowards
            (
            transform.rotation, 
            heading, 
            smoothSpeed * Time.deltaTime
            );

        } else if(actionQueue.Count > 0)
        {
            var a = actionQueue.Peek();
            
            var b = a as FortAction;

            if (b != null)
            {
                var c = actionQueue.Dequeue();
                c.Execute(this);
            }

        }

    }

    public void Act()
    {
        if(idle && actionQueue.Count > 0 )
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
