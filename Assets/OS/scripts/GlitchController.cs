using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchController : MonoBehaviour {

    [SerializeField] AsciiArtFx asciifx;
    [SerializeField] Kino.DigitalGlitch digital;
    [SerializeField] Kino.AnalogGlitch analog;

    public static GlitchController instance;

    private void OnEnable()
    {
        instance = this;
    }
    public void Activate(float intensity = 1, bool vSync = false)
    {
        digital.enabled = true;
        analog.enabled = true;

        digital.intensity = Mathf.MoveTowards(digital.intensity, 0.012f*intensity, Time.deltaTime * 5);
        analog.colorDrift = Mathf.MoveTowards(analog.colorDrift, 0.047f*intensity, Time.deltaTime * 5);
        if (vSync)
        {
            analog.verticalJump = Mathf.MoveTowards(analog.verticalJump, 0.1f * intensity, Time.deltaTime * 5);
        }
        analog.scanLineJitter = Mathf.MoveTowards(analog.scanLineJitter, 0.05f*intensity, Time.deltaTime * 5);
    }

    public void Deactivate()
    {
        digital.enabled = false;
        analog.enabled = false;
    }
    public void Spike()
    {
        StartCoroutine("RunSpike");
    }
    public bool spike = false;
    private void Update()
    {
        if (spike)
        {
            spike = false;
            Spike();
        }
    }
    IEnumerator RunSpike()
    {
        Activate();
        float timer = 0;
        while (timer<0.15f)
        {
            digital.intensity = Mathf.MoveTowards(digital.intensity, 0.5f, Time.deltaTime*5);
            analog.colorDrift = Mathf.MoveTowards(analog.colorDrift, 0.2f, Time.deltaTime * 5);
            analog.scanLineJitter = Mathf.MoveTowards(analog.scanLineJitter, 0.2f, Time.deltaTime * 5);
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        timer = 0;
        while (timer < 0.25f)
        {
            digital.intensity = Mathf.MoveTowards(digital.intensity, 0, Time.deltaTime);
            analog.colorDrift = Mathf.MoveTowards(analog.colorDrift, 0f, Time.deltaTime);
            analog.scanLineJitter = Mathf.MoveTowards(analog.scanLineJitter, 0f, Time.deltaTime);
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Deactivate(); 

    }
}
