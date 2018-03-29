using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUIItem : MonoBehaviour
{
    [SerializeField] AudioSource show;
    [SerializeField] AudioSource hide;

    private void Start()
    {
        if (!visible)
        {
            transform.localScale =minSize;
        }
        else
        {
            transform.localScale = maxSize;
        }
    }

    [SerializeField] bool inTransition = false;
    [SerializeField] bool visible = false;
    [SerializeField] Vector3 minSize = Vector3.zero;
    [SerializeField] Vector3 maxSize = Vector3.one;
    [SerializeField] float speed = 1;
    [SerializeField] float pokeScale = 1.5f;
    public void Toggle()
    {
        if (!inTransition)
        {
            if (visible)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }
    public void Show()
    {
        StartCoroutine(AnimationPoke());
    }
    public void Hide()
    {
        StartCoroutine(AnimationHide());
    }
    public void Poke()
    {
        StartCoroutine(AnimationPoke());
    }

    IEnumerator AnimationHide()
    {
        if (hide != null)
        {
            hide.Play();
        }
        visible = false;
        inTransition = true;
        while (transform.localScale.x > minSize.x || transform.localScale.y > minSize.y || transform.localScale.z > minSize.z)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, minSize, Time.deltaTime*speed);
            yield return new WaitForEndOfFrame();
        }
        inTransition = false;

    }
    
    IEnumerator AnimationShow()
    {
        if (show != null)
        {
            show.Play();
        }
        visible = true;
        inTransition = true;
        while (transform.localScale.x < maxSize.x || transform.localScale.y < maxSize.y || transform.localScale.z < maxSize.z)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, maxSize, Time.deltaTime*speed);
            yield return new WaitForEndOfFrame();
        }
        inTransition = false;
    }
    IEnumerator AnimationPoke()
    {
        if (show != null)
        {
            show.Play();
        }
        visible = true;
        inTransition = true;

        Vector3 targetSize = Vector3.one * pokeScale;
        for (float t = 0; t < 1; t+=Time.deltaTime*speed)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        for (float t = 0; t < 1; t += Time.deltaTime * speed)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        inTransition = false;
    }
}
