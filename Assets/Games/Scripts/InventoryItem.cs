using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour {

    public enum Item
    {
        KEY,MATCHES,KEY2
    }

    [SerializeField] public Item item;

}
