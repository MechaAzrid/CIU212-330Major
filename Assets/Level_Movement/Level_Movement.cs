using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Movement : MonoBehaviour
{
    public GameObject player;
    public float speed;

    public Vector3 pointer_position = new Vector3();

    private Vector3 pointer_world_position = new Vector3();
    private Camera c;
    private Event e;


    // Use this for initialization
    void Start ()
    {
        c = Camera.main;

        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void OnGUI()
    {
        e = Event.current;
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            pointer_position.x = e.mousePosition.x;
            pointer_position.y = c.pixelHeight - e.mousePosition.y;
            pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
        }
    }

    // Update is called once per frame
    void Update ()
    {
        float step = speed * Time.deltaTime;

        Vector3 cam_position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = cam_position;

        // Mouse Input
        if (Input.GetButton("Fire1"))
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, pointer_world_position, step);
        }

        // Touch Input

        // When the screen is tapped
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            pointer_position.x = Input.GetTouch(0).position.x;
            pointer_position.y = Input.GetTouch(0).position.y;
            pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));

            player.transform.position = Vector3.MoveTowards(player.transform.position, pointer_world_position, step);
        }

        // When the screen is detected a finger movement
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            pointer_position.x = Input.GetTouch(0).position.x;
            pointer_position.y = Input.GetTouch(0).position.y;
            pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
        }

        // When the screen tap is lifted
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {

        }

    }
}
