using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum camera_states { None, Pause, Movement, Tracing, Dragging, Obstacle }

public class MSG_Transitioner : MonoBehaviour
{
    [Header("The State that the camera is in")]
    public camera_states cam_states;

    [Header("Tutorial Arrays")]
    public bool[] tutorial_obstacle_passed;
    public bool[] tutorial_stickers;
    public Color[] tutorial_sticker_colours;
    public int sticker_int = 0;

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void Default_Active()
    {
        cam_states = camera_states.None;
    }

    public void Movement_Active ()
    {
        cam_states = camera_states.Movement;
    }

    public void Obstacle_Active ()
    {
        cam_states = camera_states.Obstacle;
    }

    public void Tracing_Active()
    {
        cam_states = camera_states.Tracing;
    }

    public void End ()
    {
        cam_states = camera_states.None;

        for (int i = 0; i < tutorial_obstacle_passed.Length; i++)
        {
            tutorial_obstacle_passed[i] = false;
        }

        for (int i = 0; i < tutorial_stickers.Length; i++)
        {
            tutorial_stickers[i] = false;
        }

        for (int i = 0; i < tutorial_sticker_colours.Length; i++)
        {
            tutorial_sticker_colours[i] = Color.white;
        }

        sticker_int = 0;
    }
}
