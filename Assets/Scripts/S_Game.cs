using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Game : MonoBehaviour
{
    public GameObject Slot_Panel;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Slot_Panel.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Slot_Panel.gameObject.SetActive(false);
        }
    }
}
