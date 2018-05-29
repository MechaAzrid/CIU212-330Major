using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Buttons : MonoBehaviour
{
    public string scene;

    public void Start_Button ()
    {
        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Movement_Active();
        SceneManager.LoadScene(scene);
    }

    public void Quit_Button()
    {
        Application.Quit();
    }
}
