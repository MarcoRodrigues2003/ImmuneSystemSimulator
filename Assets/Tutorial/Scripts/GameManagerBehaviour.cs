using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerBehaviour : MonoBehaviour
{
    public float rollChance;
    public float lacerationChance = 25f;
    public float infectionChance = 50f;

    public float initialRollCooldown = 5f;
    public float rollCooldown = 1f;
    public float battleRollCooldown = 5f;
    public float limbRoll;

    private bool canroll = false;

    private float timeStampRoll;
	private float timeStampCanRoll;
    
	private int LacerationLimit;
	private int	InfectionLimit;

    public Button[] buttons;
	

    void Start()
    {
        timeStampRoll = Time.time + initialRollCooldown;
        timeStampCanRoll = Time.time + initialRollCooldown;
        canroll = true;
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = false;
        }
    }


    // Update is called once per frame
    void Update()
    {

        if(timeStampCanRoll <= Time.time)
        {
            canroll = true;
        }
        

        if (timeStampRoll <= Time.time && canroll == true)
        {
            rollChance = Random.Range(0, 100);
	        if (rollChance <= lacerationChance && rollChance >= 0 && LacerationLimit < 3)
            {
                Debug.Log("Laceration");
                canroll = false;
                timeStampRoll = Time.time + battleRollCooldown;
	            timeStampCanRoll = Time.time + battleRollCooldown;
	            InfectionLimit = 0;
	            LacerationLimit = LacerationLimit + 1;
                limbRoll = Random.Range(0, 100);
                if(limbRoll <= 12.5f)
                {
                    buttons[3].enabled = true;
                }else if(limbRoll > 12.5f && limbRoll <= 25f)
                {
                    buttons[4].enabled = true;
                }else if(limbRoll > 25f && limbRoll <= 37.5f)
                {
                    buttons[5].enabled = true;
                }else if(limbRoll > 37.5f && limbRoll <= 50f)
                {
                    buttons[6].enabled = true;
                }else if(limbRoll > 50f && limbRoll <= 62.5f)
                {
                    buttons[7].enabled = true;
                }else if(limbRoll > 62.5f && limbRoll <= 75f)
                {
                    buttons[8].enabled = true;
                }else if(limbRoll > 75f && limbRoll <= 87.5f)
                {
                    buttons[9].enabled = true;
                }else
                {
                    buttons[10].enabled = true;
                }
            }
	        else if (rollChance <= infectionChance && rollChance > 25 && InfectionLimit < 3)
            {
                Debug.Log("Infection");
                canroll = false;
                timeStampRoll = Time.time + battleRollCooldown;
	            timeStampCanRoll = Time.time + battleRollCooldown;
	            LacerationLimit = 0;
	            InfectionLimit = InfectionLimit + 1;
                limbRoll = Random.Range(0, 100);
                if(limbRoll <= 33f)
                {
                    buttons[0].enabled = true;
                }else if(limbRoll > 33f && limbRoll <= 66f)
                {
                    buttons[1].enabled = true;
                }else
                {
                    buttons[2].enabled = true; 
                }
            }
            else
            {
                Debug.Log("Nothing");
                canroll = false;
                timeStampRoll = Time.time + rollCooldown;
                timeStampCanRoll = Time.time + rollCooldown;
            }
        }
    }

    public void LimbClick(string InjuryType)
    {
        string injuryType = InjuryType;

        if (injuryType == "Laceration")
        {
            SceneManager.LoadScene(2);
        }
        
    }
}
