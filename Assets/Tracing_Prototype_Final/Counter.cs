using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Tracer_Node[] node_scripts;

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
    }


    public void Count()
    {
        for (int i = 0; i < node_scripts.Length; i++)
        {
            node_scripts[i].Find_Nodes();
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
