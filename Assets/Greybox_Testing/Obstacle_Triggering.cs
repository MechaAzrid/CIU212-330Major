using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Triggering : MonoBehaviour {

    public GameObject obstacle;
    public GameObject player;
    public Obstacle_Prototype CP;
    public GameObject worldLayout;

    public Level_Movement LM;
    public Obstacle_Prototype OP;


    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        CP.enabled = true;
        player.gameObject.SetActive(false);
        worldLayout.gameObject.SetActive(false);
        LM.enabled = false;
        OP.enabled = true;
        obstacle.gameObject.SetActive(true);
    }
}
