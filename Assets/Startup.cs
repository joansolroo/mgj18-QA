using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(GlitchController))]
public class Startup : MonoBehaviour
{

    [SerializeField] int timeUntilCrash = 10;
    [SerializeField] int timeBeforeBSOD = 2;

    [SerializeField] bool fastBoot = false;

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

    IEnumerator StartChapter(int _chapter)
    {
        chapter = _chapter;
        switch (chapter)
        {
            case 0:
                OSHandler.RunNow("Noot");
                break;
            case 1:
                if (firstChapter < 1) OSHandler.CloseLast();
                OSHandler.RunNow("world_1");
                break;
            case 2:
                if (firstChapter < 2) yield return StartCoroutine(HardCrash());
                OSHandler.Reboot();
                break;
            case 3:
                OSHandler.ShowDesktop();
                OSHandler.CloseLast();
                break;
            case 4:
                OSHandler.CloseLast();
                OSHandler.RunNow("world_2_broken");
                break;
            case 5:
                OSHandler.ShowDesktop();
                OSHandler.CloseLast();
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
