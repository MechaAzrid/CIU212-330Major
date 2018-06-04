using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSG_Level_Movement : MonoBehaviour
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
            pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, pointer_position.z));
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if(GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Movement)
        {
            float step = speed * Time.deltaTime;

            Vector3 cam_position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 10);
            transform.position = cam_position;

            // Mouse Input
            if (Input.GetButton("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    Vector3 line_check = new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.1f);
                    RaycastHit blockage;
                    if (Physics.Linecast(player.transform.position, line_check, out blockage))
                    {

                    }
                    else
                    {
                        player.transform.position = Vector3.MoveTowards(player.transform.position, hit.point, step);
                    }
                }
            }

            // Touch Input

            // When the screen is tapped
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                pointer_position = Input.GetTouch(0).position;
                pointer_world_position = c.ScreenToWorldPoint(pointer_position);

                Ray ray = Camera.main.ScreenPointToRay(pointer_world_position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, hit.point, step);
                }
            }

            // When the screen is detected a finger movement
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                pointer_position = Input.GetTouch(0).position;
                pointer_world_position = c.ScreenToWorldPoint(pointer_position);

                Ray ray = Camera.main.ScreenPointToRay(pointer_world_position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, hit.point, step);
                }
            }

            // When the screen tap is lifted
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {

            }

        }
    }
}
