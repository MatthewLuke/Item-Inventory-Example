using UnityEngine;

public static class MenuManager
{
    public static bool IsInitialised { get; private set; }
    public static GameObject itemsMenu, itemDescription;

    public static void Init()
    {
        GameObject canvas = GameObject.Find("Canvas");
        itemsMenu = canvas.transform.Find("ItemsMenu").gameObject;
        itemDescription = canvas.transform.Find("ItemDescription").gameObject;

        IsInitialised = true;
    }

    public static void OpenMenu(Menu menu, GameObject callingMenu = null)
    {
        if (!IsInitialised)
            Init();

        switch (menu)
        {
            case Menu.Item_Options_Menu:
                itemsMenu.SetActive(true);
                break;
            case Menu.Item_Description:
                itemDescription.SetActive(true);
                break;
        }
        if (callingMenu)
            callingMenu.SetActive(false);
    }
}
