using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Prototype : MonoBehaviour
{
    public Vector3 pointer_world_location = new Vector3();

    public bool dragging;

    public LineRenderer obstacle_linerenderer;

    private Vector3 pointer_position = new Vector3();
    private Camera c;
    private Event e;

    private Ray ray;
    private RaycastHit hit;

    // Use this for initialization
    void Start ()
    {
        c = Camera.main;

        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    // For detecting where the mouse is
    void OnGUI()
    {
        e = Event.current;
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            pointer_position.x = e.mousePosition.x;
            pointer_position.y = c.pixelHeight - e.mousePosition.y;
            pointer_world_location = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
            
        }
    }

    // Update is called once per frame
    void Update ()
    {
        // Mouse Input

        // When the mouse is clicked
        if (Input.GetButtonDown("Fire1"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Clicked();
        }

        // When the mouse is held
        if (Input.GetButton("Fire1") && dragging)
        {
            Drag();
        }

        // when the mouse is lifted
        if (Input.GetButtonUp("Fire1") && dragging)
        {
            Lifted();
        }
    }

    void Clicked()
    {
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.gameObject.tag == "obstacle_start")
            {
                obstacle_linerenderer = hit.transform.gameObject.GetComponent<LineRenderer>();
                dragging = true;
            }
        }
    }

    void Drag()
    {
        obstacle_linerenderer.SetPosition(1, pointer_world_location);
    }

    void Lifted()
    {
        dragging = false;
        obstacle_linerenderer.SetPosition(1, obstacle_linerenderer.GetPosition(0));
    }

    public void Change_Dot (LineRenderer lr)
    {
        obstacle_linerenderer = lr;
    }
}
