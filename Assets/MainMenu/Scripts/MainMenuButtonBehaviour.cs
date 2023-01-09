using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonBehaviour : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject StartCanvas;



    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Options()
    {
        Debug.Log("Options");
        StartCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }

    public void BackToStartMenu()
    {
        StartCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
    }
}