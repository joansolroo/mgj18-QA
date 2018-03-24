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

    float anchorX;
    void Start () {
        anchorX = progressBar.position.x;
        fileNameText.text = download.filename;
    }
	
	// Update is called once per frame
	void Update () {
        if(progress < 1)
        {
            progress = Mathf.MoveTowards(progress, 1, Time.deltaTime* Random.Range(0.1f,1));
            if (progress >= 1)
            {
                GetComponent<AudioSource>().Play();
                progressBar.GetComponent<UnityEngine.UI.Image>().color = new Color32(0, 145, 255,255);
            }
            else
            {
                float max = progress * 220;
                progressBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, max);
            }
        }
       
     }

    /// DEBUG
    [SerializeField] public GameObject unitySplash;
    [SerializeField] public GameObject crash;
    IEnumerator coroutine;
    public void RunProgram()
    {
        if (progress < 1)
        {
            return;
        }
        else
        {
            Debug.Log("Running :" + download.filename);
            coroutine = RunAndCrash();
            StartCoroutine("RunAndCrash");
        }
    }

    private IEnumerator RunAndCrash()
    {
        unitySplash.SetActive(true);
        yield return new WaitForSeconds(2);
            print("WaitAndPrint " + Time.time);
        crash.SetActive(true);
    }
}
