using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuButtonBehaviour : MonoBehaviour
{

    public AudioMixer AudioMixer;
    public AudioSource buttonClickSound;
    public GameObject optionsCanvas;
    public GameObject StartCanvas;

    public GameObject TutorialCanvas;
    public GameObject Tutorial1;
    public GameObject Tutorial2;
    public GameObject Tutorial3;
    public GameObject Tutorial4;
    public GameObject Tutorial5;

    private void Start()
    {
        if(ES3.FileExists())
        {
            float Value1 = ES3.Load<float>("GeneralAudio");
            float Value2 = ES3.Load<float>("MusicAudio");
            float Value3 = ES3.Load<float>("SFXAudio");

            AudioMixer.SetFloat("General", Mathf.Log10(Value1) * 20);
            AudioMixer.SetFloat("Music", Mathf.Log10(Value2) * 20);
            AudioMixer.SetFloat("SFX", Mathf.Log10(Value3) * 20);
        }
    }


    public void StartTutorial()
    {
        StartCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        TutorialCanvas.SetActive(true);
    }


    public void Tutorial_1()
    {
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(true);
    }

    public void Tutorial_2()
    {
        Tutorial2.SetActive(false);
        Tutorial3.SetActive(true);
    }

    public void Tutorial_3()
    {
        Tutorial3.SetActive(false);
        Tutorial4.SetActive(true);
    }

    public void Tutorial_4()
    {
        Tutorial4.SetActive(false);
        Tutorial5.SetActive(true);
    }

    public void Tutorial_5()
    {
        Tutorial5.SetActive(false);
        Tutorial1.SetActive(true);
        TutorialCanvas.SetActive(false);
        StartCanvas.SetActive(true);
    }

    public void StartGame()
    {
        buttonClickSound.Play();
        SceneManager.LoadScene(1);
    }


    public void Options()
    {
        buttonClickSound.Play();
        StartCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }


    public void Quit()
    {
        buttonClickSound.Play();
        Debug.Log("Exiting");
        Application.Quit();
    }


    public void BackToStartMenu()
    {
        buttonClickSound.Play();
        StartCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
    }


    public void OnChangeValueMaster(float Value)
    {
        AudioMixer.SetFloat("General", Mathf.Log10(Value) * 20);
        ES3.Save<float>("GeneralAudio", Value);
    }


    public void OnChangeValueMusic(float Value)
    {
        AudioMixer.SetFloat("Music", Mathf.Log10(Value) * 20);
        ES3.Save<float>("MusicAudio", Value);
    }


    public void OnChangeValueSFX(float Value)
    {
        AudioMixer.SetFloat("SFX", Mathf.Log10(Value) * 20);
        ES3.Save<float>("SFXAudio", Value);
    }
}