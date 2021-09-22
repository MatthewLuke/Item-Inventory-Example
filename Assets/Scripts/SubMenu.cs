using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenu : MonoBehaviour
{
    public InspectWindow inspectWindow;
    public GameObject buttonConsume;
    public GameObject buttonRead;
    public GameObject buttonEquip;
    public GameObject buttonSplit;
    UIItemSlot curserItem;
    UIItemSlot currentItem;

    public void ShowItemMenu(UIItemSlot uiSlot, UIItemSlot curserSlot)
    {
        currentItem = uiSlot;
        curserItem = curserSlot;
        transform.position = uiSlot.transform.position;

        //switch between primary actions
        switch (uiSlot.itemSlot.Type)
        {
            case ItemType.Food:
                buttonConsume.SetActive(true);
                buttonEquip.SetActive(false);
                buttonRead.SetActive(false);
                break;
            case ItemType.Equipment:
                buttonConsume.SetActive(false);
                buttonEquip.SetActive(true);
                buttonRead.SetActive(false);
                break;
            case ItemType.Spell:
                buttonConsume.SetActive(false);
                buttonEquip.SetActive(false);
                buttonRead.SetActive(true);
                break;
        }

        //show if item has a stack greater than 1
        if(uiSlot.itemSlot.amount > 1) { buttonSplit.SetActive(true); }
        else { buttonSplit.SetActive(false); }

        
        gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
    }

    public void HideItemMenu()
    {
        gameObject.SetActive(false);
    }

    public void Consume()
    {
        ItemSlot.Consume(currentItem.itemSlot);

        curserItem.RefreshSlot();
        HideItemMenu();
    }

    public void Equip()
    {
        //normally supposed to equip an item but for this demo I'll make it like an attack that degrades the item.
        currentItem.itemSlot.condition -= 10;

        if (currentItem.itemSlot.condition <= 0)
            ItemSlot.Drop(currentItem.itemSlot);

        curserItem.RefreshSlot();
        HideItemMenu();
    }

    public void Split()
    {
        ItemSlot.Split(currentItem.itemSlot, curserItem.itemSlot);

        curserItem.RefreshSlot();
        HideItemMenu();

    }

    public void Inspect()
    {
        inspectWindow.ShowInspectWindow(currentItem.itemSlot);

        curserItem.RefreshSlot();
        HideItemMenu();
    }

    public void Drop()
    {
        ItemSlot.Drop(currentItem.itemSlot);

        curserItem.RefreshSlot();
        HideItemMenu();
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}

