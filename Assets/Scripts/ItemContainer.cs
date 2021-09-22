using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemContainer : MonoBehaviour
{
    public GameObject parentWindow;
    public Transform contentWindow; // the Grid layout option we're using

    GameObject SlotPrefab;

    List<ItemSlot> items = new List<ItemSlot>();

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;

    private void Start()
    {
        SlotPrefab = Resources.Load<GameObject>("Prefabs/UIItemSlot");


        #region Demo Code
        for (int i = 0; i < 40; i++)
        {
            items.Add(new ItemSlot());
        }

        Item[] tempItems = new Item[5];
        tempItems[0] = Resources.Load<Item>("Items/apple");
        tempItems[1] = Resources.Load<Item>("Items/book of warp");
        tempItems[2] = Resources.Load<Item>("Items/flask");
        tempItems[3] = Resources.Load<Item>("Items/herbs");
        tempItems[4] = Resources.Load<Item>("Items/sword");

        for (int i = 0; i < 20; i++)
        {
            int index = Random.Range(0, 5);
            int amount = Random.Range(1, tempItems[index].maxStack);
            int condition = tempItems[index].maxCondition;

            items[i] = new ItemSlot(tempItems[index].name);
            items[i].amount = amount;
            items[i].condition = condition;

        }
        #endregion


        OpenContainer(items);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            CloseContainer();
        else if (Input.GetKeyDown(KeyCode.W))
            OpenContainer(items);
    }

    List<UIItemSlot> UISlots = new List<UIItemSlot>();

    public void OpenContainer(List<ItemSlot> slots)
    {
        parentWindow.SetActive(true);

        //loop through each item
        for (int i = 0; i < slots.Count; i++)
        {
            GameObject newSlot = Instantiate(SlotPrefab, contentWindow);
            newSlot.GetComponent<RectTransform>().localPosition = GetPosition(i);

            newSlot.name = i.ToString();
            UISlots.Add(newSlot.GetComponent<UIItemSlot>());
            slots[i].AttatchUI(UISlots[i]);
        }
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }

    public void CloseContainer()
    {
        //loop through each slot and detach/ delete it
        foreach(UIItemSlot slot in UISlots)
        {
            slot.itemSlot.DetatchUI();
            Destroy(slot.gameObject);
        }

        //Clear this list and deactivate window
        UISlots.Clear();
        parentWindow.SetActive(false);
    }
}
