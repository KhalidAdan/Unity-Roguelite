using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;

    public float invincibilityTime;
    private float invincibilityCount;

    public SpriteRenderer playerBody;

    public int playerDeathSound = 9;
    public int playerDamageSound = 11;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //update UI
        UIController.instance.healthSlider.maxValue = maxHealth;
        SetHealth();
    }

    private void SetHealth()
    {
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityCount > 0)
        {
            invincibilityCount -= Time.deltaTime;
            if (invincibilityCount <= 0)
            {
                // TODO: make this function flashbetween transparent and full alpha
                playerBody = PlayerController.instance.spriteRenderer;
                playerBody.color = new Color(
                    playerBody.color.r,
                    playerBody.color.g,
                    playerBody.color.b,
                    1f
                );
            }
        }
    }

    public void DamagePlayer()
    {
        if (invincibilityCount <= 0)
        {
            currentHealth--;
            invincibilityCount = invincibilityTime;

            // TODO: make this function flashbetween transparent and full alpha
            playerBody = PlayerController.instance.spriteRenderer;
            playerBody.color = new Color(
                playerBody.color.r,
                playerBody.color.g,
                playerBody.color.b,
                0.5f
            );
            AudioManager.instance.PlaySFX(playerDamageSound);

            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.gameObject.SetActive(true);
                AudioManager.instance.PlaySFX(playerDeathSound);
                AudioManager.instance.PlayGameOver();
            }
            SetHealth();
        }
    }

    public void HealPlayer(int healAmount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healAmount;
        } else
        {
            // TODO: implement a bool so you dont pickup health when your are full
            currentHealth = maxHealth;
        }
        SetHealth();
    }

    public void MakeInvincible(float length)
    {
        invincibilityCount = length;
        playerBody = PlayerController.instance.spriteRenderer;
        playerBody.color = new Color(
            playerBody.color.r,
            playerBody.color.g,
            playerBody.color.b,
            0.5f
        );
    }
}
