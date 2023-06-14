using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Game : MonoBehaviour
{
    public GameObject Game_Panel;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Game_Panel.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Game_Panel.gameObject.SetActive(false);
        }
    }
}
