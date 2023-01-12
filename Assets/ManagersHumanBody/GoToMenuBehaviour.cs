using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToMenuBehaviour : MonoBehaviour
{
    public GameObject optionsPanel;
    public AudioMixer AudioMixer;
    public GameObject backToMenuButton;
    public GameObject optionsButton;


    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void EnterOptions()
    {
        Time.timeScale = 0f;
        optionsPanel.SetActive(true);
        backToMenuButton.SetActive(false);
        optionsButton.SetActive(false);
    }

    public void CloseOptions()
    {
        Time.timeScale = 1f;
        optionsPanel.SetActive(false);
        backToMenuButton.SetActive(true);
        optionsButton.SetActive(true);
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
