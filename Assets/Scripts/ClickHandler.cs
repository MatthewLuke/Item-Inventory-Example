using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class ClickHandler : MonoBehaviour
{
    GraphicRaycaster raycaster;
    PointerEventData pointer;
    public EventSystem eventSystem;

    public UIItemSlot curser;
    public SubMenu subMenu;
    public ItemTooltip tooltip;


    void Awake()
    {
        raycaster = GetComponent<GraphicRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        pointer = new PointerEventData(eventSystem);
        pointer.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointer, results);

        if (results.Count > 0 && results[0].gameObject.tag == "UIItemSlot" && subMenu.transform.gameObject.activeSelf == false)
        {
            if(results[0].gameObject.GetComponent<UIItemSlot>().itemSlot.hasItem)
                tooltip.ShowTooltip(results[0].gameObject.GetComponent<UIItemSlot>().itemSlot, results[0].gameObject.transform);
        } else
        {
            tooltip.HideToolTip();
        }



        if (Input.GetMouseButtonDown(0))
        {
            
            if (results.Count > 0 && results[0].gameObject.tag == "UIItemSlot" && subMenu.transform.gameObject.activeSelf == false)
                ProcessClick(results[0].gameObject.GetComponent<UIItemSlot>());
        }

        if (Input.GetMouseButtonDown(1))
        {

            if (results.Count > 0 && results[0].gameObject.tag == "UIItemSlot")
            {
                if (!curser.itemSlot.hasItem && results[0].gameObject.GetComponent<UIItemSlot>().itemSlot.hasItem)
                {
                    subMenu.ShowItemMenu(results[0].gameObject.GetComponent<UIItemSlot>(), curser);
                }
            }

        }
    }

    //if (itemsDisplayed[obj].ID >= 0 && player.mouseItem.obj == null) tooltip.ShowTooltip(itemsDisplayed[obj].item, obj);
    void ProcessClick (UIItemSlot clicked)
    {
        if(clicked == null)
        {
            Debug.Log("UI Element tagged as UIITemSlot  but has no UI Itemslot component");
            return;
        }

        //If itemSlots are different the they swap
        if (!ItemSlot.Compare(curser.itemSlot, clicked.itemSlot)) 
        {
            ItemSlot.Swap(curser.itemSlot, clicked.itemSlot);
            curser.RefreshSlot();
            return;
        }
        else
        {
            //If the slots are empty, return
            if (!curser.itemSlot.hasItem)
                return;
            //If they are not stackable, return
            if (!curser.itemSlot.item.isStackable)
                return;
            //If the clicked stack is full, return
            if (clicked.itemSlot.amount == clicked.itemSlot.item.maxStack)
                return;

            //Otherwise we add up the amounts and put as much as we can in the clicked slot, leaving the rest with the curser
            int total = curser.itemSlot.amount + clicked.itemSlot.amount;
            int maxStack = curser.itemSlot.item.maxStack;

            if (total <= maxStack)
            {
                clicked.itemSlot.amount = total;
                curser.itemSlot.Clear();
            }
            else
            {
                clicked.itemSlot.amount = maxStack;
                curser.itemSlot.amount = total - maxStack;
            }

            curser.RefreshSlot();
        }
    }
}
