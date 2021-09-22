using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIItemSlot : MonoBehaviour
{
    public bool isCursor;

    public ItemSlot itemSlot;
    public RectTransform slotRect;
    public Image icon;
    public TextMeshProUGUI amount;
    public Image condition;
    public string descriptionUI;
    public int costUI;

    void Awake()
    {
        itemSlot = new ItemSlot();
    }
     
    private void Update()
    {
        if (!isCursor) return;

        transform.position = Input.mousePosition;
    }

    public void RefreshSlot()
    {
        UpdateIcon();
        UpdateAmount();

        if (!isCursor)
            UpdateConditionBar();
    }

    public void FilloutSlot(string itemName)
    {
        itemSlot = new ItemSlot(itemName);
        RefreshSlot();
    }

    public void ClearSlot()
    {
        itemSlot = new ItemSlot();
        RefreshSlot();
    }

    private void UpdateIcon()
    {
        if (itemSlot == null || !itemSlot.hasItem)
            icon.enabled = false;
        else
        {
            icon.enabled = true;
            icon.sprite = itemSlot.item.icon;
            descriptionUI = itemSlot.Tooltip;
            costUI = itemSlot.Cost;
        }
    }

    private void UpdateAmount()
    {
        //cases where amount is not needed
        if (itemSlot == null || !itemSlot.hasItem || itemSlot.amount < 2)
            amount.enabled = false;
        else
        {
            amount.enabled = true;
            amount.text = itemSlot.amount.ToString();
        }
    }

    private void UpdateConditionBar()
    {
        //cases where condition bars are not needed
        if (itemSlot == null || !itemSlot.hasItem || !itemSlot.item.isDegradable)
            condition.enabled = false;

        //else work out how much the condition bar will show
        else
        {
            condition.enabled = true;

            //Get the normalised percentage of condition (0 -1)
            float conditionPercent = (float)itemSlot.condition / (float)itemSlot.item.maxCondition;

            //multiply the max width by the percentage to get the width
            float barWidth = slotRect.rect.width * conditionPercent;

            //set the width. We have to in a Vector2 so keep the y value the same
            condition.rectTransform.sizeDelta = new Vector2(barWidth, condition.rectTransform.sizeDelta.y);

            //change color from green to red as it degrades
            condition.color = Color.Lerp(Color.red, Color.green, conditionPercent);
        }
    }
}
