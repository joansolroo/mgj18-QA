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

    private void Start()
    {
        StartCoroutine("Introduction");
    }

    IEnumerator Introduction()
    {
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
            float a = 1;
            while (a > 0)
            {
                fade.color = Color.Lerp(new Color(0, 0, 0, 0), fade.color, a);
                a -= Time.deltaTime * fadeSpeed;
                yield return new WaitForEndOfFrame();
            }
        }
        introduction.SetActive(false);
    }

    public void CloseChapter()
    {
        Debug.Log("Closing chapter");
        OSHandler.CloseLast();
    }
}
