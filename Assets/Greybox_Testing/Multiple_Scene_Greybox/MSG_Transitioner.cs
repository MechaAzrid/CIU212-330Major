using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MSG_Transitioner : MonoBehaviour
{
    public string level_name;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadSceneAsync(level_name, LoadSceneMode.Additive);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.UnloadSceneAsync(level_name);
        }
    }
}
