using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    public GameObject treePanel;
    public GameObject GetPanel;

    public Slider treeHp;

    private float maxHp = 100;
    private float curHp = 100;

    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        Time.timeScale = 1.0f;

        treeHp.value = (float)curHp / (float)maxHp;
    }

    void Update()
    {
        if (curHp <= 0)
        {
            treePanel.gameObject.SetActive(false);
            GetPanel.SetActive(true);
            Time.timeScale = 0.0f;
            curHp += 100;
        }

        HandleHp();
    }

    private void HandleHp()
    {
        treeHp.value = (float)curHp / (float)maxHp;
    }

    public void attack()
    {
        if(curHp > 0)
        {
            curHp -= 20;
            audio.Play();
        }
        else
        {
            curHp = 0;
        }    
    }

    public void get()
    {
        Time.timeScale = 1.0f;
        GetPanel.SetActive(false);

        //Sprite itemSprite = itemImage.sprite;

        //inventorySlot.sprite = itemSprite;

        //inventorySlot.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            treePanel.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            treePanel.gameObject.SetActive(false);
        }
    }
}
