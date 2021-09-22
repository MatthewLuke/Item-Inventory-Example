using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public int Id;
    public ItemType type;
    [TextArea]
    public string description;
    public string tooltip;
    public Sprite icon;

    // the max amount of this item that we can stack
    public int maxStack;

    //the sell/ buy cost of the item
    public int cost;

    //the max condition this item can be in, the the item does not degrade, set value to -1.
    public int maxCondition;

    //a quick way to find out of the item is stackable or degradable
    public bool isStackable { get { return (maxStack > 1); } }
    public bool isDegradable { get { return (maxCondition > -1); } }

}
