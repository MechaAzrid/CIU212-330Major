using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter_C : MonoBehaviour
{
    public GameObject[] tracer_nodes;

    public int min_count;
    public int node_count = 0;
    public int max_count;

    private GameObject cam;
    private Tracing_Final tfs;

    public bool iscorrect = false;

    // Use this for initialization
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        tfs = cam.GetComponent<Tracing_Final>();

        for(int i = 0; i < tracer_nodes.Length; i++)
        {
            LineRenderer lr = tracer_nodes[i].GetComponent<LineRenderer>();
            lr = GetComponent<LineRenderer>();
            if(i == tracer_nodes.Length)
            {
                lr.SetPosition(0, tracer_nodes[i].transform.position);
                lr.SetPosition(1, tracer_nodes[i].transform.position);
            }
            else
            {
                lr.SetPosition(0, tracer_nodes[i].transform.position);
                lr.SetPosition(1, tracer_nodes[i + 1].transform.position);
            }
        }
    }


    public void Count()
    {
        for (int i = 0; i < tracer_nodes.Length; i++)
        {
            Collider[] hitcolliders = Physics.OverlapSphere(tracer_nodes[i].transform.position, 0.5f);
            int j = 0;
            while (j < hitcolliders.Length)
            {
                if (hitcolliders[j].tag == "input_node")
                {
                    node_count++;
                    j = hitcolliders.Length + 1;
                }
                else
                {
                    j++;
                }
            }
        }

        tfs.total_node_count += node_count;

        if (node_count > min_count)
        {
            iscorrect = true;
        }
        else
        {
            iscorrect = false;
        }
    }
}
