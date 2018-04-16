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

    public GameObject data_start;

    public List<string> temp_data_array = new List<string>();

    public Text data_text;
    private Text temp_text;
    private Vector3 start_position;
    private Vector3 temp_position;

	// Use this for initialization
	void Start ()
    {
        start_position = data_start.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Picked_Up ()
    {
        pickup_slots[array_number].color = pickup.pickup_color;
        temp_data_array.Add(pickup.temp_data);

        temp_text = Instantiate(data_text, data_start.transform) as Text;
        temp_position = new Vector3(start_position.x, start_position.y - 40, start_position.z);
        temp_text.transform.position = temp_position;
        start_position = temp_position;
        temp_text.text = pickup.temp_data.ToString();
        temp_text = null;

        array_number++;
        Destroy(pickup_object);
        pickup_object = null;
        pickup = null;
    }
}
