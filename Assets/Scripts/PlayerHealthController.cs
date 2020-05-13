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
    private bool isInvincible;

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
                // make playercontroller function called removeInvincibility
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

            // make playercontroller function called setInvinciblity
            // TODO: make this function flashbetween transparent and full alpha
            playerBody = PlayerController.instance.spriteRenderer;
            playerBody.color = new Color(
                playerBody.color.r,
                playerBody.color.g,
                playerBody.color.b,
                0.5f
            );

            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.gameObject.SetActive(true);
            }
            SetHealth();
        }
    }
}
