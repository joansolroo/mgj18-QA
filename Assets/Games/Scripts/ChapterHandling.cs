using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterHandling : MonoBehaviour
{
    [SerializeField] string sceneName;

    [SerializeField] UnityEngine.UI.Image fade;
    [SerializeField] float fadeSpeed = 1;

    [SerializeField] GameObject introduction;
    [SerializeField] float introductionDuration = 3;
    [SerializeField] GameObject manscene;

    [SerializeField] bool autoOutro = false;
    [SerializeField] float autoOutroDelay = 3;

    [SerializeField] GameObject outro;

    [SerializeField] UnityEngine.UI.Image fadeOut;
    [SerializeField] float fadeOutSpeed = 1;

    private void Start()
    {
        StartCoroutine("Introduction");
    }

    public float a;
    IEnumerator Introduction()
    {
        if (outro != null) outro.SetActive(false);
        introduction.SetActive(true);
        manscene.SetActive(false);
        yield return new WaitForSeconds(introductionDuration);
        manscene.SetActive(true);

        Camera cam = introduction.GetComponent<Camera>();
        if (cam != null)
        {
            cam.enabled = false;
        }
        AudioListener audio = introduction.GetComponent<AudioListener>();
        if (audio != null)
        {
            audio.enabled = false;
        }


        if (fade != null)
        {
            a = 1;
            while (a > 0)
            {
                fade.color = Color.Lerp(new Color(0, 0, 0, 0), fade.color, a);
                a -= Time.deltaTime * fadeSpeed;
                yield return new WaitForEndOfFrame();
            }
        }
        introduction.SetActive(false);
        if (autoOutro)
        {
            yield return new WaitForSeconds(autoOutroDelay);
            EndLevel();
        }
    }

    public void EndLevel()
    {
        StartCoroutine("Outro");
    }
    IEnumerator Outro()
    {
        if (outro != null)
        {
            introduction.SetActive(false);
            outro.SetActive(true);
            Camera cam = outro.GetComponent<Camera>();
            if (cam != null)
            {
                cam.enabled = false;
            }
            AudioListener audio = outro.GetComponent<AudioListener>();
            if (audio != null)
            {
                audio.enabled = false;
            }
            if (fadeOut != null)
            {
                Color color = fadeOut.color;
                fadeOut.color = new Color(0, 0, 0, 0);
                float a = 0;
                while (a <1)
                {
                    fadeOut.color = Color.Lerp(fadeOut.color, color, a);
                    a += Time.deltaTime * fadeSpeed;
                    yield return new WaitForEndOfFrame();
                }
            }
            CloseChapter();
        }
    }

    public void CloseChapter()
    {
        Debug.Log("Closing chapter");
        OSHandler.CloseLast();
    }
}
