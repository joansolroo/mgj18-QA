using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadManager : MonoBehaviour
{

    public static DownloadManager instance;

    [SerializeField] DownloadRenderer downloadRenderer;
    [SerializeField] UnityEngine.UI.Image background;

    List<DownloadRenderer> downloads = new List<DownloadRenderer>();

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        background.enabled = downloads.Count > 0;
    }
    [SerializeField] public GameObject unitySplash; //TODO remove later
    [SerializeField] public GameObject crash;
    public static void AddDownload(Download download)
    {
        DownloadRenderer dr = GameObject.Instantiate<DownloadRenderer>(instance.downloadRenderer);
        dr.transform.SetParent(instance.transform);
        dr.transform.localScale = Vector3.one;
        dr.GetComponent<RectTransform>().anchoredPosition = new Vector3((dr.width+10)*instance.downloads.Count, -33);
        dr.unitySplash = instance.unitySplash;
        dr.crash = instance.crash;
        instance.downloads.Add(dr);
    }
}
