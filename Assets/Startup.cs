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

    // Use this for initialization
    void Start()
    {
        gc = GetComponent<GlitchController>();
        if (fastBoot)
        {

            OSHandler.instance.StartCoroutine("Reboot");
        }
        else
        {
            OSHandler.instance.StartCoroutine("RunIntro");
        }
    }
    IEnumerator RunIntro()
    {
        OSHandler.Run("world1");
        yield return new WaitForSeconds(timeUntilCrash);
        gc.Activate(4);
        yield return new WaitForSeconds(timeBeforeBSOD);
        StartCoroutine("HardCrash");
        OSHandler.Close("world1");
    }
}
