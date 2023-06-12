using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    BagUI bagui;

    public GameObject BagPanel;
    bool activeBagUI = false;

    public Slot[] slots;
    public Transform slotHolder;

    private void Start()
    {
        bagui = BagUI.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        bagui.onSlotCountChange += SlotChange;
        BagPanel.SetActive(activeBagUI);
    }

    private void SlotChange(int val)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < bagui.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }

    public void Baag()
    {
        activeBagUI = !activeBagUI;
        BagPanel.SetActive(activeBagUI);
    }

    public void AddSlot()
    {
        bagui.SlotCnt++;
    }
}
