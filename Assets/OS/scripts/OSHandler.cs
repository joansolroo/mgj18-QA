using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSHandler : MonoBehaviour
{

    [SerializeField] public GameObject unitySplash; //TODO remove later
    [SerializeField] public GameObject crash;

    [SerializeField] public GameObject desktop;
    public static OSHandler instance;

    [SerializeField] GameObject BSOD;
    [SerializeField] int timeBSOD = 2;

    [SerializeField] GameObject brokenIntro;
    [SerializeField] GameObject middleIntro;
    [SerializeField] GameObject workingIntro;
    [SerializeField] GameObject loginScreen;
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
    MailRenderer callback; //TODO this is a hack, fix it
    public static void Run(Download download)
    {
        instance.callback = download.mailRenderer;
        instance.StartCoroutine("LoadGame", download.game);
    }
    static void Run(string name)
    {
        //instance.callback = null;
        instance.StartCoroutine("LoadGame", name);
    }
    public static void RunNow(string name)
    {
       // instance.callback = null;
        instance.StartCoroutine("LoadWithoutSplash", name);
    }

    string lastScene;
    public static void Close(string name)
    {
        instance.StartCoroutine(instance.CloseAtTheEndOfTheFrame(instance.lastScene));
    }
    public static void CloseLast()
    {
        Close(instance.lastScene);
    }

    IEnumerator CloseAtTheEndOfTheFrame(string name)
    {
        yield return new WaitForEndOfFrame();
        Application.UnloadLevel(name);
        if(callback!=null)callback.AttachmentWasOpen();
    }
    public static void ShowDesktop()
    {
        instance.desktop.SetActive(true);
    }
    IEnumerator LoadGame(string name)
    {
        desktop.SetActive(false);
        instance.unitySplash.SetActive(true);
        //yield return new WaitForSeconds(2);
        /*AsyncOperation async =*/ Application.LoadLevelAdditive(name);
        //yield return async;
      //  Debug.Log("Loading complete");
        instance.unitySplash.SetActive(false);
        lastScene = name;
        yield return new WaitForEndOfFrame();
    }
    IEnumerator LoadWithoutSplash(string name)
    {
        desktop.SetActive(false);
        //yield return new WaitForSeconds(2);
        //AsyncOperation async = Application.LoadLevelAdditiveAsync(name);
        //yield return async;
        /*AsyncOperation async =*/
        Application.LoadLevelAdditive(name);
        //yield return async;
        //  Debug.Log("Loading complete");
        lastScene = name;
        yield return new WaitForEndOfFrame();
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
        callback.AttachmentWasOpen();
    }
    /*public static void HardCrash()
    {
        instance.StartCoroutine(instance.DoHardCrash());
    }*/
    public IEnumerator HardCrash()
    {
        OSHandler.CloseLast();
        BSOD.SetActive(true);
        OSHandler.ShowDesktop();
        source.clip = crashClip;
        source.Play();
        yield return new WaitForSeconds(timeBSOD);
        BSOD.SetActive(false);
        yield return new WaitForEndOfFrame();
        //StartCoroutine(DoReboot());
    }
    public static void Reboot()
    {
        instance.StartCoroutine(instance.DoReboot());
    }
    IEnumerator DoReboot()
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
            loginScreen.SetActive(false);
            yield return new WaitForSeconds(1f);
            brokenIntro.SetActive(true);
            yield return new WaitForSeconds(6.3f);
            gc.Activate(2);
            yield return new WaitForSeconds(1.1f);
            gc.Deactivate();
            brokenIntro.SetActive(false);
            middleIntro.SetActive(true);
            yield return new WaitForSeconds(3f * demonRatio);
            middleIntro.GetComponent<AudioLowPassFilter>().enabled = false;
            middleIntro.GetComponent<SoundGlitch>().enabled = false;
            middleIntro.GetComponent<IsGlitched>().enabled = false;
            workingIntro.SetActive(true);
            yield return new WaitForSeconds(3 * (1 - demonRatio));
            StartCoroutine("FadeOut");
            yield return new WaitForSeconds(2.0f);
            loginScreen.SetActive(true);
            loginScreen.GetComponent<ToggleUIItem>().Show();
            middleIntro.SetActive(false);
            

        }
        else
        {
            inbox.gameObject.SetActive(true);
            inbox.OpenInbox();
        }

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

    public void Login()
    {
        workingIntro.SetActive(false);
        loginScreen.GetComponent<ToggleUIItem>().Hide();
       
        inbox.gameObject.SetActive(true);
        inbox.OpenInbox();
    }
}
