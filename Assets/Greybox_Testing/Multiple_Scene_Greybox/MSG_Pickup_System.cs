using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MSG_Pickup_System : MonoBehaviour
{
    public Tutorial_Triggers tutorialTriggers;

    public Image[] pickup_slots;
    private int array_number = 0;

    private Vector3 distance;
    public GameObject pickup_object;
    public MSG_Pickup pickup;

    void Start ()
    {
        for(int i = 0; i < pickup_slots.Length; i++)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MSG_Level"))
            {
                if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers[i])
                {
                    pickup_slots[i].sprite = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_sticker_sprites[i];
                }
                else
                {
                    pickup_slots[i].sprite = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_sticker_blank_sprites[i];
                }
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_A"))
            {
                if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_a_stickers[i])
                {
                    pickup_slots[i].sprite = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_a_sticker_sprites[i];
                }
                else
                {
                    pickup_slots[i].sprite = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_a_sticker_blank_sprites[i];
                }
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_B"))
            {
                if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_a_stickers[i])
                {
                    pickup_slots[i].sprite = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_b_sticker_sprites[i];
                }
                else
                {
                    pickup_slots[i].sprite = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_b_sticker_blank_sprites[i];
                }
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_C"))
            {
                if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_a_stickers[i])
                {
                    pickup_slots[i].sprite = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_c_sticker_sprites[i];
                }
                else
                {
                    pickup_slots[i].sprite = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_c_sticker_blank_sprites[i];
                }
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		
	}

    public void Picked_Up(int sticker_number)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MSG_Level"))
        {
            pickup_slots[sticker_number].sprite = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_sticker_sprites[sticker_number];
            GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers[sticker_number] = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_A"))
        {
            pickup_slots[sticker_number].sprite = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_a_sticker_sprites[sticker_number];
            GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_a_stickers[sticker_number] = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_B"))
        {
            pickup_slots[sticker_number].sprite = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_b_sticker_sprites[sticker_number];
            GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_b_stickers[sticker_number] = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_C"))
        {
            pickup_slots[sticker_number].sprite = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_c_sticker_sprites[sticker_number];
            GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_c_stickers[sticker_number] = true;
        }

        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Save_Data();
        Destroy(pickup_object);
        pickup_object = null;
        pickup = null;

        if(tutorialTriggers != null) tutorialTriggers.CongratulationsFirst();

    }
}
