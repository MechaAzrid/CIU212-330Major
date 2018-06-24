using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Triggers : MonoBehaviour
{
    [Header("Universal Scripts")]
    public MSG_Level_Movement msgLevelMovement;
    public MSG_Sticker_Selector msgStickerSelector;
    public MSG_Obstacle_Prototype msgObstaclePrototype;
    public MSG_Obstacle_Trigger msgObstacleTrigger;

    [Header("Gameobject Triggers")]
    public GameObject welcomeTrigger;
    public GameObject selectStickerTrigger;
    public GameObject obstacleTrigger;
    public GameObject stickerBookTrigger;
    public GameObject tracingTrigger;
    public GameObject dragAndDropTrigger;

    [Header("Obstacles and Tracing")]
    public GameObject tutorialObstacle;

    [Header("Audio")]
    public AudioSource audioMain;
    public AudioClip welcomeVoice;
    public AudioClip beginVoice;
    public AudioClip movementVoice;

    public AudioClip stickerSelectVoice;
    public AudioClip stickerPathVoice;

    public AudioClip obstacleVoice;
    public AudioClip obstacleInstructionsVoice;

    void Start()
    {
        audioMain = gameObject.GetComponent<AudioSource>();
        //Find The GameObject's AudioSource
    }

    public void WelcomeDialogue()
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

        selectStickerTrigger.SetActive(true);

        //Disable WelcomeTrigger GameObject and Enable Level Movement
    }

    public void SelectStickerDialogue()
    {
        StartCoroutine(playSelectSticker());
        msgStickerSelector.enabled = true;
        msgLevelMovement.enabled = false;
    }

    IEnumerator playSelectSticker()
    {
        audioMain.clip = stickerSelectVoice;
        audioMain.Play();
        yield return new WaitForSeconds(audioMain.clip.length);
        audioMain.clip = stickerPathVoice;
        audioMain.Play();

        Invoke("SelectStickerDialogueEnd", audioMain.clip.length);
    }

    void SelectStickerDialogueEnd()
    {
        selectStickerTrigger.SetActive(false);
        obstacleTrigger.SetActive(true);
        msgLevelMovement.enabled = true;
    }

    public void ObstacleDialogue()
    {
        StartCoroutine(playObstacle());
        msgLevelMovement.enabled = false;
    }

    IEnumerator playObstacle()
    {
        audioMain.clip = obstacleVoice;
        audioMain.Play();
        yield return new WaitForSeconds(audioMain.clip.length);
        tutorialObstacle.GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(1f);
        audioMain.clip = obstacleInstructionsVoice;
        audioMain.Play();

        Invoke("ObstacleDialogueEnd", audioMain.clip.length);
    }

    void ObstacleDialogueEnd()
    {
        obstacleTrigger.SetActive(false);
        msgLevelMovement.enabled = true;
    }

    void Update()
    {

    }
}
