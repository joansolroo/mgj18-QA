using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterHandling : MonoBehaviour {

    [SerializeField] UnityEngine.UI.Image fade;
    [SerializeField] GameObject introduction;
    [SerializeField] GameObject manscene;
    [SerializeField] float speed = 1;
    private void Start()
    {
        StartCoroutine("Introduction");
    }

    IEnumerator Introduction()
    {
        introduction.SetActive(true);
        manscene.SetActive(false);
        yield return new WaitForSeconds(3);
        manscene.SetActive(true);
        introduction.GetComponent<Camera>().enabled = false;
        introduction.GetComponent<AudioListener>().enabled = false;

        float a = 1;
        while(a > 0){
            fade.color = Color.Lerp(new Color(0, 0, 0, 0), fade.color, a);
            a -= Time.deltaTime*speed;
            yield return new WaitForEndOfFrame();
        }
        introduction.SetActive(false);
    }
}
