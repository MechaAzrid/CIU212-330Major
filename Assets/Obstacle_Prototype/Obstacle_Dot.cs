using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Dot : MonoBehaviour
{
    public GameObject ray_position;

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
        distance = Vector3.Distance(transform.position, op.pointer_world_position);

        if(distance < 0.2f && op.dragging && !locked && gameObject.tag != "obstacle_immoveable")
        {
            locked = true;
            op.Change_Dot (gameObject, lr, locked);
        }
	}
}
