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

    public int chapter = 1;
    // Use this for initialization
    void Start()
    {
        gc = GetComponent<GlitchController>();

        switch (chapter)
        {
            case 1:
                OSHandler.RunNow("world1");
                break;
            case 2:
                OSHandler.instance.StartCoroutine("Reboot");
                break;
        }
        if (fastBoot)
        {

          
        }
        if(chapter == 1) 
        {
            
            //OSHandler.instance.StartCoroutine("RunIntro");
        }
    }
}
