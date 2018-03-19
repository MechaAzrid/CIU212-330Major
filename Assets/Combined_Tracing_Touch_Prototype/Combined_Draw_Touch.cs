using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combined_Draw_Touch : MonoBehaviour
{
    public GameObject input_node;
    public GameObject output_node;

    private Vector3 pointer_world_position = new Vector3();
    public Camera c;
    public Vector3 pointer_position = new Vector3();
    private Event e;

    public GameObject current_input_node;
    public Input_Node input_node_script;
    public GameObject current_output_node;

    public GameObject tracer;

    private bool clicked = false;

    // Use this for initialization
    void Start()
    {
        c = Camera.main;
    }

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
    }

    void Spawn_Tracer()
    {
        Instantiate(tracer, pointer_world_position, transform.rotation);
    }

    void Update()
    {
        // Mouse Input
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Button Hit");
            clicked = true;
            Node();
            Spawn_Tracer();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("Button Lifted");
            clicked = false;
            current_output_node = Instantiate(output_node, pointer_world_position, transform.rotation) as GameObject;
            input_node_script.lr.SetPosition(1, current_output_node.transform.position);
        }

        // Touch Input
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            pointer_position.x = Input.GetTouch(0).position.x;
            pointer_position.y = Input.GetTouch(0).position.y;
            pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
            clicked = true;
            Node();
            Spawn_Tracer();
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            pointer_position.x = Input.GetTouch(0).position.x;
            pointer_position.y = Input.GetTouch(0).position.y;
            pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            pointer_position.x = Input.GetTouch(0).position.x;
            pointer_position.y = Input.GetTouch(0).position.y;
            pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
            clicked = false;
            current_output_node = Instantiate(output_node, pointer_world_position, transform.rotation) as GameObject;
            input_node_script.lr.SetPosition(1, current_output_node.transform.position);
        }


        float distance = Vector3.Distance(pointer_world_position, current_input_node.transform.position);

        if (distance > 0.5f && clicked)
        {
            current_output_node = Instantiate(output_node, pointer_world_position, transform.rotation) as GameObject;
            input_node_script.lr.SetPosition(1, current_output_node.transform.position);
            Node();
        }
    }
}
