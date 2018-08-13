using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sticker_Book : MonoBehaviour
{
    public int page_number = 0;

    public GameObject left_arrow;
    public GameObject right_arrow;

    public GameObject[] pages;

    [Header("Level A")]
    public Image[] level_a_sticker_images;
    public Sprite[] level_a_blank_sprites;
    public Sprite[] level_a_filled_sprites;

    [Header("Level B")]
    public Image[] level_b_sticker_images;
    public Sprite[] level_b_blank_sprites;
    public Sprite[] level_b_filled_sprites;

    [Header("Level C")]
    public Image[] level_c_sticker_images;
    public Sprite[] level_c_blank_sprites;
    public Sprite[] level_c_filled_sprites;


    // Use this for initialization
    void Start ()
    {
        Book_Opened();
    }

    public void Book_Opened ()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_A"))
        {
            page_number = 1;

            for (int i = 0; i < level_a_sticker_images.Length; i++)
            {
                if (MSG_Transitioner.data.level_a_stickers[i])
                {
                    level_a_sticker_images[i].sprite = MSG_Transitioner.data.level_a_sticker_sprites[i];
                }
                else
                {
                    level_a_sticker_images[i].sprite = MSG_Transitioner.data.level_a_sticker_blank_sprites[i];
                }
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_B"))
        {
            page_number = 2;

            for (int i = 0; i < level_b_sticker_images.Length; i++)
            {
                if (MSG_Transitioner.data.level_b_stickers[i])
                {
                    level_b_sticker_images[i].sprite = MSG_Transitioner.data.level_b_sticker_sprites[i];
                }
                else
                {
                    level_b_sticker_images[i].sprite = MSG_Transitioner.data.level_b_sticker_blank_sprites[i];
                }
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_C"))
        {
            page_number = 3;

            for (int i = 0; i < level_c_sticker_images.Length; i++)
            {
                if (MSG_Transitioner.data.level_c_stickers[i])
                {
                    level_c_sticker_images[i].sprite = MSG_Transitioner.data.level_c_sticker_sprites[i];
                }
                else
                {
                    level_c_sticker_images[i].sprite = MSG_Transitioner.data.level_c_sticker_blank_sprites[i];
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Enabling And Disabling arrows at beginning and ending 
        if (page_number <= 0)
        {
            left_arrow.SetActive(false);
        }
        else
        {
            left_arrow.SetActive(true);
        }

        if (page_number >= 3)
        {
            right_arrow.SetActive(false);
        }
        else
        {
            right_arrow.SetActive(true);
        }

        for (int i = 0; i < pages.Length; i++)
        {
            if (i == page_number)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
    }

    public void Arrow_Left ()
    {
        page_number--;
    }

    public void Arrow_Right ()
    {
        page_number++;
    }
}
