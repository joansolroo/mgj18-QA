using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerToggle))]
public class InventoryItem : MonoBehaviour
{

    TriggerToggle trigger;
    public enum Item
    {
        KEY, LIGHTER, KEY2, LEVEL_END, NOTHING
    }

    [SerializeField] public Item item;
    [SerializeField] public bool picked = false;

    [SerializeField] ChapterHandling chapterHandling;
    [SerializeField] bool endOfLevel = false;

    float StartTime;
    private void Start()
    {
        trigger = GetComponent<TriggerToggle>();
        StartTime = Time.time;
    }
    void OnTriggerStay(Collider c)
    {
        if ((Time.time> (StartTime+1)) && Input.GetKeyDown(KeyCode.Space))
        {
            if (trigger.requiredItem == Item.NOTHING || Inventory.contains(trigger.requiredItem))
            {
                Inventory.Use(trigger.requiredItem);
                Inventory.Pick(item);
                if (endOfLevel)
                {
                    chapterHandling.EndLevel();
                }
                trigger.Disable();
                this.GetComponent<Collider>().enabled = false;
                picked = true;
            }

        }
    }

}
