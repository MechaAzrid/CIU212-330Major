using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSG_Gold_Block : MonoBehaviour
{
    public Renderer[] black_blocks;
    public Renderer[] grey_blocks;

    public GameObject[] bridge;

    public Color gold_black;
    public Color gold_grey;

    public Color green_black;
    public Color green_grey;

    public GameObject top_text;
    public GameObject bottom_text;

    private bool stickers_collected()
    {
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers.Length - 1; i++)
        {
            if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers[i] == false)
            {
                return false;
            }
        }

        return true;
    }

    void FixedUpdate()
    {
        if (!stickers_collected())
        {
            foreach (GameObject bridge_piece in bridge)
            {
                bridge_piece.SetActive(false);
            }

            for (int i = 0; i < black_blocks.Length; i++)
            {
                black_blocks[i].material.color = gold_black;
                grey_blocks[i].material.color = gold_grey;
                gameObject.GetComponent<Collider>().enabled = true;
            }

            float distance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            if (distance < 1.3f)
            {
                Text count_text = bottom_text.GetComponent<Text>();
                int number = 0;

                for (int i = 0; i < GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers.Length - 1; i++)
                {
                    if(GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers[i])
                    {
                        number++;
                    }
                }

                count_text.text = (number.ToString() + " / " + (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers.Length - 1).ToString());

                top_text.SetActive(true);
                bottom_text.SetActive(true);
            }
            else
            {
                top_text.SetActive(false);
                bottom_text.SetActive(false);
            }
        }

        if (stickers_collected())
        {
            foreach (GameObject bridge_piece in bridge)
            {
                bridge_piece.SetActive(true);
            }

            for (int i = 0; i < black_blocks.Length; i++)
            {
                black_blocks[i].material.color = green_black;
                grey_blocks[i].material.color = green_grey;
                gameObject.GetComponent<Collider>().enabled = false;

                top_text.SetActive(false);
                bottom_text.SetActive(false);
            }
        }
    }
}
