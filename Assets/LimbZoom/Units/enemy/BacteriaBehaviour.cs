using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaBehaviour : MonoBehaviour
{
	public GameObject[] Target;
	public float velocity;
	private int HP;

	private Transform closestEnemy;
	
	public Rigidbody2D rb;
	Vector2 moveDirection;


    // Start is called before the first frame update
    void Start()
    {
	    HP = 2;
	    rb=GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
		closestEnemy = GetClosestEnemy();

        if (closestEnemy)
	    {
	    	Vector3 direction = (closestEnemy.position - transform.position).normalized;
	    	float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
	    	rb.rotation = angle;
	    	moveDirection = direction;
	    }
        
    }


	public Transform GetClosestEnemy()
	{
		Target = GameObject.FindGameObjectsWithTag("Enemy");
		float closestDistance = Mathf.Infinity;
		Transform targetTransform = null;

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
		return targetTransform;
	}

	// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	protected void FixedUpdate()
	{
			rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * velocity;
	}	


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ally") == true)
        {
            HP = HP - 1;
            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
