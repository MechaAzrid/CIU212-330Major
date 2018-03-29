using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracing_Final : MonoBehaviour
{
    [Header("Object used in line construction")]
    public GameObject input_node;

    [Header("Array for the tracing templates")]
    public GameObject[] tracers;

    [Header("Items below do not need to be touched")]

    [Header("Total number of nodes in scene")]
    public int total_nodes = 0;

    [Header("Current pointer position")]
    public Vector3 pointer_position = new Vector3();

    [Header("Node settings for tracing")]
    public GameObject current_input_node;
    public Input_Node input_node_script;
    public GameObject current_output_node;

    [Header("Total numbers form counters in templates")]
    public int total_min_count;
    public int total_max_count;
    public int total_node_count = 0;

    [Header("Number of correct templates")]
    public int correctnumber = 0;

    private Vector3 pointer_world_position = new Vector3();
    private Camera c;
    private Event e;
    private bool clicked = false;
    private float distance;

    // Use this for initialization
    void Start()
    {
        c = Camera.main;

        for (int i = 0; i < tracers.Length; i++)
        {
            Counter counter = tracers[i].GetComponent<Counter>();
            total_min_count = total_min_count + counter.min_count;
            total_max_count = total_max_count + counter.max_count;
        }
    }

    // For detecting where the mouse is
    void OnGUI()
    {
        e = Event.current;

        pointer_position.x = e.mousePosition.x;
        pointer_position.y = c.pixelHeight - e.mousePosition.y;

        pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
    }

    void Update()
    {
        // Mouse Input

        // When the left click is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            clicked = true;
            Node_Start();
        }
        // When the Left Click is lifted
        if (Input.GetButtonUp("Fire1"))
        {
            clicked = false;
            current_output_node = Instantiate(input_node, pointer_world_position, transform.rotation) as GameObject;
            input_node_script.lr.SetPosition(1, current_output_node.transform.position);
            current_input_node = current_output_node;
            current_output_node = null;
            input_node_script = current_input_node.GetComponent<Input_Node>();
            input_node_script.lr.SetPosition(1, current_output_node.transform.position);
            current_input_node = null;
            total_nodes++;
        }

        // Touch Input

        // When the screen is tapped
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            pointer_position.x = Input.GetTouch(0).position.x;
            pointer_position.y = Input.GetTouch(0).position.y;
            pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
            clicked = true;
            Node_Start();
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
            current_output_node = Instantiate(input_node, pointer_world_position, transform.rotation) as GameObject;
            input_node_script.lr.SetPosition(1, current_output_node.transform.position);
            current_input_node = current_output_node;
            current_output_node = null;
            input_node_script = current_input_node.GetComponent<Input_Node>();
            input_node_script.lr.SetPosition(1, current_output_node.transform.position);
            current_input_node = null;
            total_nodes++;
        }


        if (current_input_node != null)
        {
            distance = Vector3.Distance(pointer_world_position, current_input_node.transform.position);
        }

        if (distance > 0.5f && clicked)
        {
            current_output_node = Instantiate(input_node, pointer_world_position, transform.rotation) as GameObject;
            input_node_script.lr.SetPosition(1, current_output_node.transform.position);
            Node();
        }
    }

    void Node_Start()
    {
        current_input_node = Instantiate(input_node, pointer_world_position, transform.rotation) as GameObject;
        input_node_script = current_input_node.GetComponent<Input_Node>();
        total_nodes++;
    }

    void Node()
    {
        current_input_node = current_output_node;
        current_output_node = null;
        input_node_script = current_input_node.GetComponent<Input_Node>();
        total_nodes++;
    }

    public void Count()
    {

        for (int i = 0; i < tracers.Length; i++)
        {
            Debug.Log("Loop: " + i);
            Counter counter = tracers[i].GetComponent<Counter>();
            counter.Count();
            if (counter.iscorrect)
            {
                correctnumber++;
            }
        }

        GameObject checker = GameObject.Find("Checker");
        Image checker_image = checker.GetComponent<Image>();

        if (correctnumber == tracers.Length && total_nodes < total_max_count)
        {
            checker_image.color = Color.green;
        }
        else
        {
            checker_image.color = Color.red;
        }
    }
}
