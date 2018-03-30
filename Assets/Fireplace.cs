using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour {

    [SerializeField] InventoryItem.Item requiredItem;
    [SerializeField] GameObject fire;
    [SerializeField] Animation animation;
    [SerializeField] GameObject keytrigger;
    bool triggered = false;
    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            if (!triggered && Input.GetKeyDown(KeyCode.Space))
            {
                if (requiredItem == InventoryItem.Item.NOTHING || Inventory.contains(requiredItem))
                {
                    Inventory.Use(requiredItem);
                    fire.SetActive(true);
                    animation.Play();
                    triggered = true;
                    StartCoroutine(RevealKey());
                }
            }
        }
    }
    IEnumerator RevealKey()
    {
        yield return new WaitForSeconds(1.5f);
        keytrigger.SetActive(true);
    }
}
