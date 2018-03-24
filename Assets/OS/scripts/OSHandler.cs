using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSHandler : MonoBehaviour {

    [SerializeField] public GameObject unitySplash; //TODO remove later
    [SerializeField] public GameObject crash;

    static OSHandler instance;
    private void Awake()
    {
        instance = this;
    }
    public static void Run(Download download)
    {
        instance.StartCoroutine("RunAndCrash");
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
    }
}
