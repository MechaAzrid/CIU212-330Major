using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Triggers : MonoBehaviour
{
    [Header("Universal Scripts")]
    public MSG_Level_Movement msgLevelMovement;

    [Header("Gameobject Triggers")]
    public GameObject welcomeTrigger;
    public GameObject selectStickerTrigger;
    public GameObject obstacleTrigger;
    public GameObject stickerBookTrigger;
    public GameObject tracingTrigger;
    public GameObject dragAndDropTrigger;

    [Header("Audio")]
    public AudioSource audioMain;
    public AudioClip welcomeVoice;
    public AudioClip beginVoice;
    public AudioClip movementVoice;

    void Start()
    {
        audioMain = GetComponent<AudioSource>();
        //Find The GameObject's AudioSource
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            WelcomeDialogue();
            //If The Trigger Detects The Player Tag Instatiate The WelcomeDialogue
        }
    }

    void WelcomeDialogue()
    {
        StartCoroutine(playWelcomeDialogue());

        msgLevelMovement.enabled = false;
        //Disable Level Movement
    }

    IEnumerator playWelcomeDialogue()
    {
        audioMain.clip = welcomeVoice;
        audioMain.Play();
        yield return new WaitForSeconds(audioMain.clip.length);
        audioMain.clip = beginVoice;
        audioMain.Play();
        yield return new WaitForSeconds(audioMain.clip.length);
        audioMain.clip = movementVoice;
        audioMain.Play();

        Invoke("WelcomeDialogueEnd", audioMain.clip.length);
        //Once DialogueVoice Has Reached The End Of The Length (Seconds) Invoke WelcomeDialogueEnd
    }

    void WelcomeDialogueEnd()
    {
        welcomeTrigger.SetActive(false);
        msgLevelMovement.enabled = true;
        //Disable WelcomeTrigger GameObject and Enable Level Movement
    }

    void Update()
    {

    }
}
