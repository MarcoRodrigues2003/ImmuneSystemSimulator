using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BCellBehaviour : MonoBehaviour
{
    public GameObject[] target;
    public GameObject goToTarget;
    public string[] enemyTag;
    public float velocity;
    public int HP;
    public float minDistance;
    public float collisionForce;
    private float distance;
    private Vector2 moveDirection;
    public AudioSource collisionSound;

    private Rigidbody2D rb;

    private Transform closestEnemy;

    public GameObject AntiBody;

    public float ABCooldown;
    private float ABTimer;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0,0);
        goToTarget = GameObject.FindGameObjectWithTag("BCellTarget");
        ABTimer = Time.time;
    }




    // Update is called once per frame
    void Update()
    {
        closestEnemy = GetClosestEnemy();

        if(closestEnemy == null)
        {
            if(Vector2.Distance(transform.position, goToTarget.transform.position) < 0.02f)
            {
                transform.position = goToTarget.transform.position;
            }else
            {
                Vector2 direction = goToTarget.transform.position - transform.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
                moveDirection = direction;
                velocity = 0.6f;
            }
        }
        else
        {

            distance = Vector2.Distance(transform.position, closestEnemy.transform.position);

            if (distance > minDistance && closestEnemy)
            {
                Vector2 direction = closestEnemy.position - transform.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
                moveDirection = direction;
                velocity = 0.6f;
            }
            else if (distance <= minDistance)
            {
                Vector2 direction = -closestEnemy.position - -transform.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
                moveDirection = direction;
                velocity = 0.5f;
            }
        }


        if(Time.time > ABTimer + ABCooldown && closestEnemy != null)
        {
            Instantiate(AntiBody, transform.position, Quaternion.identity);
            ABTimer = Time.time;
        }


    }



    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * velocity;
    }



    public Transform GetClosestEnemy()
    {
        Transform targetTransform = null;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < enemyTag.Length; i++)
        {
            target = GameObject.FindGameObjectsWithTag(enemyTag[i]);

            foreach (GameObject go in target)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < enemyTag.Length; i++)
        {
            if (collision.gameObject.CompareTag(enemyTag[i]) == true)
            {
                collisionSound.Play();
                HP--;
                rb.AddForce(collisionForce * Time.deltaTime * -moveDirection);
                if (HP <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}