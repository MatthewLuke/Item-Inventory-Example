using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemSlotText;
    [SerializeField] Text itemCostText;

    public void ShowTooltip(ItemSlot itemSlot, Transform slotPos)
    {
        itemNameText.text = itemSlot.Name;
        itemSlotText.text = itemSlot.Tooltip;

        if(itemSlot.Cost != 0) itemCostText.text = itemSlot.Cost.ToString() + "g"; else itemCostText.text = "";

        transform.position = slotPos.position;

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
