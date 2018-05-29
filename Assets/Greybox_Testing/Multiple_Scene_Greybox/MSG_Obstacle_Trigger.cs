using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSG_Obstacle_Trigger : MonoBehaviour
{
    public int obstacle_int;

    public bool passed;

    public Renderer[] black_blocks;
    public Renderer[] grey_blocks;

    public Color red_black;
    public Color red_grey;

    public Color green_black;
    public Color green_grey;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(!passed)
        {
            for (int i = 0; i < black_blocks.Length; i++)
            {
                black_blocks[i].material.color = red_black;
                grey_blocks[i].material.color = red_grey;
            }
        }

        if (passed)
        {
            for (int i = 0; i < black_blocks.Length; i++)
            {
                black_blocks[i].material.color = green_black;
                grey_blocks[i].material.color = green_grey;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(!passed)
        {
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            MSG_Obstacle_Prototype cam_ob = cam.GetComponent<MSG_Obstacle_Prototype>();

            cam_ob.original_cam_position = cam.transform.position;
            cam_ob.obstacle_int = obstacle_int;

            cam.transform.position = new Vector3(10000, 0, -10);

            GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Obstacle_Active();
            cam_ob.obstacles_active();
        }
    }
}
