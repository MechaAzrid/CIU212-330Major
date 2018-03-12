using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Controls : MonoBehaviour
{
    public GameObject object_test;


    private Vector3 touch_world_position = new Vector3();
    public Camera c;
    public Vector3 touch_position = new Vector3();

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    void Update ()
    {
        // When the screen is tapped
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touch_position.x = Input.GetTouch(0).position.x;
            touch_position.y = c.pixelHeight - Input.GetTouch(0).position.y;
            touch_world_position = c.ScreenToWorldPoint(new Vector3(touch_position.x, touch_position.y, 10.0f));

            Instantiate(object_test, touch_world_position, transform.rotation);
        }

        // When the screen is held
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            touch_position.x = Input.GetTouch(0).position.x;
            touch_position.y = c.pixelHeight - Input.GetTouch(0).position.y;
            touch_world_position = c.ScreenToWorldPoint(new Vector3(touch_position.x, touch_position.y, 10.0f));
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touch_position.x = Input.GetTouch(0).deltaPosition.x;
            touch_position.y = c.pixelHeight - Input.GetTouch(0).deltaPosition.y;
            touch_world_position = c.ScreenToWorldPoint(new Vector3(touch_position.x, touch_position.y, 10.0f));
        }
    }
}
