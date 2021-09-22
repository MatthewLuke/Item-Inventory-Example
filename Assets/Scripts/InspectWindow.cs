using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectWindow : MonoBehaviour
{
    [SerializeField] Text itemDescriptionText;

    public void ShowInspectWindow(ItemSlot itemSlot)
    {
        itemDescriptionText.text = itemSlot.Description;
        gameObject.SetActive(true);
    }

    public void HideInspectWindow()
    {
        gameObject.SetActive(false);
    }
}
