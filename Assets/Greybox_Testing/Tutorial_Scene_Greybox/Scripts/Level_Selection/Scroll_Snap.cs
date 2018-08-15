using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scroll_Snap : MonoBehaviour
{
    [Header("Audio SFX")]
    public AudioSource audioMain;
    public AudioClip levelSelect;

    public RectTransform panel; //Holds ScrollPanel
    public Button[] button; //Holds all the Buttons
    public RectTransform center; //Center To Compare The Distance For Each Button

    private float[] distance; //Holds Every Buttons Distance To The Center
    private bool dragging = false; //True while we drag
    private int buttonDistance; //Holds The Distance Between The Buttons
    private int minButtonNumber; //Holds Which Distance Is The Smallest One

    void Start ()
    {
        audioMain = gameObject.GetComponent<AudioSource>();

        int buttonLength = button.Length;
        distance = new float[buttonLength];
        //Declare the length of the distance array - distance array will have the same length as the button array

        buttonDistance = (int)Mathf.Abs(button[1].GetComponent<RectTransform>().anchoredPosition.x - button[0].GetComponent<RectTransform>().anchoredPosition.x);
        //Getting the distance between buttons
	}
	
	void Update ()
    {
		for (int  i = 0; i < button.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - button[i].transform.position.x);
        }

        float minDistance = Mathf.Min(distance);
        //Minimum number and store in the minDistance

        for (int a = 0; a < button.Length; a++)
        {
            if (minDistance == distance[a])
            {
                minButtonNumber = a;
            }
        }

        if (!dragging)
        {
            LerpToButton(minButtonNumber * -buttonDistance);
        }
	}

    void LerpToButton(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPostion = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPostion;
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }

    public void Start_Button(string scene)
    {
        audioMain.clip = levelSelect;
        audioMain.Play();

        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Movement_Active();
        SceneManager.LoadScene(scene);
    }
}

