using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public int min_count;
    public int node_count = 0;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonUp("Fire1"))
        {
            StartCoroutine("Delay_Time");
        }
	}

    IEnumerator Delay_Time ()
    {
        yield return new WaitForSeconds(0.1f);
        Count();
    }

    void Count ()
    {
        GameObject checker = GameObject.Find("Checker");
        Image checker_image = checker.GetComponent<Image>();

        if (node_count >= min_count)
        {
            checker_image.color = Color.green;
        }
        else
        {
            checker_image.color = Color.red;
        }
    }
}
