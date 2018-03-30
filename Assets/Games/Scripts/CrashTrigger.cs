using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashTrigger : MonoBehaviour {

    [SerializeField] Animation animation;
    [SerializeField] DudeController controller;
    
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            animation.Play();
            this.enabled = false;
        }
    }
}
