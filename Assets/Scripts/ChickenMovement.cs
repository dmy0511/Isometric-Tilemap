using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMovement : MonoBehaviour
{
    public float minX = 1.5f;
    public float maxX = 20.5f;
    public float minY = -3.1f;
    public float maxY = 6.4f;
    public float speed = 0.5f;
    public Sprite eggSprite;

    private bool movingRight = true;
    private bool movingUp = true;
    private int playerCollisions = 0;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.radius = 0.5f;
        circleCollider.isTrigger = false;
    } 

    private void Update()
    {
        if (playerCollisions >= 3)
        {
            Destroy(gameObject);
            return;
        }

        float movementX = (playerCollisions >= 1 ? speed * 2f : speed) * Time.deltaTime;
        float movementY = (playerCollisions >= 1 ? speed * 2f : speed) * Time.deltaTime;
        Vector3 targetPosition = transform.position;

        if (movingRight)
        {
            targetPosition.x += movementX;
            if (targetPosition.x >= maxX)
            {
                movingRight = false;
                FlipSprite();
            }
        }
        else
        {
            targetPosition.x -= movementX;
            if (targetPosition.x <= minX)
            {
                movingRight = true;
                FlipSprite();
            }
        }

        if (movingUp)
        {
            targetPosition.y += movementY;
            if (targetPosition.y >= maxY)
                movingUp = false;
        }
        else
        {
            targetPosition.y -= movementY;
            if (targetPosition.y <= minY)
                movingUp = true;
        }

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        transform.position = targetPosition;
    }

    private void FlipSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollisions++;

            if (playerCollisions == 1)
            {
                speed = 3f;
            }
            else if (playerCollisions == 2)
            {
                spriteRenderer.sprite = eggSprite;
                speed = 0f; // Stop the movement
            }
        }

        movingRight = !movingRight;
        FlipSprite();
    }
}



