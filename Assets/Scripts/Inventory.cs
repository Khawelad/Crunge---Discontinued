using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance;

    void Awake()
    {
        if(Instance != null) {
            Debug.Log("Somethings wrong with inv instances.");
            return;
        }
        Instance = this;
    }
    #endregion

    public delegate void OnItemChange();
    public OnItemChange OnItemChangeCallback;

    public int InventorySpaceLimit = 20;
    public List<ItemInfo> ItemsInventory = new List<ItemInfo>();

    public bool AddItem (ItemInfo Item) {
        if (!Item.IsDefaultItem)
        {
                if (ItemsInventory.Count >= InventorySpaceLimit) {
                    Debug.Log("Not Enough Room!");
                    return false;
                }
            ItemsInventory.Add(Item);

            if (OnItemChangeCallback != null)
                OnItemChangeCallback.Invoke();
        }
        return true;
    }

    public void RemoveItem(ItemInfo Item) {
        ItemsInventory.Remove(Item);

        if (OnItemChangeCallback != null)
                OnItemChangeCallback.Invoke();
    }
}
