using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampToggle : MonoBehaviour {

    [SerializeField] bool active = false;
    [SerializeField] GameObject lights;

    void OnTriggerStay(Collider c)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            active = !active;
            lights.SetActive(active);
        }
    }
}
