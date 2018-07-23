﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    public MSG_Level_Movement move_script;

    public float wait_timer = 0.5f;

    public int direction_int = 2;

    public GameObject[] up_left_animation;
    public GameObject[] up_right_animation;
    public GameObject[] down_right_animation;
    public GameObject[] down_left_animation;

    public int animation_frame = 0;

    public bool locked;

    private Transform player;

    private Vector3 temp_position;

	// Use this for initialization
	void Start ()
    {
        player = GetComponent<Transform>();
        temp_position = player.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (player.position.x < temp_position.x && player.position.y > temp_position.y && move_script.moving)
        {
            temp_position = player.position;

            direction_int = 0;

            if (!locked)
            {
                locked = true;
                Move();
            }
        }

        if (player.position.x > temp_position.x && player.position.y > temp_position.y && move_script.moving)
        {
            temp_position = player.position;

            direction_int = 1;

            if (!locked)
            {
                locked = true;
                Move();
            }
        }

        if (player.position.x > temp_position.x && player.position.y < temp_position.y && move_script.moving)
        {
            temp_position = player.position;

            direction_int = 2;

            if (!locked)
            {
                locked = true;
                Move();
            }
        }

        if (player.position.x < temp_position.x && player.position.y < temp_position.y && move_script.moving)
        {
            temp_position = player.position;

            direction_int = 3;

            if (!locked)
            {
                locked = true;
                Move();
            }
        }

        if (!move_script.moving)
        {
            locked = false;
            Stop();
        }
    }

    void Move ()
    {
        if (direction_int == 0)
        {
            for (int i = 0; i < up_left_animation.Length; i++)
            {
                if(i != animation_frame)
                {
                    up_left_animation[i].SetActive(false);
                    up_right_animation[i].SetActive(false);
                    down_right_animation[i].SetActive(false);
                    down_left_animation[i].SetActive(false);
                }
                else
                {
                    up_left_animation[i].SetActive(true);
                }
            }
        }

        if (direction_int == 1)
        {
            for (int i = 0; i < up_right_animation.Length; i++)
            {
                if (i != animation_frame)
                {
                    up_left_animation[i].SetActive(false);
                    up_right_animation[i].SetActive(false);
                    down_right_animation[i].SetActive(false);
                    down_left_animation[i].SetActive(false);
                }
                else
                {
                    up_right_animation[i].SetActive(true);
                }
            }
        }

        if (direction_int == 2)
        {
            for (int i = 0; i < down_right_animation.Length; i++)
            {
                if (i != animation_frame)
                {
                    up_left_animation[i].SetActive(false);
                    up_right_animation[i].SetActive(false);
                    down_right_animation[i].SetActive(false);
                    down_left_animation[i].SetActive(false);
                }
                else
                {
                    down_right_animation[i].SetActive(true);
                }
            }
        }

        if (direction_int == 3)
        {
            for (int i = 0; i < down_left_animation.Length; i++)
            {
                if (i != animation_frame)
                {
                    up_left_animation[i].SetActive(false);
                    up_right_animation[i].SetActive(false);
                    down_right_animation[i].SetActive(false);
                    down_left_animation[i].SetActive(false);
                }
                else
                {
                    down_left_animation[i].SetActive(true);
                }
            }
        }

        StartCoroutine(Wait_Time());
    }

    void Stop ()
    {
        animation_frame = 0;

        for (int i = 0; i < up_left_animation.Length; i++)
        {
            up_left_animation[i].SetActive(false);
            up_right_animation[i].SetActive(false);
            down_right_animation[i].SetActive(false);
            down_left_animation[i].SetActive(false);
        }

        if(direction_int == 0)
        {
            up_left_animation[0].SetActive(true);
        }
        if (direction_int == 1)
        {
            up_right_animation[0].SetActive(true);
        }
        if (direction_int == 2)
        {
            down_right_animation[0].SetActive(true);
        }
        if (direction_int == 3)
        {
            down_left_animation[0].SetActive(true);
        }
    }

    IEnumerator Wait_Time ()
    {
        if (move_script.moving)
        {
            yield return new WaitForSeconds(wait_timer);

            if (animation_frame == up_left_animation.Length - 1)
            {
                animation_frame = 0;
            }
            else
            {
                animation_frame++;
            }

            Move();
        }
    }
}