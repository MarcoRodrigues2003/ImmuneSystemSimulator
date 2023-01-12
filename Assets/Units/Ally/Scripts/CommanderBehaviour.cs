using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class CommanderBehaviour : MonoBehaviour
{
    public string enemyTag;
    public int HP;
    private float Timer;
    private SpriteRenderer thisBase;
    public AudioSource collisionSound;

    public GameObject gameOverPanel;
    public TextMeshProUGUI totalPointsLoss;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        thisBase = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer + .2f <= Time.time)
        {
            thisBase.color = Color.white;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == enemyTag)
        {
            collisionSound.Play();
            HP --;
            thisBase.color = Color.red;
            Timer = Time.time;

            if(HP <= 0)
            {
                Time.timeScale = 0f;
                totalPointsLoss.text = ES3.Load<int>("totalScore").ToString();
                gameOverPanel.SetActive(true);

                ES3.Save("totalScore", 0);
            }
        }
    }
}
