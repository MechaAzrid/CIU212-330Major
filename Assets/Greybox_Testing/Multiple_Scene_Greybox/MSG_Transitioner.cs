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
    public Sprite[] tutorial_sticker_blank_sprites;
    public Sprite[] tutorial_sticker_sprites;

    [Header("Level_A Arrays")]
    public bool[] level_a_obstacle_passed;
    public bool[] level_a_stickers;
    public Sprite[] level_a_sticker_blank_sprites;
    public Sprite[] level_a_sticker_sprites;

    [Header("Level_B Arrays")]
    public bool[] level_b_obstacle_passed;
    public bool[] level_b_stickers;
    public Sprite[] level_b_sticker_blank_sprites;
    public Sprite[] level_b_sticker_sprites;

    [Header("Level_C Arrays")]
    public bool[] level_c_obstacle_passed;
    public bool[] level_c_stickers;
    public Sprite[] level_c_sticker_blank_sprites;
    public Sprite[] level_c_sticker_sprites;

    [Header("Whether Saving is active or not")]
    public bool saving_active = true;

    [Header("Tutorial Save data")]
    // Obstacles
    public string[] tutorial_obstacle_passed_key;
    public int[] tutorial_obstacle_passed_value;
    // Stickers
    public string[] tutorial_stickers_key;
    public int[] tutorial_stickers_value;

    [Header("Level_A Save data")]
    // Obstacles
    public string[] level_a_obstacle_passed_key;
    public int[] level_a_obstacle_passed_value;
    // Stickers
    public string[] level_a_stickers_key;
    public int[] level_a_stickers_value;

    [Header("Level_B Save data")]
    // Obstacles
    public string[] level_b_obstacle_passed_key;
    public int[] level_b_obstacle_passed_value;
    // Stickers
    public string[] level_b_stickers_key;
    public int[] level_b_stickers_value;

    [Header("Level_C Save data")]
    // Obstacles
    public string[] level_c_obstacle_passed_key;
    public int[] level_c_obstacle_passed_value;
    // Stickers
    public string[] level_c_stickers_key;
    public int[] level_c_stickers_value;

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // Tutorial
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

        // Level_A
        // obstacles
        for (int i = 0; i < level_a_obstacle_passed_key.Length; i++)
        {
            level_a_obstacle_passed_value[i] = PlayerPrefs.GetInt(level_a_obstacle_passed_key[i], 0);

            if (level_a_obstacle_passed_value[i] == 0)
            {
                level_a_obstacle_passed[i] = false;
            }
            else
            {
                level_a_obstacle_passed[i] = true;
            }
        }
        // stickers
        for (int i = 0; i < level_a_stickers_key.Length; i++)
        {
            level_a_stickers_value[i] = PlayerPrefs.GetInt(level_a_stickers_key[i], 0);

            if (level_a_stickers_value[i] == 0)
            {
                level_a_stickers[i] = false;
            }
            else
            {
                level_a_stickers[i] = true;
            }
        }

        // Level_B
        // obstacles
        for (int i = 0; i < level_b_obstacle_passed_key.Length; i++)
        {
            level_b_obstacle_passed_value[i] = PlayerPrefs.GetInt(level_b_obstacle_passed_key[i], 0);

            if (level_b_obstacle_passed_value[i] == 0)
            {
                level_b_obstacle_passed[i] = false;
            }
            else
            {
                level_b_obstacle_passed[i] = true;
            }
        }
        // stickers
        for (int i = 0; i < level_b_stickers_key.Length; i++)
        {
            level_b_stickers_value[i] = PlayerPrefs.GetInt(level_b_stickers_key[i], 0);

            if (level_b_stickers_value[i] == 0)
            {
                level_b_stickers[i] = false;
            }
            else
            {
                level_b_stickers[i] = true;
            }
        }

        // Level_C
        // obstacles
        for (int i = 0; i < level_c_obstacle_passed_key.Length; i++)
        {
            level_c_obstacle_passed_value[i] = PlayerPrefs.GetInt(level_c_obstacle_passed_key[i], 0);

            if (level_c_obstacle_passed_value[i] == 0)
            {
                level_c_obstacle_passed[i] = false;
            }
            else
            {
                level_c_obstacle_passed[i] = true;
            }
        }
        // stickers
        for (int i = 0; i < level_c_stickers_key.Length; i++)
        {
            level_c_stickers_value[i] = PlayerPrefs.GetInt(level_c_stickers_key[i], 0);

            if (level_c_stickers_value[i] == 0)
            {
                level_c_stickers[i] = false;
            }
            else
            {
                level_c_stickers[i] = true;
            }
        }

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
        if(saving_active)
        {
            // Tutorial
            // obstacles
            for (int i = 0; i < tutorial_obstacle_passed_key.Length; i++)
            {
                if (tutorial_obstacle_passed[i])
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

            // Level_A
            // obstacles
            for (int i = 0; i < level_a_obstacle_passed_key.Length; i++)
            {
                if (level_a_obstacle_passed[i])
                {
                    PlayerPrefs.SetInt(level_a_obstacle_passed_key[i], 1);
                }
                else
                {
                    PlayerPrefs.SetInt(level_a_obstacle_passed_key[i], 0);
                }
            }
            // stickers
            for (int i = 0; i < level_a_stickers_key.Length; i++)
            {
                if (level_a_stickers[i])
                {
                    PlayerPrefs.SetInt(level_a_stickers_key[i], 1);
                }
                else
                {
                    PlayerPrefs.SetInt(level_a_stickers_key[i], 0);
                }
            }

            // Level_B
            // obstacles
            for (int i = 0; i < level_b_obstacle_passed_key.Length; i++)
            {
                if (level_b_obstacle_passed[i])
                {
                    PlayerPrefs.SetInt(level_b_obstacle_passed_key[i], 1);
                }
                else
                {
                    PlayerPrefs.SetInt(level_b_obstacle_passed_key[i], 0);
                }
            }
            // stickers
            for (int i = 0; i < level_b_stickers_key.Length; i++)
            {
                if (level_b_stickers[i])
                {
                    PlayerPrefs.SetInt(level_b_stickers_key[i], 1);
                }
                else
                {
                    PlayerPrefs.SetInt(level_b_stickers_key[i], 0);
                }
            }

            // Level_C
            // obstacles
            for (int i = 0; i < level_c_obstacle_passed_key.Length; i++)
            {
                if (level_c_obstacle_passed[i])
                {
                    PlayerPrefs.SetInt(level_c_obstacle_passed_key[i], 1);
                }
                else
                {
                    PlayerPrefs.SetInt(level_c_obstacle_passed_key[i], 0);
                }
            }
            // stickers
            for (int i = 0; i < level_c_stickers_key.Length; i++)
            {
                if (level_c_stickers[i])
                {
                    PlayerPrefs.SetInt(level_c_stickers_key[i], 1);
                }
                else
                {
                    PlayerPrefs.SetInt(level_c_stickers_key[i], 0);
                }
            }
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
        Reset_Data();
    }

    void Reset_Data ()
    {
        // Tutorial
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

        // Level_A
        // obstacles
        for (int i = 0; i < level_a_obstacle_passed_key.Length; i++)
        {
            PlayerPrefs.SetInt(level_a_obstacle_passed_key[i], 0);
        }
        // stickers
        for (int i = 0; i < level_a_stickers_key.Length; i++)
        {
            PlayerPrefs.SetInt(level_a_stickers_key[i], 0);
        }

        // Level_B
        // obstacles
        for (int i = 0; i < level_b_obstacle_passed_key.Length; i++)
        {
            PlayerPrefs.SetInt(level_b_obstacle_passed_key[i], 0);
        }
        // stickers
        for (int i = 0; i < level_b_stickers_key.Length; i++)
        {
            PlayerPrefs.SetInt(level_b_stickers_key[i], 0);
        }

        // Level_C
        // obstacles
        for (int i = 0; i < level_c_obstacle_passed_key.Length; i++)
        {
            PlayerPrefs.SetInt(level_c_obstacle_passed_key[i], 0);
        }
        // stickers
        for (int i = 0; i < level_c_stickers_key.Length; i++)
        {
            PlayerPrefs.SetInt(level_c_stickers_key[i], 0);
        }
    }
}
