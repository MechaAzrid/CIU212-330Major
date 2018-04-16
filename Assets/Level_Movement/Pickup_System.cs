using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pickup_System : MonoBehaviour
{
    public Image[] pickup_slots;
    private int array_number = 0;

    private Vector3 distance;

    public GameObject pickup_object;
    public Pickup pickup;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Picked_Up ()
    {
        pickup_slots[array_number].color = pickup.pickup_color;
        array_number++;
        Destroy(pickup_object);
        pickup_object = null;
        pickup = null;
    }
}
