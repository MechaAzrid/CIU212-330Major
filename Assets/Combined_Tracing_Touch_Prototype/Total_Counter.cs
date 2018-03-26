using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Total_Counter : MonoBehaviour
{
    public Combined_Draw_Touch drawing_script;
    public GameObject[] tracers;

    public int total_min_count;
    public int total_max_count;
    public int total_node_count = 0;

	// Use this for initialization
	void Start ()
    {
		for(int i = 0; i < tracers.Length; i++)
        {
            Counter counter = tracers[i].GetComponent<Counter>();
            total_min_count = total_min_count + counter.min_count;
            total_max_count = total_max_count + counter.max_count;
        }
	}

    public void Count ()
    {

        for (int i = 0; i < tracers.Length; i++)
        {
            Counter counter = tracers[i].GetComponent<Counter>();
            total_node_count = total_node_count + counter.node_count;
        }

        GameObject checker = GameObject.Find("Checker");
        Image checker_image = checker.GetComponent<Image>();

        if (total_node_count >= total_min_count && drawing_script.total_nodes <= total_max_count)
        {
            checker_image.color = Color.green;
        }
        else
        {
            checker_image.color = Color.red;
        }
    }
}
