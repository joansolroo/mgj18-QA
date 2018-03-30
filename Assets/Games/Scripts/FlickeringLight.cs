using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

    Light light;
    [SerializeField] Gradient color;
    [SerializeField] float intensity;
    [SerializeField] float speed = 1;
    private void Start()
    {
        light = GetComponent<Light>();
    }
    // Update is called once per frame
    void Update () {
        light.color = color.Evaluate((Time.time*speed)%1);
        //light.intensity = intensity * (1 + (Mathf.Sin(Time.time*speed)*0.5f));
	}
}
