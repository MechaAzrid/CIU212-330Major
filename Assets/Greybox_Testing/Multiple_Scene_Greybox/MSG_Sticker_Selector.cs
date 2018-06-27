using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Levels { none, tutorial }

public class MSG_Sticker_Selector : MonoBehaviour
{
    public Levels level;

    public GameObject[] active_images;

    public Vector3[] sticker_positions;

    public int selected_sticker = -1;

    public LineRenderer lr;

    private bool finder_active;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(selected_sticker == -1)
        {
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
        }
        else
        {
            lr.SetPosition(0, GameObject.FindGameObjectWithTag("Player").transform.position);
            lr.SetPosition(1, sticker_positions[selected_sticker]);
        }

	    for (int i = 0; i < active_images.Length; i++)
        {
            if(i == selected_sticker)
            {
                active_images[i].SetActive(true);
            }
            else
            {
                active_images[i].SetActive(false);
            }
        }

        if (level == Levels.tutorial)
        {
            if (finder_active && GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers[selected_sticker])
            {
                finder_active = false;
                selected_sticker = -1;
            }
        }
    }

    public void Button_Click (int number)
    {
        if (level == Levels.tutorial)
        {
            if (!GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers[number])
            {
                if (number != selected_sticker)
                {
                    finder_active = true;
                    selected_sticker = number;
                }
                else
                {
                    finder_active = false;
                    selected_sticker = -1;
                }
            }
        }
    }
}
