using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadManager : MonoBehaviour
{
    public Transform button;
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
        button.gameObject.SetActive(background.enabled);
    }
   
    public void Close()
    {
        for(int idx = downloads.Count-1; idx >= 0; --idx)
        {
            GameObject.Destroy(downloads[idx].gameObject);
            instance.downloads.RemoveAt(idx);
        }
        
    }
    public static void AddDownload(Download download)
    {
        DownloadRenderer dr = GameObject.Instantiate<DownloadRenderer>(instance.downloadRenderer);
        dr.download = download;
        dr.transform.SetParent(instance.transform);
        dr.transform.localScale = Vector3.one;
        dr.GetComponent<RectTransform>().anchoredPosition = new Vector3((dr.width+10)*instance.downloads.Count, -33);
        instance.downloads.Insert(0,dr);
        if (instance.downloads.Count > 2)
        {
            DownloadRenderer toRemove = instance.downloads[instance.downloads.Count-1];
            GameObject.Destroy(toRemove.gameObject);
            instance.downloads.RemoveAt(instance.downloads.Count - 1);
        }
    }
}
