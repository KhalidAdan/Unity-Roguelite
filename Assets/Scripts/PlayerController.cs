using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    // controls the moveSpeed of the character, in Unity 5 seems ok
    public float moveSpeed;
    // dash speed
    private float activeMoveSpeed;
    public float dashSpeed = 12f, dashLength = 0.5f, dashCooldown = 1f, dashInviciblity = 0.5f;
    private float dashCounter, dashCoolCounter;
    // holds a reference to ther rigidbody assigned to the player object
    public Rigidbody2D rigidBody;
    // holds a reference to the animator
    public Animator anim;
    // holds a reference to the gun hand gameobject
    public Transform gunArm;
    // the player input
    private Vector2 moveInput;
    // the main camera
    private Camera cam;

    public GameObject bulletToFire;
    public Transform firePoint;
    // make this on a PER GUN basis
    public float timeBetweenShots;
    private float shotCounter;

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        //used so other objects can grab the player position
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotateGunArm();
        ShootGun();
        Dash();
        ControlAnimations();
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // stop dash spam by checking if cooldowns are available
            if (dashCoolCounter <= 0 && dashCoolCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                anim.SetTrigger("dash");
                PlayerHealthController.instance.MakeInvincible(dashInviciblity);
            }
        }
        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }
        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    private void MovePlayer()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        //Normalize horizontal and vertical movement
        moveInput.Normalize();

        rigidBody.velocity = moveInput * activeMoveSpeed;
    }

    private void RotateGunArm()
    {

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = cam.WorldToScreenPoint(
                                transform.localPosition
                              );

        // flip character model if gun points behind character
        if (mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }
        //rotate gun arm
        Vector2 offset = new Vector2(
            mousePos.x - screenPoint.x,
            mousePos.y - screenPoint.y
        );
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);

    }
    private void ShootGun()
    {
        // left mb = 0, right = 1, middle = 2
        if (Input.GetMouseButtonDown(0))
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }
        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime; 
            if (shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }
    }

    private void ControlAnimations()
    {
        // control animations based on movement of character
        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
}
