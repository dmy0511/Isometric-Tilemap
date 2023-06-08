using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    public GameObject BagPanel;
    bool activeBagUI = false;

    private void Start()
    {
        BagPanel.SetActive(activeBagUI);
    }

    public void BagUI()
    {
        activeBagUI = !activeBagUI;
        BagPanel.SetActive(activeBagUI);
    }
}
