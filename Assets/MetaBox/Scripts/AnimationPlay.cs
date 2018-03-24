using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class AnimationPlay : MonoBehaviour {

    public Sprite[] sprites;
    public float animationSpeed;

    UnityEngine.UI.Image image;
    private void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
    }

    bool playing = false;
    private void OnEnable()
    {
        playing = true;
        //TODO: enable a coroutine here. It is not working for some reason (Animator is not working either)
    }

    public void OnDisable()
    {
        playing = false;
    }
    float idx = 0;
    private void Update()
    {
        
        image.sprite = sprites[(int)idx];
        
        idx = ((idx+animationSpeed) % sprites.Length);
    }
}
