using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSG_Sticker_Selector : MonoBehaviour
{
    public GameObject[] active_images;
    //public GameObject[] active_buttons;

    public int selected_sticker = -1;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
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
	}

    public void Button_Click (int number)
    {
        Debug.Log("Clicked");
        selected_sticker = number;
    }
}
