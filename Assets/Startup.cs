using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(GlitchController))]
public class Startup : MonoBehaviour {

    [SerializeField] GameObject brokenIntro;
    [SerializeField] GameObject workingIntro;
    [SerializeField] GameObject Inbox;

    [SerializeField] bool fastBoot = false;
    AudioSource source;
    GlitchController gc;
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        gc = GetComponent<GlitchController>();
        StartCoroutine("RunIntro");

	}
    IEnumerator RunIntro()
    {
        if (!fastBoot)
        {
            source.Play();
            workingIntro.SetActive(false);
            brokenIntro.SetActive(false);
            yield return new WaitForSeconds(1f);
            brokenIntro.SetActive(true);
            yield return new WaitForSeconds(6.3f);
            gc.Activate();
            yield return new WaitForSeconds(1.1f);
            gc.Deactivate();
            brokenIntro.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            workingIntro.SetActive(true);
            yield return new WaitForSeconds(2.75f);
            StartCoroutine("FadeOut");
        }

        Inbox.SetActive(true);
        yield return new WaitForSeconds(2.0f);
    }
    IEnumerator FadeOut()
    {
        while (source.volume > 0)
        {
            source.volume = Mathf.MoveTowards(source.volume, 0, Time.deltaTime/5);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
