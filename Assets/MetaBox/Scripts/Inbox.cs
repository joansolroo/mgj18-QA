using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inbox : MonoBehaviour {

    [SerializeField] List<Mail> mails;
    [SerializeField] MailRenderer mailPrefab;

    [SerializeField] Transform emailRegion;
    List<MailRenderer> mailRenderers;
    [SerializeField] GameObject loadingLayer;

    [SerializeField] RectTransform[] loadingOrder;
    // Use this for initialization
    void Start () {
        mailRenderers = new List<MailRenderer>();

        StartCoroutine("AddEmails");
        //Loading(1);
    }

    public void Loading( float time)
    {
        StartCoroutine("LoadingCoroutine", time);
    }

    private IEnumerator LoadingCoroutine(float time)
    {
        loadingLayer.SetActive(true);
        yield return new WaitForSeconds(time);
        loadingLayer.SetActive(false);
    }

    private IEnumerator AddEmails()
    {
        loadingLayer.SetActive(true);
        foreach (RectTransform rt in loadingOrder)
        {
            rt.gameObject.SetActive(false);
        }
        foreach (RectTransform rt in loadingOrder)
        {
            yield return new WaitForSeconds(Random.Range(0.1f,0.3f));
            rt.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        foreach (Mail m in mails)
        {
            MailRenderer mr = Instantiate(mailPrefab);
            mr.mail = m;
            mailRenderers.Insert(0,mr);
            mr.height = 2;
            UpdateLayout();
            yield return new WaitForSeconds(0.25f);
        }
        loadingLayer.SetActive(false);
    }
    private IEnumerator AddEmail(Mail m)
    {
        loadingLayer.SetActive(true);
        
        MailRenderer mr = Instantiate(mailPrefab);
        mr.mail = m;
        mailRenderers.Insert(0,mr);
        mr.height = 2;
        UpdateLayout();
        yield return new WaitForSeconds(0.25f);

        loadingLayer.SetActive(false);
    }
    public void UpdateLayout()
    {
        int idx = 0;
        float offset = -60;
        foreach(MailRenderer mr in mailRenderers) { 
            mr.transform.SetParent(emailRegion.transform);
            RectTransform rt = mr.GetComponent<RectTransform>();
            //rt.localScale = Vector3.one;
            float sizeY = true? 120/2*mr.height:120;
            offset += sizeY + 20;
            rt.anchoredPosition = new Vector2(-10, -offset);
            rt.sizeDelta = new Vector2(0, sizeY);
            mr.inbox = this;
            idx++;
        }
        RectTransform container = emailRegion.parent.GetComponent<RectTransform>();
        container.sizeDelta = new Vector2(0, offset+180);
    }
}
