using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    [SerializeField] InventoryItem[] items;
    Dictionary<InventoryItem.Item, InventoryItem> itemsIndexed;

    private void Start()
    {
        foreach(InventoryItem i in items)
        {

        }
    }

}
