using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Slider ResourceSlider;
    public TextMeshProUGUI resourceSliderText;
    public GameObject[] numberOfBCells;
    public Button[] troopButton;
    private int numberOfDeadCells;
    public float resourceBoost;
    public int extraHelp;
    public TextMeshProUGUI deadCellsCounter;
    public int resourceBeginValue;

    public GameObject macrophage;
    public GameObject[] AllySpawnPoints;

    private int totalScore;
    public float Timer;
    public float denditricAllowedTime;

    // Start is called before the first frame update
    void Start()
    {
        ResourceSlider.value = resourceBeginValue;
        resourceSliderText.text = ResourceSlider.value.ToString() + "%";
        AllySpawnPoints = GameObject.FindGameObjectsWithTag("AllySpawnPoint");

	    Timer = Time.time;
	    if(ES3.FileExists() == true)
	    {
	    	totalScore = ES3.Load<int>("totalScore");
	    }
	    else
	    {
	    	totalScore = 0;
	    }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer + 1 <= Time.time)
        {
            ResourceSlider.value = ResourceSlider.value + 5;
            resourceSliderText.text = ResourceSlider.value.ToString() + "%";
            Timer = Time.time;
        }

        numberOfBCells = GameObject.FindGameObjectsWithTag("BCell");

        if (numberOfBCells.Length >= 5)
        {
            troopButton[3].interactable = false;
        } else
        {
            troopButton[3].interactable = true;
        }

        if (Timer > denditricAllowedTime)
        {
            troopButton[2].interactable = true;
        }
        else
        {
            troopButton[2].interactable = false;
        }

    }

    public void CellsToBoost()
    {
        numberOfDeadCells += 1;

        if (numberOfDeadCells >= 30 && ResourceSlider.value > 25)
        {
            ResourceSlider.value += 20;
            numberOfDeadCells = 0;
        }
        else if (numberOfDeadCells >= 30 && ResourceSlider.value <= 25)
        {
            for (int i = 0; i < extraHelp; i++)
            {
                for (int j = 0; j < AllySpawnPoints.Length; i++)
                {
                    Instantiate(macrophage, AllySpawnPoints[j].transform.position, Quaternion.identity);
                }
            }
        }

        deadCellsCounter.text = numberOfDeadCells.ToString() + "/30";

    }

    public void Score (int score)
    {
        totalScore += score;
        ES3.Save("totalScore", totalScore);
    }
}
