using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public Rigidbody2D rigidBody;
    public float moveSpeed;

    public float chaseRange;
    private Vector3 moveDirection;
    private Vector3 playerPosition;

    public Animator anim;

    public int hp = 150;

    public GameObject[] deathSplatters;

    public GameObject hitEffect;

    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public SpriteRenderer enemyBody;

    public float shootingRange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyBody.isVisible)
        {
            MoveEnemy();
            ShootPlayer();
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
            }
        }
    }

    private void MoveEnemy()
    {
        playerPosition = PlayerController.instance.transform.position;
        if ( Vector3.Distance(transform.position, playerPosition) < chaseRange )
        {
            moveDirection = playerPosition - transform.position;
        } else
        {
            moveDirection = Vector3.zero;
        }
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

        }
    }
}
