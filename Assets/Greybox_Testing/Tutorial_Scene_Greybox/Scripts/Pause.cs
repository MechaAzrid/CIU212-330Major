using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [Header("Pause UI")]
    public Transform pauseCanvas;
    public GameObject pauseButton;

    public string scene;

    public void PauseGame()
    {
        Time.timeScale = 0;

        pauseCanvas.gameObject.SetActive(true);
        pauseButton.GetComponent<Button>().interactable = false;
    }

	public void ReturnToMenu()
    {
        SceneManager.LoadScene(scene);
    }

    public void Continue()
    {
        Time.timeScale = 1;

        pauseCanvas.gameObject.SetActive(false);
        pauseButton.GetComponent<Button>().interactable = true;
    }
}
