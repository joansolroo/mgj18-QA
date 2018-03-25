using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent (typeof(GlitchController))]
public class Startup : MonoBehaviour {

    [SerializeField] GameObject BSOD;

    [SerializeField] GameObject brokenIntro;
    [SerializeField] GameObject workingIntro;
    [SerializeField] Inbox inbox;

    [SerializeField] bool fastBoot = false;
    AudioSource source;
    [SerializeField] AudioClip bootClip;
    [SerializeField] AudioClip crashClip;

    GlitchController gc;
    [SerializeField] int timeUntilCrash = 10;
    [SerializeField] int timeBeforeBSOD = 2;
    [SerializeField] int timeBSOD = 10;
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        gc = GetComponent<GlitchController>();

        StartCoroutine("RunIntro");

	}
    IEnumerator RunIntro()
    {
        BSOD.SetActive(false);
        OSHandler.Run("world1");
        yield return new WaitForSeconds(timeUntilCrash);
        gc.Activate(4);
        yield return new WaitForSeconds(timeBeforeBSOD);
        StartCoroutine("HardCrash");
        OSHandler.Close("world1");
    }

    IEnumerator HardCrash()
    {
        BSOD.SetActive(true);
        source.clip = crashClip;
        source.Play();
        yield return new WaitForSeconds(timeBSOD);
        BSOD.SetActive(false);

        StartCoroutine("Reboot");
    }
    IEnumerator Reboot()
    {
        gc.Deactivate();
        inbox.gameObject.SetActive(false);
        if (!fastBoot)
        {
            source.clip = bootClip;
            source.Play();
            workingIntro.SetActive(false);
            brokenIntro.SetActive(false);
            yield return new WaitForSeconds(1f);
            brokenIntro.SetActive(true);
            yield return new WaitForSeconds(6.3f);
            gc.Activate(2);
            yield return new WaitForSeconds(1.1f);
            gc.Deactivate();
            brokenIntro.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            workingIntro.SetActive(true);
            yield return new WaitForSeconds(2.75f);
            StartCoroutine("FadeOut");
        }
        workingIntro.SetActive(false);
        inbox.gameObject.SetActive(true);
        inbox.OpenInbox();
        yield return new WaitForSeconds(2.0f);
    }
    IEnumerator FadeOut()
    {
        while (source.volume > 0.05)
        {
            source.volume = Mathf.MoveTowards(source.volume, 0, (source.volume)*Time.deltaTime/2);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        source.Stop();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
