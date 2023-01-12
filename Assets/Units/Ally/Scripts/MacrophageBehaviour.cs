using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MacrophageBehaviour : MonoBehaviour
{
	public GameObject[] Target;
	public float velocity;
	public float CollisionForce;
	public int HP;
	public string[] enemyTag;
	public int score;
	public AudioSource collisionSound;

	private GameObject[] touch;

	
	private Rigidbody2D rb;
	Vector2 moveDirection;

	private Transform closestEnemy;
	private Transform targetTransform = null;

	public GameObject deadCell;
	public int chanceOfDeadCell;

	private GameObject GameManagerObject;
	private GameManager GameManagerScript;



	// Start is called before the first frame update
	void Start()
    {
	    rb=GetComponent<Rigidbody2D>();
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
		{
			GameManagerObject = GameObject.FindGameObjectWithTag("GameController");
			GameManagerScript = GameManagerObject.GetComponent<GameManager>();
		}
		touch = GameObject.FindGameObjectsWithTag("Touch1");
	}

    // Update is called once per frame
    void Update()
    {

		closestEnemy = GetClosestEnemy();
		Transform getClosestTouch = GetClosestTouch();

		if(transform.tag == "Ally" && getClosestTouch.position != new Vector3(0,0,0))
        {
			Vector2 direction = (getClosestTouch.position - transform.position);
			direction.Normalize();
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			rb.rotation = angle;
			moveDirection = direction;
		}
		else if (closestEnemy == null)
		{
			Vector2 direction = (GameObject.Find("AllyBase").transform.position - transform.position);
			direction.Normalize();
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			rb.rotation = angle;
			moveDirection = direction;
		}else
        {

			if (closestEnemy)
			{
				Vector2 direction = (closestEnemy.position - transform.position);
				direction.Normalize();
				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				rb.rotation = angle;
				moveDirection = direction;
			}

		}

	}
    

	public Transform GetClosestTouch()
    {
		float closestDistance = Mathf.Infinity;
		for (int i = 0; i < Input.touchCount; i++)
		{

			foreach (GameObject go in touch)
			{
				float actualDistance;
				actualDistance = Vector2.Distance(transform.position, go.transform.position);
				if (actualDistance < closestDistance)
				{
					closestDistance = actualDistance;
					targetTransform = go.transform;
				}
			}
		}

		return targetTransform;
	}

	public Transform GetClosestEnemy()
	{
		float closestDistance = Mathf.Infinity;
		for (int i=0; i<enemyTag.Length; i++)
        {
			Target = GameObject.FindGameObjectsWithTag(enemyTag[i]);
			

			foreach (GameObject go in Target)
			{
				float actualDistance;
				actualDistance = Vector2.Distance(transform.position, go.transform.position);
				if (actualDistance < closestDistance)
				{
					closestDistance = actualDistance;
					targetTransform = go.transform;
				}
			}
		}
		return targetTransform;
    }


    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    protected void FixedUpdate()
	{
		rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * velocity;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		for(int i=0; i<enemyTag.Length; i++)
        {
			if (collision.gameObject.CompareTag(enemyTag[i]) == true)
			{
				HP --;
				rb.AddForce(CollisionForce * 1.5f * Time.deltaTime * -moveDirection);

				collisionSound.Play();

				if (HP <= 0 && this.transform.tag != "Enemy")
				{
					Destroy(this.gameObject);

				}else if(HP <= 0 && this.transform.tag == "Enemy")
                {
					float roll = UnityEngine.Random.Range(0,101);
					if(roll <= chanceOfDeadCell)
                    {
						Instantiate(deadCell, transform.position, Quaternion.identity);
						Destroy(this.gameObject);
                    }

					if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
					{
						GameManagerScript.Score(score);
					}
					

					Destroy(this.gameObject);
				}
			}
		}
	}
}
