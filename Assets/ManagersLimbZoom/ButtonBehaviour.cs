using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class ButtonBehaviour : MonoBehaviour
{
	public GameObject[] spawnPoints;

	public Slider ResourceSlider;
	public TextMeshProUGUI resourceSliderText;
	public AudioSource buttonClickSound;

	public GameObject macrophage;
	public GameObject neutrophil;
	public GameObject denditric;
	public GameObject arty;

    public GameObject optionsPanel;
    public AudioMixer AudioMixer;
    public GameObject backToMenuButton;
    public GameObject optionsButton;




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




    private void Start()
    {
		spawnPoints = GameObject.FindGameObjectsWithTag("AllySpawnPoint");
    }




    public void EnterOptions()
    {
		buttonClickSound.Play();
        Time.timeScale = 0f;
        optionsPanel.SetActive(true);
        backToMenuButton.SetActive(false);
        optionsButton.SetActive(false);
    }

    public void CloseOptions()
    {
		buttonClickSound.Play();
		Time.timeScale = 1f;
        optionsPanel.SetActive(false);
        backToMenuButton.SetActive(true);
        optionsButton.SetActive(true);
    }




    public void Macrophage()
	{
		buttonClickSound.Play();
		int resourceCost = 25;

		if(ResourceSlider.value >= resourceCost)
		{ 

			for (int i = 0; i<spawnPoints.Length; i++)
			{
				Instantiate(macrophage, spawnPoints[i].transform.position, Quaternion.identity);
			}

			ResourceSlider.value = ResourceSlider.value - resourceCost;
			resourceSliderText.text = ResourceSlider.value.ToString() + "%";

		}
	}
	
	public void Neutrophil()
	{
		buttonClickSound.Play();
		int resourceCost = 10;

		if (ResourceSlider.value >= resourceCost)
		{ 

			for (int i = 0; i < spawnPoints.Length; i++)
			{
				Instantiate(neutrophil, spawnPoints[i].transform.position, Quaternion.identity);
			}

			ResourceSlider.value = ResourceSlider.value - resourceCost;
			resourceSliderText.text = ResourceSlider.value.ToString() + "%";

		}
	}
	
	public void Denditric()
	{
		buttonClickSound.Play();
		int resourceCost = 50;

			if (ResourceSlider.value >= resourceCost)
			{

				for (int i = 0; i < spawnPoints.Length; i++)
				{
					Instantiate(denditric, spawnPoints[i].transform.position, Quaternion.identity);
				}

				ResourceSlider.value = ResourceSlider.value - resourceCost;
				resourceSliderText.text = ResourceSlider.value.ToString() + "%";

			}
	}
	
	public void Arty()
	{
		buttonClickSound.Play();
		int resourceCost = 50;

		if (ResourceSlider.value >= resourceCost)
		{

			for (int i = 0; i < spawnPoints.Length; i++)
			{
				Instantiate(arty, spawnPoints[i].transform.position, Quaternion.identity);
			}

			ResourceSlider.value = ResourceSlider.value - resourceCost;
			resourceSliderText.text = ResourceSlider.value.ToString() + "%";

		}
	}




	public void BackToMenu()
    {
		buttonClickSound.Play();
		SceneManager.LoadScene(0);
    }

	public void Continue()
    {
		buttonClickSound.Play();
		SceneManager.LoadScene(1);
    }
}
