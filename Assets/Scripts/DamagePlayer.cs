using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // FOR OBJECTS THE PLAYER CAN WALK OVER ie. spike
    private void OnTriggerEnter2D(Collider2D other)
    {
        // remember that "isTrigger" on the box collider must be checked in the BoxCollider2D
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // remember that "isTrigger" on the box collider must be checked in the BoxCollider2D
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }

    // FOR SOLID OBJECTS PLAYERS CAN'T MOVE THROUGH ie. saw blades on a wall
    private void OnCollisionEnter2D(Collision2D other)
    {
        // remember that "isTrigger" on the box collider must be checked in the BoxCollider2D
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        // remember that "isTrigger" on the box collider must be checked in the BoxCollider2D
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }
}
