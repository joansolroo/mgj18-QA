﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DudeController : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 5;
    [SerializeField] float movementSpeed = 5;

    [SerializeField] AudioClip walk;
    AudioSource audio;
    // Use this for initialization

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }


    [SerializeField] Vector2 speed = Vector2.zero;
    bool step = false;
    // Update is called once per frame
    void Update()
    {
        float dr = Input.GetAxis("Horizontal");
        float dx = Input.GetAxis("Vertical");
        speed.x = dx; speed.y = dr;
        // Debug.Log("dr:" + dx + " dy:" + dx);
        Vector3 rotation = gameObject.transform.localEulerAngles;
        rotation.y += dr * rotationSpeed * Time.deltaTime;
        float sin = Mathf.Sin(Time.time * 10);
        if (!step && Mathf.Abs(dx) > 0 && Mathf.Abs(sin) >=0.98f)
        {
            audio.clip = walk;
            audio.Play();
            step = true;
        }
        else if (Mathf.Abs(sin) < 0.3)
        {
            step = false;
            audio.Stop();
        }
        rotation.z = Mathf.Lerp(0, sin * 35 / 2, Mathf.Abs(dx));
        gameObject.transform.position = gameObject.transform.TransformPoint(0, 0, -dx * movementSpeed * Time.deltaTime);
        gameObject.transform.localEulerAngles = rotation;
    }
}
