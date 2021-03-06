﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [Header("-Place in first tracer node-")]

    [Header("Items below need to be set in inspector")]

    [Header("Assign each node into the array in order")]
    public GameObject[] tracer_nodes;

    [Header("The Minimum and Maximum amount of drawn nodes there are allowed to be for this template")]
    public int min_count;
    public int max_count;

    [Header("Items below do not need to be touched")]

    [Header("Total amount of nodes in template")]
    public int total_nodes;

    [Header("The number of drawn nodes detected")]
    public int node_count = 0;

    [Header("Bool stating whether or not this template is being considered correct")]
    public bool iscorrect = false;

    private GameObject cam;
    private Tracing_Final tfs;

    // Use this for initialization
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        tfs = cam.GetComponent<Tracing_Final>();

        for (int i = 0; i < tracer_nodes.Length; i++)
        {
            LineRenderer lr = tracer_nodes[i].GetComponent<LineRenderer>();

            if (i == tracer_nodes.Length - 1)
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


    public void Count ()
    {
        for (int i = 0; i < tracer_nodes.Length; i++)
        {
            GameObject node = tracer_nodes[i];
            Loop(node);
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

    void Loop (GameObject node)
    {
        Collider[] hitcolliders = Physics.OverlapSphere(node.transform.position, 0.5f);
        int i = 0;
        while (i < hitcolliders.Length)
        {
            if (hitcolliders[i].tag == "input_node")
            {
                node_count++;
                i = hitcolliders.Length + 1;
            }
            else
            {
                i++;
            }
        }
    }
}
