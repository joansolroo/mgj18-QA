using UnityEngine;
using System.Runtime.InteropServices;

public class Spam : MonoBehaviour {

    private void Start()
    {
        Application.ExternalEval("window.open('http://google.com');");
        //OpenLinkJSPlugin();
    }
    /*
    public void OpenLinkJSPlugin()
    {
//#if !UNITY_EDITOR
        openWindow("http://unity3d.com");
//#endif
    }

    [DllImport("__Internal")]
    private static extern void openWindow(string url);
    */
}
