using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combined_Draw_Touch : MonoBehaviour
{
    public GameObject input_node;
    public GameObject output_node;

    public int total_nodes = 0;

    private Vector3 pointer_world_position = new Vector3();
    public Camera c;
    public Vector3 pointer_position = new Vector3();
    private Event e;

    public GameObject current_input_node;
    public Input_Node input_node_script;
    public GameObject current_output_node;

    public GameObject tracer;

    private bool clicked = false;

    private float distance;

    // Use this for initialization
    void Start()
    {
        c = Camera.main;
    }

    // For detecting where the mouse is
    void OnGUI()
    {
        e = Event.current;

        pointer_position.x = e.mousePosition.x;
        pointer_position.y = c.pixelHeight - e.mousePosition.y;

        pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
    }

    void Node()
    {
        current_input_node = Instantiate(input_node, pointer_world_position, transform.rotation) as GameObject;
        input_node_script = current_input_node.GetComponent<Input_Node>();
        total_nodes++;
    }

    //void Spawn_Tracer()
    //{
    //    Instantiate(tracer, pointer_world_position, transform.rotation);
    //}

    void Update()
    {
        // Mouse Input

        // When the left click is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Button Hit");
            clicked = true;
            Node();
            //Spawn_Tracer();
        }
        // When the Left Click is lifted
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("Button Lifted");
            clicked = false;
            current_output_node = Instantiate(output_node, pointer_world_position, transform.rotation) as GameObject;
            input_node_script.lr.SetPosition(1, current_output_node.transform.position);
        }

        // Touch Input

        // When the screen is tapped
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            pointer_position.x = Input.GetTouch(0).position.x;
            pointer_position.y = Input.GetTouch(0).position.y;
            pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
            clicked = true;
            Node();
            //Spawn_Tracer();
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
            pointer_position.x = Input.GetTouch(0).position.x;
            pointer_position.y = Input.GetTouch(0).position.y;
            pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
            clicked = false;
            current_output_node = Instantiate(output_node, pointer_world_position, transform.rotation) as GameObject;
            input_node_script.lr.SetPosition(1, current_output_node.transform.position);
        }


        if(current_input_node != null)
        {
            distance = Vector3.Distance(pointer_world_position, current_input_node.transform.position);
        }

        if (distance > 0.5f && clicked)
        {
            current_output_node = Instantiate(output_node, pointer_world_position, transform.rotation) as GameObject;
            input_node_script.lr.SetPosition(1, current_output_node.transform.position);
            Node();
        }
    }
}
