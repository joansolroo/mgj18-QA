using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadRenderer : MonoBehaviour {

    [SerializeField] float progress = 0;
    [SerializeField] Download download;

    [SerializeField] UnityEngine.UI.Text fileNameText;
    [SerializeField] UnityEngine.RectTransform progressBar;

    [SerializeField] public int width = 220;
    [SerializeField] public int height = 68;
    // Use this for initialization

    new AudioSource audio;

    void Start () {

        fileNameText.text = download.filename;

        audio = GetComponent<AudioSource>();
    }

    
    // Update is called once per frame
    void Update () {
        if(progress < 1)
        {
            progress = Mathf.MoveTowards(progress, 1, Time.deltaTime* Random.Range(0.1f,1));
            if (progress >= 1)
            {
                progressBar.GetComponent<UnityEngine.UI.Image>().color = new Color32(0, 145, 255,255);
            }
            else
            {
                if (progress > 0.9f && !audio.isPlaying)
                {
                    audio.Play();
                }
                float max = progress * 220;
                progressBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, max);
            }
        }
       
     }

    public void RunProgram()
    {
        if (progress < 1)
        {
            return;
        }
        else
        {
            OSHandler.Run(download);
        }
    }
    
}
