using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MSG_Tracing_Final : MonoBehaviour
{
    [Header("-Place on the main Camera-")]

    [Header("Items below need to be set in inspector")]

    public GameObject continue_button;

    [Header("Object used in line construction")]
    public GameObject input_node;

    [Header("Arrays for the tracing templates")]
    public List<int> word_length = new List<int>();
    public List<GameObject> tracers = new List<GameObject>();
    public List<GameObject> temp_tracers = new List<GameObject>();

    [Header("Obstacle Tracers")]
    public List<GameObject> obstacle_tracers = new List<GameObject>();
    public List<GameObject> temp_obstacle_tracers = new List<GameObject>();

    [Header("Variable(s) that can be set but otherwise will be done automatically")]

    [Header("Whether the drawing is active")]
    public bool isactive = false;

    [Header("Items below do not need to be touched")]

    [Header("Total number of nodes in scene")]
    public int total_nodes = 0;

    [Header("Node list")]
    public List<GameObject> node_list = new List<GameObject>();

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

    private Vector3 pointer_world_position_temp_location = new Vector3();
    private Vector3 pointer_world_location;
    private Camera c;
    private Event e;
    private bool clicked = false;
    private float distance;
    private bool locked = false;

    public Image obstacle_checker;
    public Image tracing_checker;

    // Use this for initialization
    void Start()
    {
        c = Camera.main;

        Screen.orientation = ScreenOrientation.LandscapeLeft;

        //if(tracers.Count > 0)
        //{
        //    Begin();
        //}
    }

    //public void Begin ()
    //{
    //    for (int i = 0; i < tracers.Count; i++)
    //    {
    //        Counter counter = tracers[i].GetComponent<Counter>();
    //        total_min_count = total_min_count + counter.min_count;
    //        total_max_count = total_max_count + counter.max_count;
    //    }
    //}

    public void Tracing_Start (int sticker_number)
    {
        int past_templates = 0;

        for (int i = 0; i < word_length.Count; i++)
        {
            if(sticker_number == i)
            {
                for (int j = 0; j < word_length[i]; j++)
                {
                    temp_tracers.Add(tracers[j + past_templates]);
                }
            }
            else
            {
                past_templates += word_length[i];
            }
        }

        for (int i = 0; i < temp_tracers.Count; i++)
        {
            Counter counter = temp_tracers[i].GetComponent<Counter>();
            total_min_count = total_min_count + counter.min_count;
            total_max_count = total_max_count + counter.max_count;
        }
    }

    public void Obstacle_Start (int obstacle_number)
    {
        temp_obstacle_tracers.Add(obstacle_tracers[obstacle_number]);

        Counter counter = temp_obstacle_tracers[0].GetComponent<Counter>();
        total_min_count = total_min_count + counter.min_count;
        total_max_count = total_max_count + counter.max_count;
    }

    // For detecting where the mouse is
    void OnGUI()
    {
        e = Event.current;
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            pointer_position.x = e.mousePosition.x;
            pointer_position.y = c.pixelHeight - e.mousePosition.y;
            pointer_world_position_temp_location = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
            pointer_world_location = pointer_world_position_temp_location;
        }
    }

    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Tracing ||
            GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Obstacle)
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
                Lifted();
            }

            // Touch Input

            // When the screen is tapped
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                pointer_position.x = Input.GetTouch(0).position.x;
                pointer_position.y = Input.GetTouch(0).position.y;
                pointer_world_position_temp_location = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
                pointer_world_location = pointer_world_position_temp_location;
                clicked = true;
                Node_Start();
            }
            //// When the screen is detected a finger movement
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                pointer_position.x = Input.GetTouch(0).position.x;
                pointer_position.y = Input.GetTouch(0).position.y;
                pointer_world_position_temp_location = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
                pointer_world_location = pointer_world_position_temp_location;
            }
            // When the screen tap is lifted
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Lifted();
            }

            if (current_input_node != null)
            {
                distance = Vector3.Distance(pointer_world_location, current_input_node.transform.position);
            }

            if (distance > 0.5f && clicked)
            {
                current_output_node = Instantiate(input_node, pointer_world_location, transform.rotation) as GameObject;
                node_list.Add(current_output_node);
                input_node_script.lr.SetPosition(1, current_output_node.transform.position);
                Node();
            }
        }
    }

    void Lifted()
    {
        clicked = false;
        current_output_node = Instantiate(input_node, pointer_world_location, transform.rotation) as GameObject;
        node_list.Add(current_output_node);
        input_node_script.lr.SetPosition(1, current_output_node.transform.position);
        current_input_node = current_output_node;
        input_node_script = current_input_node.GetComponent<Input_Node>();
        input_node_script.lr.SetPosition(1, current_input_node.transform.position);
        current_input_node = null;
        current_output_node = null;
        total_nodes++;
    }

    void Node_Start()
    {
        current_input_node = Instantiate(input_node, pointer_world_location, transform.rotation) as GameObject;
        node_list.Add(current_input_node);
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
        if (!locked)
        {
            locked = true;

            if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Obstacle)
            {
                Counter counter = temp_obstacle_tracers[0].GetComponent<Counter>();
                counter.Count();

                if (counter.iscorrect && total_nodes < total_max_count)
                {
                    tracing_checker.color = Color.green;
                    continue_button.SetActive(true);
                }
                else
                {
                    tracing_checker.color = Color.red;
                }
            }

            if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Tracing)
            {
                for (int i = 0; i < temp_tracers.Count; i++)
                {
                    Counter counter = temp_tracers[i].GetComponent<Counter>();
                    counter.Count();
                    if (counter.iscorrect)
                    {
                        correctnumber++;
                    }
                }

                if (correctnumber == temp_tracers.Count && total_nodes < total_max_count)
                {
                    tracing_checker.color = Color.green;
                    continue_button.SetActive(true);
                }
                else
                {
                    tracing_checker.color = Color.red;
                }
            }
        }
    }

    public void Reset_Letters ()
    {
        total_nodes = 0;
        total_node_count = 0;
        correctnumber = 0;
        locked = false;

        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Obstacle)
        {
            Counter counter = temp_obstacle_tracers[0].GetComponent<Counter>();
            counter.node_count = 0;
            counter.iscorrect = false;

            obstacle_checker.color = Color.white;
        }

        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Tracing)
        {
            for (int i = 0; i < temp_tracers.Count; i++)
            {
                Counter counter = temp_tracers[i].GetComponent<Counter>();
                counter.node_count = 0;
                counter.iscorrect = false;
            }

            tracing_checker.color = Color.white;
        }

        for (int i = 0; i < node_list.Count; i++)
        {
            Destroy(node_list[i]);
        }
        node_list.Clear();
    }

    public void Return_Button ()
    {
        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Obstacle)
        {

        }

        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Tracing)
        {

        }


    }

    public void Continue_Button ()
    {
        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Obstacle)
        {

        }

        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Tracing)
        {

        }



        // Temp
        SceneManager.LoadScene("MSG_Start");
        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Default_Active();
    }
}
