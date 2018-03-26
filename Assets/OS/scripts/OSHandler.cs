using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSHandler : MonoBehaviour {

    [SerializeField] public GameObject unitySplash; //TODO remove later
    [SerializeField] public GameObject crash;

    [SerializeField] public GameObject desktop;
    public static OSHandler instance;

    [SerializeField] GameObject BSOD;
    [SerializeField] int timeBSOD = 2;

    [SerializeField] GameObject brokenIntro;
    [SerializeField] GameObject middleIntro;
    [SerializeField] GameObject workingIntro;
    [SerializeField] Inbox inbox;
    [SerializeField] float demonRatio = 0.05f;
    [SerializeField] bool fastBoot = false;
    AudioSource source;
    [SerializeField] AudioClip bootClip;
    [SerializeField] AudioClip crashClip;


    private void Awake()
    {
        instance = this;
    }

    GlitchController gc;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        gc = GetComponent<GlitchController>();
        BSOD.SetActive(false);

    }
    public static void Run(Download download)
    {
        Run(download.game);
    }
    public static void Run(string name)
    {
        instance.StartCoroutine("LoadGame", name);
    }
    public static void RunNow(string name)
    {
        instance.StartCoroutine("LoadWithoutSplash", name);
    }

    public static void Close(string name)
    {
        Application.UnloadLevel(name);
        instance.desktop.SetActive(true);
    }
    IEnumerator LoadGame(string name){
        desktop.SetActive(false);
        instance.unitySplash.SetActive(true);
        yield return new WaitForSeconds(2);
        AsyncOperation async = Application.LoadLevelAdditiveAsync(name);
        yield return async;
        Debug.Log("Loading complete");
        instance.unitySplash.SetActive(false);
    }
    IEnumerator LoadWithoutSplash(string name)
    {
        desktop.SetActive(false);
        yield return new WaitForSeconds(2);
        AsyncOperation async = Application.LoadLevelAdditiveAsync(name);
        yield return async;
    }
    
    public static void Crash(ProgramError error)
    {
        instance.crash.SetActive(true);
    }

    public IEnumerator RunAndCrash()
    {
        instance.unitySplash.SetActive(true);
        yield return new WaitForSeconds(2);
        Crash(new ProgramError());
    }

    public void ErrorMessageOk()
    {
        instance.crash.SetActive(false);
        ProgramExit();
    }
    public void ProgramExit()
    {
        instance.unitySplash.SetActive(false);
        desktop.SetActive(true);
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
        desktop.SetActive(true);
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
            middleIntro.SetActive(true);
            yield return new WaitForSeconds(3f*demonRatio);
            middleIntro.GetComponent<AudioLowPassFilter>().enabled = false;
            middleIntro.GetComponent<SoundGlitch>().enabled = false;
            middleIntro.GetComponent<IsGlitched>().enabled = false;
            workingIntro.SetActive(true);
            yield return new WaitForSeconds(3*(1- demonRatio));
            StartCoroutine("FadeOut");
            yield return new WaitForSeconds(2.0f);
        }
        middleIntro.SetActive(false);
        workingIntro.SetActive(false);
        inbox.gameObject.SetActive(true);
        inbox.OpenInbox();
        
    }
    IEnumerator FadeOut()
    {
        while (source.volume > 0.05)
        {
            source.volume = Mathf.MoveTowards(source.volume, 0, (source.volume) * Time.deltaTime / 2);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        source.Stop();
    }
}
