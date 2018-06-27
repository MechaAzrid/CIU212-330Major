using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
            pickup_slots[i].color = GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_sticker_colours[i];
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		
	}

    public void Picked_Up(int sticker_number)
    {
        pickup_slots[sticker_number].color = pickup.pickup_color;
        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_sticker_colours[sticker_number] = pickup.pickup_color;
        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers[sticker_number] = true;

        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Save_Data();
        Destroy(pickup_object);
        pickup_object = null;
        pickup = null;

        if(tutorialTriggers != null) tutorialTriggers.CongratulationsFirst();

    }
}
