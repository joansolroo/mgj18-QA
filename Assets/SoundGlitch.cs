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
        targetPitch = audio.pitch;

    }

    float targetPitch;
	// Update is called once per frame
	void Update () {
        if (activate)
        {
            if (Random.value > 0.05)
            {
                targetPitch = Random.Range(minPitch, maxPitch);
            }
            audio.pitch = Mathf.MoveTowards(audio.pitch,targetPitch,maxSpeed);
        }
	}
}
