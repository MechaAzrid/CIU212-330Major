using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSG_Pickup : MonoBehaviour
{
    public bool gold;
    public int sticker_number;

    private Renderer object_renderer;
    private SpriteRenderer object_sprite;

    public float distance;

    public GameObject player;
    public MSG_Pickup_System pickup_system;

    public bool locked = false;

    public string temp_data;

    // Use this for initialization
    void Start ()
    {
        object_renderer = GetComponent<Renderer>();
        object_sprite = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");
        pickup_system = GameObject.Find("Main Camera").GetComponent<MSG_Pickup_System>();

        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers[sticker_number] == false)
        {
            gameObject.GetComponent<Collider>().enabled = true;

            if (object_renderer != null) gameObject.GetComponent<Renderer>().enabled = true;
            if (object_sprite != null) gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < 1.5f && !locked)
        {
            locked = true;
            pickup_system.pickup_object = gameObject;
            pickup_system.pickup = gameObject.GetComponent<MSG_Pickup>();
            pickup_system.Picked_Up(sticker_number);

            if(gold)
            {
                GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Tracing_Active();

                GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
                cam.transform.position = new Vector3(-10000, 0, -10);
            }
        }
    }
}
