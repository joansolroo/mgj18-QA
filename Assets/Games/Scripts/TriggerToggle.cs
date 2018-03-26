using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToggle : MonoBehaviour {

    [SerializeField] GameObject visualization;

    public float transitionDuration2 = 0.25f;

    [SerializeField] bool triggerBased = true; //THIS IS A HACK, these should be 2 different monobehaviours
    [SerializeField] bool active = true; //THIS IS A HACK, these should be 2 different monobehaviours
    private void Start()
    {
        if (!active)
        {
            HideImmediately();
        }
        wasActive = active;
    }
    bool wasActive = false;
    private void Update()
    {
        if (active && !wasActive)
        {
            wasActive = active;
            StartCoroutine("Show");
        }
        if (!active && wasActive)
        {
            StartCoroutine("Hide");
            wasActive = active;
        }

        if (visualization.activeSelf)
        {
            float dx = Camera.main.WorldToViewportPoint(this.transform.position).x;
            dx = (dx - 0.5f) * 2;
            Vector3 targetAngle = CameraRotation.instance.transform.eulerAngles + new Vector3(0, dx*-13, 0);
            visualization.transform.rotation = Quaternion.RotateTowards(visualization.transform.rotation, Quaternion.Euler(targetAngle), Quaternion.Angle(visualization.transform.rotation, Quaternion.Euler(targetAngle)) * Time.deltaTime * 5);

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (triggerBased)
        {
            if (other.gameObject.tag == "Player")
            {
                active = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            active = false;
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

        float dx = Camera.main.WorldToViewportPoint(this.transform.position).x;
        dx = (dx - 0.5f) * 2;
        Vector3 targetAngle = CameraRotation.instance.transform.eulerAngles + new Vector3(0, dx * -13, 0);

        visualization.transform.eulerAngles=targetAngle;

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

}
