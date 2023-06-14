using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class omg : MonoBehaviour
{
    public void SceneLoad(string Hidden)
    {
        SceneManager.LoadScene(Hidden);
    }

    public GameObject EnterPanel;
    public GameObject BlockPanel;

    public void Play()
    {
        EnterPanel.gameObject.SetActive(false);
    }

    public void Exit()
    {
        BlockPanel.gameObject.SetActive(true);
        Invoke("DeletePanel", 2f);
    }

    public void DeletePanel()
    {
        BlockPanel.SetActive(false);
    }
}
