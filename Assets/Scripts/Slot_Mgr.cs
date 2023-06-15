using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Slot_Mgr : MonoBehaviour
{
    public GameObject Slot_Panel;

    public Text moneyText;
    public Button leverButton;
    public Image[] imageUIs;
    public Sprite[] images;

    bool isSpinning;

    int money = 1000;

    void Start()
    {
        leverButton.onClick.AddListener(OnLeverButtonClick);
    }

    private void OnLeverButtonClick()
    {
        if (!isSpinning)
        {
            if (money > 0)
            {
                StartCoroutine(Images());
                money -= 50;
                UpdateMoneyText();
            }
            else
            {
                Debug.Log("보유하신 돈이 없습니다.");
            }
        }
    }

    private IEnumerator Images()
    {
        isSpinning = true;

        for (int i = 0; i < imageUIs.Length; i++)
        {
            imageUIs[i].sprite = images[Random.Range(0, images.Length)];
        }

        float elapsedTime = 0f;
        while (elapsedTime < 3f)
        {
            for (int i = 0; i < imageUIs.Length; i++)
            {
                imageUIs[i].transform.Rotate(Vector3.forward, 30f * Time.deltaTime);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bool isSame = true;
        for (int i = 1; i < imageUIs.Length; i++)
        {
            if (imageUIs[i].sprite != imageUIs[i - 1].sprite)
            {
                isSame = false;
                break;
            }
        }

        if (isSame)
        {
            money += 100000;
            UpdateMoneyText();
            Debug.Log("대박");
        }
        else
        {
            Debug.Log("돈을 잃으셨습니다.");
        }

        isSpinning = false;
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "보유 금액 : " + money.ToString();
    }

    public void ExitBtnClick()
    {
        Slot_Panel.gameObject.SetActive(false);
    }
}
