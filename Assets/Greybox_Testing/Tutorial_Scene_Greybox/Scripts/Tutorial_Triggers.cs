using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Triggers : MonoBehaviour
{
    [Header("Universal Scripts")]
    public Tutorial_Triggers tutorialTriggers;
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
    public GameObject firstSticker;
    public GameObject dragAndDropTrigger;

    [Header("Obstacles and Tracing")]
    public GameObject tutorialObstacle;
    public GameObject activeSticker;

    public int obstacle_int;


    [Header("Audio")]
    public AudioSource audioMain;
    public AudioClip welcomeVoice;
    public AudioClip beginVoice;
    public AudioClip movementVoice;

    public AudioClip stickerSelectVoice;
    public AudioClip stickerPathVoice;
    public AudioClip stickerPathVoice2;

    public AudioClip obstacleVoice;
    public AudioClip obstacleInstructionsVoice;
    public AudioClip obstacleInstructionsVoice2;
    public AudioClip obstacleInstructionsVoice3;
    public AudioClip obstacleInstructionsVoice4;
    public AudioClip obstacleInstructionsVoice5;
    public AudioClip obstacleInstructionsVoice6;

    public AudioClip firstStickerVoice;


    void Start()
    {
        audioMain = gameObject.GetComponent<AudioSource>();
        //Find The GameObject's AudioSource
    }

    public void Update()
    {

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
        yield return new WaitForSeconds(audioMain.clip.length);
        audioMain.clip = stickerPathVoice2;
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
        //tutorialObstacle.GetComponent<Button>().onClick.Invoke();
        ObstacleTrigger();
        msgObstaclePrototype.enabled = false;
        yield return new WaitForSeconds(1f);
        audioMain.clip = obstacleInstructionsVoice;
        audioMain.Play();
        yield return new WaitForSeconds(audioMain.clip.length);
        audioMain.clip = obstacleInstructionsVoice2;
        audioMain.Play();
        yield return new WaitForSeconds(audioMain.clip.length);
        audioMain.clip = obstacleInstructionsVoice3;
        audioMain.Play();
        yield return new WaitForSeconds(audioMain.clip.length);
        audioMain.clip = obstacleInstructionsVoice4;
        audioMain.Play();
        yield return new WaitForSeconds(audioMain.clip.length);
        audioMain.clip = obstacleInstructionsVoice5;
        audioMain.Play();
        yield return new WaitForSeconds(audioMain.clip.length);
        audioMain.clip = obstacleInstructionsVoice6;
        audioMain.Play();

        Invoke("ObstacleDialogueEnd", audioMain.clip.length);
    }

    void ObstacleDialogueEnd()
    {
        msgObstaclePrototype.enabled = true;
        obstacleTrigger.SetActive(false);
        msgLevelMovement.enabled = true;
        
    }

    public void CongratulationsFirst()
    {
        msgLevelMovement.enabled = false;
        audioMain.clip = firstStickerVoice;
        audioMain.Play();

        Invoke("CongratulationsFirstEnd", audioMain.clip.length);
    }

    void CongratulationsFirstEnd()
    {
        msgLevelMovement.enabled = true;
    }

    void ObstacleTrigger()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        MSG_Obstacle_Prototype cam_ob = cam.GetComponent<MSG_Obstacle_Prototype>();

        cam_ob.original_cam_position = cam.transform.position;
        cam_ob.obstacle_int = obstacle_int;

        cam.transform.position = new Vector3(10000, 0, -10);

        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Obstacle_Active();
        cam_ob.obstacles_active(msgObstacleTrigger);
    }
}
