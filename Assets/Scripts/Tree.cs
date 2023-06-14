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

    public Text moneyText;
    private int moneyUp;
    
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Time.timeScale = 1.0f;

        treeHp.value = (float)curHp / (float)maxHp;
        moneyUp = 0;
        UpdateMoneyText();
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
            GetComponent<AudioSource>().Play();
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
        
        if (moneyUp > -1)
        {
            moneyUp += 50;
            UpdateMoneyText();
        }
    }
    
    private void UpdateMoneyText()
    {
        moneyText.text = moneyUp.ToString() + "원";
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
