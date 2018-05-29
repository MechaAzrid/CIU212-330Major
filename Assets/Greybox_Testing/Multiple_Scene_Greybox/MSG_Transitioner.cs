using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum camera_states { None, Pause, Movement, Tracing, Dragging, Obstacle }

public class MSG_Transitioner : MonoBehaviour
{
    [Header("The State that the camera is in")]
    public camera_states cam_states;

    [Header("Level Names")]
    public string level_name;

    [Header("Sticker Arrays")]
    public bool[] tutorial_stickers;

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void Movement_Active ()
    {
        cam_states = camera_states.Movement;
    }

    public void Obstacle_Active ()
    {
        cam_states = camera_states.Obstacle;
    }
}
