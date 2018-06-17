﻿using System.Collections;
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
    public int sticker_int = 0;
    public Color[] tutorial_sticker_colours;

    [Header("Tutorial Save data")]
    // Obstacles
    public string[] tutorial_obstacle_passed_key;
    public int[] tutorial_obstacle_passed_value;

    // Stickers
    public string[] tutorial_stickers_key;
    public int[] tutorial_stickers_value;

    public string sticker_int_key;
    public int sticker_int_value;

    public string[] tutorial_sticker_colours_red_key;
    public string[] tutorial_sticker_colours_green_key;
    public string[] tutorial_sticker_colours_blue_key;
    public float[] tutorial_sticker_colours_red_value;
    public float[] tutorial_sticker_colours_green_value;
    public float[] tutorial_sticker_colours_blue_value;

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // obstacles
        for (int i = 0; i < tutorial_obstacle_passed_key.Length; i++)
        {
            tutorial_obstacle_passed_value[i] = PlayerPrefs.GetInt(tutorial_obstacle_passed_key[i], 0);

            if(tutorial_obstacle_passed_value[i] == 0)
            {
                tutorial_obstacle_passed[i] = false;
            }
            else
            {
                tutorial_obstacle_passed[i] = true;
            }
        }

        // stickers
        Debug.Log("Yeet");

        for (int i = 0; i < tutorial_stickers_key.Length; i++)
        {
            tutorial_stickers_value[i] = PlayerPrefs.GetInt(tutorial_stickers_key[i], 0);

            if (tutorial_stickers_value[i] == 0)
            {
                tutorial_stickers[i] = false;
            }
            else
            {
                tutorial_stickers[i] = true;
            }
        }

        // sticker int
        sticker_int_value = PlayerPrefs.GetInt(sticker_int_key, 0);
        sticker_int = sticker_int_value;

        // sticker colours
        for (int i = 0; i < tutorial_sticker_colours_red_key.Length; i++)
        {
            tutorial_sticker_colours_red_value[i] = PlayerPrefs.GetFloat(tutorial_sticker_colours_red_key[i], 255);
            tutorial_sticker_colours_green_value[i] = PlayerPrefs.GetFloat(tutorial_sticker_colours_green_key[i], 255);
            tutorial_sticker_colours_blue_value[i] = PlayerPrefs.GetFloat(tutorial_sticker_colours_blue_key[i], 255);

            tutorial_sticker_colours[i] = new Color(tutorial_sticker_colours_red_value[i], tutorial_sticker_colours_green_value[i], tutorial_sticker_colours_blue_value[i]);
        }


        Save_Data();
    }

    public void Save_Data ()
    {
        // obstacles
        for (int i = 0; i < tutorial_obstacle_passed_key.Length; i++)
        {
            if(tutorial_obstacle_passed[i])
            {
                PlayerPrefs.SetInt(tutorial_obstacle_passed_key[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt(tutorial_obstacle_passed_key[i], 0);
            }
        }

        // stickers
        for (int i = 0; i < tutorial_stickers_key.Length; i++)
        {
            if (tutorial_stickers[i])
            {
                PlayerPrefs.SetInt(tutorial_stickers_key[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt(tutorial_stickers_key[i], 0);
            }
        }

        // sticker int
        PlayerPrefs.SetInt(sticker_int_key, sticker_int);

        // sticker colour
        for (int i = 0; i < tutorial_sticker_colours_red_key.Length; i++)
        {
            PlayerPrefs.SetFloat(tutorial_sticker_colours_red_key[i], tutorial_sticker_colours[i].r);
            PlayerPrefs.SetFloat(tutorial_sticker_colours_green_key[i], tutorial_sticker_colours[i].g);
            PlayerPrefs.SetFloat(tutorial_sticker_colours_blue_key[i], tutorial_sticker_colours[i].b);
        }
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
        Reset_Data();
    }

    void Reset_Data ()
    {
        // obstacles
        for (int i = 0; i < tutorial_obstacle_passed_key.Length; i++)
        {
            PlayerPrefs.SetInt(tutorial_obstacle_passed_key[i], 0);
        }

        // stickers
        for (int i = 0; i < tutorial_stickers_key.Length; i++)
        {
            PlayerPrefs.SetInt(tutorial_stickers_key[i], 0);
        }

        // sticker int
        PlayerPrefs.SetInt(sticker_int_key, 0);

        // sticker colour
        for (int i = 0; i < tutorial_sticker_colours_red_key.Length; i++)
        {
            PlayerPrefs.SetFloat(tutorial_sticker_colours_red_key[i], 255);
            PlayerPrefs.SetFloat(tutorial_sticker_colours_green_key[i], 255);
            PlayerPrefs.SetFloat(tutorial_sticker_colours_blue_key[i], 255);
        }
    }
}
