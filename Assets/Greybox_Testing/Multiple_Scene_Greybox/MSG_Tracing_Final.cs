﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MSG_Tracing_Final : MonoBehaviour
{
    [Header("-Place on the main Camera-")]

    [Header("Items below need to be set in inspector")]

    public GameObject tracing_continue_button;
    public GameObject tracing_reset_button;

    [Header("Object used in line construction")]
    public GameObject input_node;
    public bool input_dot_active = true;

    [Header("Arrays for the tracing templates")]
    public List<int> word_length = new List<int>();
    public List<GameObject> tracers = new List<GameObject>();
    public List<GameObject> temp_tracers = new List<GameObject>();

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

    [Header("camera related")]
    public Vector3 original_cam_position;
    private MSG_Obstacle_Trigger trigger;

    private Vector3 pointer_world_position_temp_location = new Vector3();
    private Vector3 pointer_world_location;
    private Camera c;
    private Event e;
    private bool clicked = false;
    private float distance;
    private bool locked = false;

    public Image tracing_checker;

    public bool using_dif_images;
    public Sprite neutral_image;
    public Sprite incorrect_image;
    public Sprite correct_image;

    [Header("Announcing Letters")]
    public AudioSource audioMain;
    [Header("Announce A Letters")]
    public AudioClip arrow;
    public AudioClip axe;
    public AudioClip acorn;
    public AudioClip apple;
    public AudioClip anchor;
    public AudioClip angelFish;
    [Header("Announce B Letters")]
    public AudioClip button;
    public AudioClip bubble;
    public AudioClip blanket;
    public AudioClip butterFly;
    public AudioClip bee;
    public AudioClip bunny;
    [Header("Announce C Letters")]
    public AudioClip cowBell;
    public AudioClip cobWeb;
    public AudioClip crown;
    public AudioClip clock;
    public AudioClip cake;
    public AudioClip cat;

    // Causing errors
    //[Header("")]

    // Use this for initialization
    void Start()
    {
        c = Camera.main;

        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void Obstacle_Start (int obstacle_number, MSG_Obstacle_Trigger trigg)
    {
        trigger = trigg;

        int past_templates = 0;

        for (int i = 0; i < word_length.Count; i++)
        {
            if (obstacle_number == i)
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

            counter.set_active();

            total_min_count = total_min_count + counter.min_count;
            total_max_count = total_max_count + counter.max_count;
        }
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

                if(!input_dot_active)
                {
                    current_output_node.GetComponent<Renderer>().enabled = false;
                }

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

        if (!input_dot_active)
        {
            current_output_node.GetComponent<Renderer>().enabled = false;
        }

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

        if (!input_dot_active)
        {
            current_input_node.GetComponent<Renderer>().enabled = false;
        }

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
                if (using_dif_images)
                {
                    tracing_checker.sprite = correct_image;
                }
                else
                {
                    tracing_checker.color = Color.green;
                }

                tracing_continue_button.SetActive(true);
                tracing_reset_button.SetActive(false);
            }
            else
            {
                if (using_dif_images)
                {
                    tracing_checker.sprite = incorrect_image;
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

        for (int i = 0; i < temp_tracers.Count; i++)
        {
            Counter counter = temp_tracers[i].GetComponent<Counter>();
            counter.node_count = 0;
            counter.iscorrect = false;
        }

        if (using_dif_images)
        {
            tracing_checker.sprite = neutral_image;
        }
        else
        {
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
        StartCoroutine(MSG_Transitioner.data.Obstacle_Exit_Timer());

        tracing_continue_button.SetActive(false);
        tracing_checker.color = Color.white;
        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Movement_Active();
        trigger = null;
        transform.position = original_cam_position;
        original_cam_position = new Vector3(0, 0, 0);

        total_nodes = 0;
        total_node_count = 0;
        correctnumber = 0;

        total_min_count = 0;
        total_max_count = 0;

        for (int i = 0; i < temp_tracers.Count; i++)
        {
            Counter counter = temp_tracers[i].GetComponent<Counter>();
            counter.set_inactive();
        }

        for (int i = 0; i < node_list.Count; i++)
        {
            Destroy(node_list[i]);
        }
        node_list.Clear();

        temp_tracers.Clear();
    }

    public void Continue_Button ()
    {
        StartCoroutine(MSG_Transitioner.data.Obstacle_Exit_Timer());

        tracing_continue_button.SetActive(false);
        tracing_reset_button.SetActive(true);
        tracing_checker.color = Color.white;
        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Movement_Active();
        trigger.passed = true;
        trigger = null;
        transform.position = original_cam_position;
        original_cam_position = new Vector3(0, 0, 0);
        locked = false;

        total_nodes = 0;
        total_node_count = 0;
        correctnumber = 0;

        total_min_count = 0;
        total_max_count = 0;

        for (int i = 0; i < temp_tracers.Count; i++)
        {
            Counter counter = temp_tracers[i].GetComponent<Counter>();
            counter.set_inactive();
        }

        for (int i = 0; i < node_list.Count; i++)
        {
            Destroy(node_list[i]);
        }
        node_list.Clear();

        temp_tracers.Clear();
    }

    public void AnnnounceLetters()     {         if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_A"))         {             if (trigger.obstacle_int == 0)             {                 audioMain.clip = arrow;                 audioMain.Play();             }              if (trigger.obstacle_int == 1)             {                 audioMain.clip = axe;                 audioMain.Play();             }              if (trigger.obstacle_int == 2)             {                 audioMain.clip = acorn;                 audioMain.Play();             }              if (trigger.obstacle_int == 3)             {                 audioMain.clip = apple;                 audioMain.Play();             }              if (trigger.obstacle_int == 4)             {                 audioMain.clip = anchor;                 audioMain.Play();             }         }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_B"))
        {
            if (trigger.obstacle_int == 0)
            {
                audioMain.clip = button;
                audioMain.Play();
            }

            if (trigger.obstacle_int == 1)
            {
                audioMain.clip = bubble;
                audioMain.Play();
            }

            if (trigger.obstacle_int == 2)
            {
                audioMain.clip = blanket;
                audioMain.Play();
            }

            if (trigger.obstacle_int == 3)
            {
                audioMain.clip = butterFly;
                audioMain.Play();
            }

            if (trigger.obstacle_int == 4)
            {
                audioMain.clip = bee;
                audioMain.Play();
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_C"))
        {
            if (trigger.obstacle_int == 0)
            {
                audioMain.clip = cowBell;
                audioMain.Play();
            }

            if (trigger.obstacle_int == 1)
            {
                audioMain.clip = cobWeb;
                audioMain.Play();
            }

            if (trigger.obstacle_int == 2)
            {
                audioMain.clip = crown;
                audioMain.Play();
            }

            if (trigger.obstacle_int == 3)
            {
                audioMain.clip = clock;
                audioMain.Play();
            }

            if (trigger.obstacle_int == 4)
            {
                audioMain.clip = cake;
                audioMain.Play();
            }
        }
    }
}