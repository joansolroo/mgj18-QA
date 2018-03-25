using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSHandler : MonoBehaviour {

    [SerializeField] public GameObject unitySplash; //TODO remove later
    [SerializeField] public GameObject crash;

    [SerializeField] public GameObject desktop;
    static OSHandler instance;
    private void Awake()
    {
        instance = this;
    }
    public static void Run(Download download)
    {
       // instance.StartCoroutine("RunAndCrash");
        instance.StartCoroutine("LoadGame", instance.scene);
    }
    string scene = "world1"; 
    IEnumerator LoadGame(string name){
        desktop.SetActive(false);
        instance.unitySplash.SetActive(true);
        yield return new WaitForSeconds(2);
        AsyncOperation async = Application.LoadLevelAdditiveAsync(name);
        yield return async;
        Debug.Log("Loading complete");
        instance.unitySplash.SetActive(false);
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
}
