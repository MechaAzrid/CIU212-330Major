using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset_Scene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset_Scene_F ()
    {
        SceneManager.LoadScene("Tracing_Prototype_Scene");
    }
}
