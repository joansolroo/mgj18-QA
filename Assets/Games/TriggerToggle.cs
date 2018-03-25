using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToggle : MonoBehaviour {

    [SerializeField] GameObject visualization;

    float transitionDuration2 = 0.25f;

    private void Start()
    {
        HideImmediately();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine("Show");
        }
    }

    float OriginalScale=1;
    void HideImmediately()
    {
        Vector3 scale = visualization.transform.localScale;
        OriginalScale = scale.x;
        scale.x = 0;
        visualization.transform.localScale = scale;
        visualization.SetActive(false);
    }
    IEnumerator Show()
    {
        visualization.SetActive(true);

        Vector3 scale = visualization.transform.localScale;
        float s = scale.x;
        
        while (s < OriginalScale)
        {
            s = Mathf.MoveTowards(s, OriginalScale, Time.deltaTime/ transitionDuration2);
            scale.x = s;
            visualization.transform.localScale = scale;
            yield return new WaitForEndOfFrame();
        }
            
    }
    IEnumerator Hide()
    {

        Vector3 scale = visualization.transform.localScale;
        float s = scale.x;

        while (s > 0)
        {
            s = Mathf.MoveTowards(s, 0, Time.deltaTime/ transitionDuration2);
            scale.x = s;
            visualization.transform.localScale = scale;
            yield return new WaitForEndOfFrame();
        }
        visualization.SetActive(false);

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine("Hide");
        }
    }
}
