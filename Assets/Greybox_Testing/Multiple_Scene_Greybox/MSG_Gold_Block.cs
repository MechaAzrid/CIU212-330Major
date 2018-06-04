using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSG_Gold_Block : MonoBehaviour
{
    public bool passed;

    public Renderer[] black_blocks;
    public Renderer[] grey_blocks;

    public Color gold_black;
    public Color gold_grey;

    public Color green_black;
    public Color green_grey;

    // Use this for initialization
    void Start ()
    {
		
	}

    void FixedUpdate()
    {
        if (!passed)
        {
            for (int i = 0; i < black_blocks.Length; i++)
            {
                black_blocks[i].material.color = gold_black;
                grey_blocks[i].material.color = gold_grey;
                gameObject.GetComponent<Collider>().enabled = true;
            }
        }

        if (passed)
        {
            for (int i = 0; i < black_blocks.Length; i++)
            {
                black_blocks[i].material.color = green_black;
                grey_blocks[i].material.color = green_grey;
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
