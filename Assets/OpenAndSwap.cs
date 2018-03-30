using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndSwap : MonoBehaviour {

    [SerializeField] GameObject thisContainer;
    [SerializeField] GameObject swap;
    TriggerToggle trigger;
    public enum Item
    {
        KEY, LIGHTER, KEY2, LEVEL_END, NOTHING
    }

    private void Start()
    {
        trigger = GetComponent<TriggerToggle>();

    }
    void OnTriggerStay(Collider c)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (trigger.requiredItem == InventoryItem.Item.NOTHING || Inventory.contains(trigger.requiredItem))
            {
                Inventory.Use(trigger.requiredItem);

                thisContainer.SetActive(false);
                swap.SetActive(true);
            }

        }
    }
}
