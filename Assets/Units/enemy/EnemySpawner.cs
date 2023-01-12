using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Pathogen;

    public float spawnCooldown;
    private float spawnTime;

    public GameObject gameWonPanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI totalPointsWon;
    public TextMeshProUGUI totalPointsLoss;

    public float midGameScale;
    private float midGameTimer;
    public float lateGameScale;
    private float lateGameTimer;
    public float endGameScale;
    private float endGameTimer;

    private void Start()
    {
        midGameTimer = Time.time + midGameScale;
        lateGameTimer = Time.time + lateGameScale;
        endGameTimer = Time.time + endGameScale;
    }


    // Update is called once per frame
    void Update()
    {
        float roll = Random.Range(-0.2f, 0.2f);

        if (Time.time > spawnTime)
        {
            Instantiate(Pathogen, transform.position, Quaternion.identity);
            spawnTime = Time.time + spawnCooldown;
        }

        if(Time.time > midGameTimer)
        {
            spawnCooldown = 0.9f;
        }

        if(Time.time > lateGameTimer)
        {
            spawnCooldown = 0.7f;
        }

        if(Time.time > endGameTimer)
        {
            Time.timeScale = 0f;
            totalPointsWon.text = ES3.Load<int>("totalScore").ToString();
            gameWonPanel.SetActive(true);
        }
    }
}