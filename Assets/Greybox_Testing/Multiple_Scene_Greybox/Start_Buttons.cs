using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_Buttons : MonoBehaviour
{
    public string scene;

    public GameObject[] menus;

    public Image[] tutorial_stickers;

    public void Start_Button ()
    {
        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Movement_Active();
        SceneManager.LoadScene(scene);
    }

    public void stickers_Button ()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (i == 1)
            {
                menus[i].SetActive(true);
            }
            else
            {
                menus[i].SetActive(false);
            }
        }

        for (int i = 0; i < tutorial_stickers.Length; i++)
        {
            if(GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers[i])
            {
                tutorial_stickers[i].sprite = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_sticker_sprites[i];
            }
            else
            {
                tutorial_stickers[i].sprite = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_sticker_blank_sprites[i];
            }
        }
    }

    public void Sticker_Return_Button ()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (i == 0)
            {
                menus[i].SetActive(true);
            }
            else
            {
                menus[i].SetActive(false);
            }
        }
    }

    public void Reset_Button ()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if( i == 2)
            {
                menus[i].SetActive(true);
            }
            else
            {
                menus[i].SetActive(false);
            }
        }
    }

    public void Reset_Choice (bool choice)
    {
        if(choice)
        {
            GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().End();

            for (int i = 0; i < menus.Length; i++)
            {
                if (i == 0)
                {
                    menus[i].SetActive(true);
                }
                else
                {
                    menus[i].SetActive(false);
                }
            }
        }
        else if (!choice)
        {
            for (int i = 0; i < menus.Length; i++)
            {
                if (i == 0)
                {
                    menus[i].SetActive(true);
                }
                else
                {
                    menus[i].SetActive(false);
                }
            }
        }
    }

    public void Quit_Button()
    {
        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Save_Data();
        Application.Quit();
    }
}
