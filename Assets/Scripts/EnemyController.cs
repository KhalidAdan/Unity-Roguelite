using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public Rigidbody2D rigidBody;
    public float moveSpeed;

    [Header("ChasePlayer")]
    public bool shouldChasePlayer;
    public float chaseRange;
    private Vector3 moveDirection;

    [Header("Run Away")]
    public bool shouldRunAway;
    public float runAwayRange;

    [Header("Wandering")]
    public bool shouldWander;
    public float wanderLength;
    public float pauseLength;
    private float wanderCounter;
    private float pauseCounter;
    private Vector3 wanderDirection;

    [Header("Patrolling")]
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;


    [Header("Shooting")]
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public SpriteRenderer enemyBody;


    [Header("Variables")]
    private Vector3 playerPosition;
    public Animator anim;
    public int hp = 150;
    public GameObject[] deathSplatters;
    public GameObject hitEffect;
    public float shootingRange;
    public int enemyDamagedSound = 2;
    public int enemyDeathSound = 1;
    public int enemyGunSound = 13;

    // Start is called before the first frame update
    void Start()
    {
        if (shouldWander)
        {
            pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (
            enemyBody.isVisible &&
            PlayerController.instance.gameObject.activeInHierarchy
            )
        {
            MoveEnemy();
            ShootPlayer();
        } else
        {
            // stop moving enemies on player death
            rigidBody.velocity = Vector2.zero;
        }
        ControlAnimations();
    }

    private void ShootPlayer()
    {
        if (
            shouldShoot &&
            Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootingRange
            )
        {
            fireCounter -= Time.deltaTime;
            if (fireCounter <= 0)
            {
                fireCounter = fireRate;
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                AudioManager.instance.PlaySFX(enemyGunSound);
            }
        }
    }

    private void MoveEnemy()
    {
        moveDirection = Vector3.zero;

        playerPosition = PlayerController.instance.transform.position;
        if ( Vector3.Distance(transform.position, playerPosition) < chaseRange && shouldChasePlayer)
        {
            moveDirection = playerPosition - transform.position;
        } else
        {

            if (shouldWander)
            {
                if (wanderCounter > 0)
                {
                    wanderCounter -= Time.deltaTime;
                    // move the enemy
                    moveDirection = wanderDirection;
                    if (wanderCounter <= 0)
                    {
                        pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.25f);
                    }
                }
                if (pauseCounter > 0)
                {
                    pauseCounter -= Time.deltaTime;
                    if (pauseCounter <= 0)
                    {
                        wanderCounter = Random.Range(wanderLength * 0.75f, wanderLength * 1.25f);

                        wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                    }
                }
            }

            if (shouldPatrol)
            {
                moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;

                if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < 0.2f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }
                }
            }
        }

        if (shouldRunAway && Vector3.Distance(transform.position, playerPosition)< runAwayRange)
        {
            moveDirection = transform.position - playerPosition;
        }

                
        /*else
        {
            moveDirection = Vector3.zero;
        }*/
        moveDirection.Normalize();
        rigidBody.velocity = moveDirection * moveSpeed;
    }

    private void ControlAnimations()
    {
        // control animations based on movement of character
        if (moveDirection != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }   

    public void DamageEnemy(int bulletDamage)
    {
        hp -= bulletDamage;
        Instantiate(hitEffect, transform.position, transform.rotation);
        AudioManager.instance.PlaySFX(enemyDamagedSound);
        if (hp <= 0)
        {
            // destroys THIS game object
            Destroy(gameObject);

            //choose the deathsplatter and rotate it in random 90 degree increments
            int rotation = Random.Range(0, 4);

            int selected = Random.Range(0, deathSplatters.Length);
            Instantiate(
                deathSplatters[selected], 
                transform.position, 
                Quaternion.Euler(0f, 0f, rotation * 90f)
            );
            AudioManager.instance.PlaySFX(enemyDeathSound);

        }
    }
}
