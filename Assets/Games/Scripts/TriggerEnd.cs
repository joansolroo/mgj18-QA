using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnd : MonoBehaviour {

    [SerializeField] AudioSource audio;
    [SerializeField] Animation animation;
    [SerializeField] ChapterHandling handler;
    void OnTriggerEnter(Collider c)
    {
        handler.EndLevel();
        animation.wrapMode = WrapMode.Once;
        this.enabled = false;

    }
}
