using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPieces : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Vector3 moveDirection;
    public float deceleration = 5f;
    public float lifetime = 3f;

    public SpriteRenderer renderer;
    public float fadeSpeed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        // move the broken piece in a random direction
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // decelerate the broken piece
        transform.position += moveDirection * Time.deltaTime;
        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            //fadeout
            renderer.color = new Color(
                                        renderer.color.r,
                                        renderer.color.g,
                                        renderer.color.b,
                                        Mathf.MoveTowards(renderer.color.a, 0f, fadeSpeed * Time.deltaTime)
                                    );
            if (renderer.color.a == 0f)
            {
                //destroy
                Destroy(gameObject);
            }
        }
    }
}
