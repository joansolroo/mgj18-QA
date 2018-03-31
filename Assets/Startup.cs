using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(GlitchController))]
public class Startup : MonoBehaviour
{

    [SerializeField] int timeUntilCrash = 10;
    [SerializeField] int timeBeforeBSOD = 2;

    // [SerializeField] bool fastBoot = false;

    GlitchController gc;
    public static Startup instance;
    public int chapter = 1;

    int firstChapter;
    private void Awake()
    {
        firstChapter = chapter;
        instance = this;
    }
    // Use this for initialization
    void Start()
    {
        gc = GetComponent<GlitchController>();
        StartCoroutine(StartChapter(chapter));
    }
    [SerializeField] bool forceNext = false;
    private void Update()
    {
        if (forceNext)
        {
            forceNext = false;
            NextChapter();
        }
    }
    IEnumerator StartChapter(int _chapter)
    {
        chapter = _chapter;
        switch (chapter)
        {
            case 0: //noot intro
                OSHandler.RunNow("Noot");
                break;
            case 1: //glitchy lvl1
                if (firstChapter < 1) OSHandler.CloseLast();
                OSHandler.RunNow("world_1");
                break;
            case 2: //boot, then mails
                if (firstChapter < 2) yield return StartCoroutine(HardCrash());
                OSHandler.Reboot();
                break;
            case 3: //LOFI
                OSHandler.ShowDesktop();
                OSHandler.CloseLast();
                break;
            case 4: //LVL2 lofi - missing
                OSHandler.CloseLast();
                OSHandler.RunNow("world_2_broken");
                break;
            case 5: //after missing cabin file
                OSHandler.ShowDesktop();
                if (firstChapter < 5) OSHandler.CloseLast();
                break;
            case 6: // AFTER CABIN
                OSHandler.ShowDesktop();
                OSHandler.CloseLast();
                break;
            case 7:
                if (firstChapter < 7) OSHandler.CloseLast();
                OSHandler.RunNow("world_2");
                break;
            case 8:
                if (firstChapter < 8) OSHandler.CloseLast();
                OSHandler.RunNow("world_3");
                break;
            case 9:
                yield return StartCoroutine(HardCrash());
                OSHandler.Reboot();
                OSHandler.ShowDesktop();

                OSHandler.instance.demonRatio = 1;
                OSHandler.instance.inbox.DemonicHack();
                break;
            case 10:
                if (firstChapter < 10) OSHandler.CloseLast();
                OSHandler.RunNow("world_2_fp");
                break;
            case 11:
                if (firstChapter < 11) OSHandler.CloseLast();
                OSHandler.RunNow("world_3_fp");
                break;
            case 12:
                if (firstChapter < 12) OSHandler.CloseLast();
                OSHandler.RunNow("world_1_end");
                break;
            case 13:
                if (firstChapter < 12) OSHandler.CloseLast();
                OSHandler.RunNow("Outro");
                break;
        }
        yield return new WaitForEndOfFrame();
    }

    public void NextChapter()
    {
        StartCoroutine(StartChapter(chapter + 1));
    }

    IEnumerator HardCrash()
    {

        // Debug.Log("introooo");
        // yield return new WaitForSeconds(timeUntilCrash);
        GlitchController.instance.Activate(4);
        yield return new WaitForSeconds(2);
        yield return OSHandler.instance.HardCrash();

    }

}
