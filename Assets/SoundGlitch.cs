using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundGlitch : MonoBehaviour {

    [SerializeField] bool activate = false;
    AudioSource audio;
    [SerializeField] float minPitch = 1;
    [SerializeField] float maxPitch = 1;
    [SerializeField] float maxSpeed = 1;
    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (activate)
        {
            audio.pitch = Mathf.MoveTowards(audio.pitch,Random.Range(minPitch, maxPitch),maxSpeed);
        }
	}
}
