using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 2.0f;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    public GameObject applePanel;
    public GameObject chickenPanel;
    public GameObject fishPanel;
    public GameObject FarmPanel;
    public GameObject omgPanel;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate()
    {
        if(inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "apple_Sign")
        {
            applePanel.gameObject.SetActive(true);
        }
        else if (collision.gameObject.tag == "chicken_Sign")
        {
            chickenPanel.SetActive(true);
        }
        else if (collision.gameObject.tag == "fish_Sign")
        {
            fishPanel.SetActive(true);
        }
        else if (collision.gameObject.tag == "Farm_Sign")
        {
            FarmPanel.SetActive(true);
        }
        else if (collision.gameObject.tag == "omg_Sign")
        {
            omgPanel.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "apple_Sign")
        {
            applePanel.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "chicken_Sign")
        {
            chickenPanel.SetActive(false);
        }
        else if (collision.gameObject.tag == "fish_Sign")
        {
            fishPanel.SetActive(false);
        }
        else if (collision.gameObject.tag == "Farm_Sign")
        {
            FarmPanel.SetActive(false);
        }
        else if (collision.gameObject.tag == "omg_Sign")
        {
            omgPanel.SetActive(false);
        }
    }
}
