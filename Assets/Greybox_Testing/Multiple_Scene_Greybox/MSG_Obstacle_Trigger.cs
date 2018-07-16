using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSG_Obstacle_Trigger : MonoBehaviour
{
    public int obstacle_int;

    public bool passed;

    public Renderer[] black_blocks;
    public Renderer[] grey_blocks;

    public GameObject[] bridge;

    public Color red_black;
    public Color red_grey;

    public Color green_black;
    public Color green_grey;

    public GameObject button;

    private bool locked;

    // Use this for initialization
    void Start ()
    {
        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_obstacle_passed[obstacle_int] == true)
        {
            passed = true;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(!passed)
        {
            foreach (GameObject bridge_piece in bridge)
            {
                bridge_piece.SetActive(false);
            }

            foreach (Renderer black_renderer in black_blocks)
            {
                black_renderer.material.color = red_black;
            }

            foreach (Renderer grey_renderer in grey_blocks)
            {
                grey_renderer.material.color = red_grey;
            }

            float distance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            if (distance < 1.3f)
            {
                button.SetActive(true);
            }
            else
            {
                button.SetActive(false);
            }
        }

        if (passed)
        {
            foreach (GameObject bridge_piece in bridge)
            {
                bridge_piece.SetActive(true);
            }

            foreach (Renderer black_renderer in black_blocks)
            {
                black_renderer.material.color = green_black;
            }

            foreach (Renderer grey_renderer in grey_blocks)
            {
                grey_renderer.material.color = green_grey;
            }

            GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_obstacle_passed[obstacle_int] = true;
            button.SetActive(false);

            Save();
        }
    }

    public void Button_Clicked()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        //MSG_Obstacle_Prototype cam_ob = cam.GetComponent<MSG_Obstacle_Prototype>();

        //cam_ob.original_cam_position = cam.transform.position;
        //cam_ob.obstacle_int = obstacle_int;

        MSG_Tracing_Final cam_tr = cam.GetComponent<MSG_Tracing_Final>();

        cam_tr.original_cam_position = cam.transform.position;

        cam.transform.position = new Vector3(-10000, 0, -10);

        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Tracing_Active();
        //cam_ob.obstacles_active(this);

        cam_tr.Obstacle_Start(obstacle_int, this);
    }

    void Save ()
    {
        if(!locked)
        {
            locked = true;
            GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Save_Data();
        }
    }
}
