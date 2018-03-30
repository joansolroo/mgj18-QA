﻿using System.Collections;
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
            transform.localScale = minSize;
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
    public void Show(float delay = 0)
    {
        gameObject.SetActive(true);
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(AnimationPoke(delay));
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
    public void Hide(float delay = 0)
    {
        StartCoroutine(AnimationHide(delay));
    }
    public void Poke(float delay = 0)
    {
        StartCoroutine(AnimationPoke(delay));
    }

    IEnumerator AnimationHide(float delay)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }
        if (hide != null)
        {
            hide.Play();
        }
        visible = false;
        inTransition = true;
        while (transform.localScale.x > minSize.x || transform.localScale.y > minSize.y || transform.localScale.z > minSize.z)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, minSize, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        inTransition = false;

    }

    IEnumerator AnimationShow(float delay = 0)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }
        if (show != null)
        {
            show.Play();
        }
        visible = true;
        inTransition = true;
        while (transform.localScale.x < maxSize.x || transform.localScale.y < maxSize.y || transform.localScale.z < maxSize.z)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, maxSize, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        inTransition = false;
    }
    IEnumerator AnimationPoke(float delay)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }
        if (show != null)
        {
            show.Play();
        }
        visible = true;
        inTransition = true;

        Vector3 targetSize = Vector3.one * pokeScale;
        for (float t = 0; t < 1; t += Time.deltaTime * speed)
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
