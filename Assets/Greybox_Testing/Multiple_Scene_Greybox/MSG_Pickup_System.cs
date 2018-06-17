using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MSG_Pickup_System : MonoBehaviour
{
    public Image[] pickup_slots;
    private int array_number = 0;

    private Vector3 distance;
    public GameObject pickup_object;
    public MSG_Pickup pickup;

    void Start ()
    {
        for(int i = 0; i < pickup_slots.Length; i++)
        {
            pickup_slots[i].color = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_sticker_colours[i];
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		
	}

    public void Picked_Up(int sticker_number)
    {
        pickup_slots[GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().sticker_int].color = pickup.pickup_color;
        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_sticker_colours[GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().sticker_int] =
            pickup.pickup_color;
        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers[sticker_number] = true;

        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().sticker_int++;
        Destroy(pickup_object);
        pickup_object = null;
        pickup = null;
    }
}
