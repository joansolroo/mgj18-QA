using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] List<InventoryItem.Item> items = new List<InventoryItem.Item>();
    [SerializeField] InventoryItemRenderer[] itemRenderer;
    static Inventory instance;
    private void Awake()
    {
        instance = this;
    }
    public static void Pick(InventoryItem.Item item)
    {
        instance.items.Add(item);

        foreach (InventoryItemRenderer ir in instance.itemRenderer)
        {
            if (ir.item == item)
            {
                ir.Enable();
                break;
            }
        }
    }
    public static void Use(InventoryItem.Item item)
    {
        if (item == InventoryItem.Item.NOTHING)
        {
            return;
        }
        else
        {
            instance.items.Remove(item);

            foreach (InventoryItemRenderer ir in instance.itemRenderer)
            {
                if (ir.item == item)
                {
                    ir.Disable();
                    break;
                }
            }
        }
    }
    void OnDisable()
    {
        items.Clear();
    }
    public static bool contains(InventoryItem.Item item)
    {
        return instance.items.Contains(item);
    }
}
