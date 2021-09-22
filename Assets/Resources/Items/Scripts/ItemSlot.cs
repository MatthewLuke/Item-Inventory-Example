using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public enum ItemType
{
    Food,
    Equipment,
    Spell,
    Default
}

public enum Attributes
{
    Speed,
    Intellect,
    Stamina,
    Strength
}

public enum SpellTypes
{
    StatBonus,
    Teleport,
    Healing,
    Cast
}
public class ItemSlot
{
    public Item item;

    public string Name;
    public ItemType Type;
    public int Id;
    public string Description;
    public string Tooltip;
    public int Cost;


    private int _amount;
    public int amount
    {
        get { return _amount; }
        set {
            if (item == null) _amount = 0;
            else if (value > item.maxStack) _amount = item.maxStack;
            else if (value < 1) _amount = 0;
            else _amount = value;
            RefreshUISlot();
        }
    }


    //the condition of the item(s). A slot can only stack identical items
    //items with different conditions are treated as different items
    private int _condition;
    public int condition
    {
        get { return _condition; }
        set
        {
            if (item == null) _condition = 0;
            else if (value > item.maxCondition) _condition = item.maxCondition;
            else _condition = value;
            RefreshUISlot();
        }
    }

    private UIItemSlot uiItemSlot;
    public void AttatchUI (UIItemSlot uiSlot) 
    { 
        uiItemSlot = uiSlot;
        uiItemSlot.itemSlot = this;
        RefreshUISlot();
    }
    public void DetatchUI() 
    { 
        uiItemSlot.ClearSlot();
        uiItemSlot = null;
    }
    public bool isAttachedToUI { get { return (uiItemSlot != null); } }

    public void RefreshUISlot()
    {
        //if it's not attached to a UI Item SLot there's nothing to refresh
        if (!isAttachedToUI)
            return;

        uiItemSlot.RefreshSlot();
    }


    

    //returns true if slots are the same
    public static bool Compare(ItemSlot slotA, ItemSlot slotB)
    {
        //if the items or conditions are different then thayare treated as different
        if (slotA.item != slotB.item || slotA.condition != slotB.condition)
            return false;

        return true;
    }

    public static void Swap(ItemSlot slotA, ItemSlot slotB)
    {
        //Cahse slotA's value
        ItemSlot _itemSlot = new ItemSlot();

        _itemSlot.FillInSlot(slotA);
        slotA.FillInSlot(slotB);
        slotB.FillInSlot(_itemSlot);


        slotA.RefreshUISlot();
        slotB.RefreshUISlot();
    }

    public static void Consume(ItemSlot slot)
    {
        if (slot.amount < 2)
            slot.Clear();
        else
            slot.amount--;

        slot.RefreshUISlot();
    }

    public static void Split(ItemSlot slotA, ItemSlot curserSlot)
    {
        int splitAmount = (int)(slotA.amount * 0.5f);

        slotA.amount -= splitAmount;

        curserSlot.FillInSlot(slotA);
        curserSlot.amount = splitAmount;

        slotA.RefreshUISlot();
        curserSlot.RefreshUISlot();
    }

    public void FillIn(Item itemDetails)
    {
        Name = itemDetails.itemName;
        Type = itemDetails.type;
        Cost = itemDetails.cost;
        Description = itemDetails.description;
        Tooltip = itemDetails.tooltip;
    }

    public void FillInSlot(ItemSlot slot)
    {
        item        = slot.item;
        Type        = slot.Type;
        amount      = slot.amount;
        condition   = slot.condition;
        Name        = slot.Name;
        Cost        = slot.Cost;
        Description = slot.Description;
        Tooltip     = slot.Tooltip;
    }

    public static void Drop(ItemSlot slot)
    {
        slot.Clear();
        slot.RefreshUISlot();
    }

    public void Clear()
    {
        item = null;
        amount = 0;
        condition = 0;
        RefreshUISlot();
    }

    //quick bool to check it slot is occupied
    public bool hasItem { get { return (item != null); } }

    private Item FindByName(string itemName)
    {
        itemName = itemName.ToLower(); //make sure the string is lower case (and file names too)
        Item _item = Resources.Load<Item>(string.Format("Items/{0}", itemName)); //Load item from Resources folder

        if (_item == null)
        {
            Debug.LogWarning(string.Format("Could not find \"{0}\". Item slot is Empty.", itemName));
        }
        return _item;
    }

    public ItemSlot(string itemName, int _amount = 1, int _condition = 0)
    {
        Item _item = FindByName(itemName); //Get our Item

        if (_item == null)
        {
            item = null;
            amount = 0;
            condition = 0;
            return;
        }
        else
        {
            item = _item;
            amount = _amount;
            condition = _condition;
            FillIn(item);
        }
    }

    public ItemSlot()
    {
        item = null;
        amount = 0;
        condition = 0;
        Name = "";
        Cost = 0;
        Description = "";
        Tooltip = "";
    }

}
