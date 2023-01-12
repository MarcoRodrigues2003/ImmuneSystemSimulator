using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DenditricCellBehaviour : MonoBehaviour
{
    public GameObject[] Target;
    public float velocity;
    public float CollisionForce;
    public int HP;
    public string enemyTag;
    public string[] avoidTag;
    public float avoidDistance;

    public AudioSource collisionSound;

    private GameObject GameManagerObject;
    private GameManager GameManagerScript;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private Transform closestDeadCell;
    private Transform closestEnemy;
    private Transform targetTransform = null;
    private Transform avoidTransform = null;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        GameManagerScript = GameManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        closestDeadCell = GetClosestDeadCell();
        closestEnemy = GetClosestEnemy();

        if (closestDeadCell == null)
        {
            Vector2 direction = (GameObject.Find("AllyBase").transform.position - transform.position);
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }
        else
        {

            if(closestDeadCell && closestEnemy == null)
            {
                Vector2 direction = (closestDeadCell.position - transform.position);
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
                moveDirection = direction;
            }
            else if(closestDeadCell && Vector2.Distance(transform.position, closestEnemy.transform.position) > avoidDistance)
            {
                Vector2 direction = (closestDeadCell.position - transform.position);
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
                moveDirection = direction;
            }
            else if(closestDeadCell && Vector2.Distance(transform.position, closestEnemy.transform.position) < avoidDistance)
            {
                Vector2 direction = (-closestEnemy.position - -transform.position);
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
                moveDirection = direction;
            }

        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * velocity;
    }


    public Transform GetClosestDeadCell()
    {
        float closestDistance = Mathf.Infinity;


        Target = GameObject.FindGameObjectsWithTag(enemyTag);


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


    public Transform GetClosestEnemy()
    {
        float closestDistance = Mathf.Infinity;
        for (int i = 0; i < avoidTag.Length; i++)
        {
            Target = GameObject.FindGameObjectsWithTag(avoidTag[i]);


            foreach (GameObject go in Target)
            {
                float actualDistance;
                actualDistance = Vector2.Distance(transform.position, go.transform.position);
                if (actualDistance < closestDistance)
                {
                    closestDistance = actualDistance;
                    avoidTransform = go.transform;
                }
            }
        }
        return avoidTransform;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < avoidTag.Length; i++)
        {
            if (collision.gameObject.CompareTag(avoidTag[i]) == true)
            {
                collisionSound.Play();
                HP--;
                rb.AddForce(CollisionForce * Time.deltaTime * -moveDirection);
                if (HP <= 0 )
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag) == true)
        {
            GameManagerScript.CellsToBoost();
            Destroy(collision.gameObject);
        }
    }

}
