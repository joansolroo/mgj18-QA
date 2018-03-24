using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour {

    [SerializeField] GameObject brokenIntro;
    [SerializeField] GameObject workingIntro;
    [SerializeField] GameObject Inbox;

    AudioSource source;
    // Use this for initialization
    void Start () {
        StartCoroutine("RunIntro");

        source = GetComponent<AudioSource>();
        source.Play();
	}
    IEnumerator RunIntro()
    {
        workingIntro.SetActive(false);
        brokenIntro.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        brokenIntro.SetActive(true);
        yield return new WaitForSeconds(8);
        brokenIntro.SetActive(false);
        yield return new WaitForSeconds(1);
        workingIntro.SetActive(true);
        yield return new WaitForSeconds(2);
        Inbox.SetActive(true);
        StartCoroutine("FadeOut");
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
