using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum obstacle_states { none, start, moveable, immoveable, end }

public class Obstacle_Dot : MonoBehaviour
{
    public obstacle_states obstacle_state;

    private LineRenderer lr;

    private Obstacle_Prototype op;
    private float distance;

    private bool locked;

	// Use this for initialization
	void Start ()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);

        op = GameObject.Find("Main Camera").GetComponent<Obstacle_Prototype>();
    }


	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector3.Distance(transform.position, op.pointer_world_location);

        if(distance < 0.2f && op.dragging && !locked)
        {
            locked = true;
            op.Change_Dot (lr);
        }
	}
}
