using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemInfo ItemInfo;
    public float ItemPickupRadius = 1f;
    public LayerMask PlayerMask;

    void Update()
    {
        if(Physics.CheckSphere(transform.position, ItemPickupRadius, PlayerMask))
            PickupItem();
    }

    void PickupItem()
    {
        Debug.Log("Picked Up An " + ItemInfo.Name);
        bool HasPickedUp = Inventory.Instance.AddItem(ItemInfo);
        if (HasPickedUp) 
            Destroy(gameObject);
    }
}
