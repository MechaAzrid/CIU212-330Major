using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracer_Node : MonoBehaviour
{
    public Counter counter;
    private LineRenderer lr;
    public GameObject reciever;

	// Use this for initialization
	void Start ()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, reciever.transform.position);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButtonUp("Fire1"))
        {
            Collider[] hitcolliders = Physics.OverlapSphere(transform.position, 0.5f);
            int i = 0;
            while (i < hitcolliders.Length)
            {
                if(hitcolliders[i].tag == "input_node")
                {
                    counter.node_count++;
                    i = hitcolliders.Length + 1;
                }
                i++;
            }
        }
	}
}
