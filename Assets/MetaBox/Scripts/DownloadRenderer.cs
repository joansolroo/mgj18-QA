using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadRenderer : MonoBehaviour {

    [SerializeField] float progress = 0;
    [SerializeField] public Download download;

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
                StartCoroutine(StartAnimationDone());

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

    
    IEnumerator StartAnimationDone()
    {
        Vector3 scale = this.transform.localScale;
        for (float t = 0; t < 1; t += 0.05f)
        {
            this.transform.localScale = scale + Vector3.one*Mathf.Sin(t * 5)*0.05f;
            yield return new WaitForEndOfFrame();
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
            StartCoroutine(StartRunProgram());
        }
    }
    bool running = false;
    IEnumerator StartRunProgram()
    {
        if (!running)
        {
            running = true;
            StartCoroutine(StartAnimationDone());
            yield return new WaitForSeconds(0.25f);
            //OSHandler.Run(download);
            download.mailRenderer.AttachmentWasOpen();
        }
        yield return new WaitForSeconds(0.25f);
        running = false;
    }
}
