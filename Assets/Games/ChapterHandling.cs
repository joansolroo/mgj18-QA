using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterHandling : MonoBehaviour {

    [SerializeField] UnityEngine.UI.Image fade;
    [SerializeField] GameObject introduction;
    [SerializeField] float speed = 1;
    private void Start()
    {
        StartCoroutine("Introduction");
    }

    IEnumerator Introduction()
    {
        yield return new WaitForSeconds(4);
        float a = 1;
        while(a > 0){
            fade.color = Color.Lerp(new Color(0, 0, 0, 0), fade.color, a);
            a -= Time.deltaTime*speed;
            yield return new WaitForEndOfFrame();
        }
        introduction.SetActive(false);
    }
}
