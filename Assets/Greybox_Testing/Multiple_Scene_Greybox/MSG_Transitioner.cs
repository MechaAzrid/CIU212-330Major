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
    public int sticker_int = 0;
    public Sprite[] tutorial_sticker_blank_sprites;
    public Sprite[] tutorial_sticker_sprites;

    [Header("Tutorial Save data")]
    // Obstacles
    public string[] tutorial_obstacle_passed_key;
    public int[] tutorial_obstacle_passed_value;

    // Stickers
    public string[] tutorial_stickers_key;
    public int[] tutorial_stickers_value;

    public string sticker_int_key;
    public int sticker_int_value;

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

        Save_Data();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            End();
        }
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
    }
}
