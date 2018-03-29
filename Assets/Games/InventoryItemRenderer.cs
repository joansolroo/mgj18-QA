using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerToggle))]
public class InventoryItemRenderer : MonoBehaviour {

    [SerializeField] public InventoryItem.Item item;
    TriggerToggle toggle;

    private void Start()
    {
        toggle = GetComponent<TriggerToggle>();
        toggle.Disable();
    }
    public void Enable()
    {
        toggle.Enable();
    }
    public void Disable()
    {
        toggle.Disable();
    }
}
