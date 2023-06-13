using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class ChickenMovement : MonoBehaviour
{
    public float minX = 1.5f;
    public float maxX = 20.5f;
    public float minY = -3.1f;
    public float maxY = 6.4f;
    public float speed = 0.5f;
    public Sprite eggSprite;
    public Sprite runSprite;
    public GameObject GetPanel2;

    private bool movingRight = true;
    private bool movingUp = true;
    private int playerCollisions = 0;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private Rigidbody2D rb;

    private void Start()
    {
        movingRight = (Random.Range(0, 2) == 1) ? true : false;
        movingUp = (Random.Range(0, 2) == 1) ? true : false;

        Time.timeScale = 1.0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); // Added this line to get the Rigidbody2D component
    }

    private void Update()
    {
        if (playerCollisions >= 3)
        {
            Destroy(gameObject);
            GetPanel2.SetActive(true);
            Time.timeScale = 0.0f;
            return;
        }

        Vector3 targetPosition = transform.position;
        float movementX = (playerCollisions >= 1 ? speed * 2f : speed) * Time.deltaTime;
        float movementY = (playerCollisions >= 1 ? speed * 2f : speed) * Time.deltaTime;

        float horizontalDirection = movingRight ? 1f : -1f;
        float verticalDirection = movingUp ? 1f : -1f;

        targetPosition.x += horizontalDirection * movementX;
        targetPosition.y += verticalDirection * movementY;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        transform.position = targetPosition;

        Vector3 moveDirection = targetPosition - transform.position;
        FlipSprite(moveDirection.normalized);
    }

    private void FlipSprite(bool facingRight)
    {
        spriteRenderer.flipX = !facingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollisions++;

            if (playerCollisions == 1)
            {
                spriteRenderer.sprite = runSprite;
                speed = 2f;
            }
            else if (playerCollisions == 2)
            {
                spriteRenderer.sprite = eggSprite;
                speed = 0f;
            }
        }
        else if (collision.gameObject.CompareTag("Tilemap"))
        {
            movingRight = !movingRight;
            FlipSprite(movingRight);
        }
    }

    public void get2()
    {
        Time.timeScale = 1.0f;
        GetPanel2.SetActive(false);
    }
    
    private void FlipSprite(Vector3 direction)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false; // Not flipped
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true; // Flipped horizontally
        }
    }
}



