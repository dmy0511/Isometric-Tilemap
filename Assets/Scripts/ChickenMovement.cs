using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMovement : MonoBehaviour
{
    public Sprite runSprite;
    public Sprite eggSprite;

    public GameObject get_Panel_2;
    public GameObject chickenObject;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    
    private float moveSpeed = 1f;
    private int collisionCount = 0;
    private Vector2 movementDirection;
    private bool isMoving = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        movementDirection = GetRandomDirection();
        rb.velocity = movementDirection * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collisionCount++;

            if (collisionCount == 1)
            {
                spriteRenderer.sprite = runSprite;
                moveSpeed = 5f;
                Flip();
            }
            else if (collisionCount == 2)
            {
                spriteRenderer.sprite = eggSprite;
                StopMotion();
            }
            else if (collisionCount == 3)
            {
                Time.timeScale = 0f;
                get_Panel_2.SetActive(true);
            }
        }
        else if (collision.gameObject.CompareTag("Collider"))
        {
            Flip();
        }
        else if (collision.gameObject.CompareTag("Chicken"))
        {
            Flip();
        }
    }

    private void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;

        movementDirection *= -1;

        rb.velocity = movementDirection * moveSpeed;
    }

    private Vector2 GetRandomDirection()
    {
        float randomAngle = UnityEngine.Random.Range(0f, 360f);

        return Quaternion.Euler(0f, 0f, randomAngle) * Vector2.right;
    }

    private void StopMotion()
    {
        rb.velocity = Vector2.zero;

        isMoving = false;
    }

    void Update()
    {
        if (isMoving)
        {
            rb.velocity = movementDirection * moveSpeed;

            if (movementDirection.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (movementDirection.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    public void OnGetButtonPressed()
    {
        Destroy(chickenObject);
        Time.timeScale = 1f;
        get_Panel_2.SetActive(false);
    }
}





