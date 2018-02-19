using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Script : MonoBehaviour
{
    public Vector3 mouse_position;
    public GameObject test;

    private Vector3 mouse_world_position = new Vector3();
    public Camera c = Camera.main;
    private Vector3 mouse_pos = new Vector3();

	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update ()
    {
        mouse_pos.x = Input.mousePosition.x;
        mouse_pos.y = c.pixelHeight - Input.mousePosition.y;

        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Button Hit");
            mouse_world_position = c.ScreenToWorldPoint(new Vector3(mouse_pos.x, mouse_pos.y, 10.0f));
            Instantiate(test, mouse_world_position, transform.rotation);
        }
    }
}
