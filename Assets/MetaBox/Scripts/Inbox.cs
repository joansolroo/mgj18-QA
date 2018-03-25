﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inbox : MonoBehaviour {

    [SerializeField] List<Mail> mails;
    [SerializeField] MailRenderer mailPrefab;

    [SerializeField] Transform emailRegion;
    List<MailRenderer> mailRenderers;
    [SerializeField] GameObject loadingLayer;

    [SerializeField] RectTransform[] loadingOrder;

    [SerializeField] ResponseComposer responseComposer;
    // Use this for initialization

    public static Inbox instance;
    private void Awake()
    {
        instance = this;
    }
    void Start () {
        mailRenderers = new List<MailRenderer>();
       // OpenInbox();
    }

    public void OpenInbox()
    {
        StartCoroutine("AddEmails");
    }
    public void CloseInbox()
    {
        foreach(RectTransform rt in loadingOrder)
        {
            rt.gameObject.SetActive(false);
        }

        for(int m = mailRenderers.Count-1;m>=0;--m)
        {
            Destroy(mailRenderers[m].gameObject);
        }
        mailRenderers.Clear();

        responseComposer.Close();
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
       
        foreach (RectTransform rt in loadingOrder)
        {
            rt.gameObject.SetActive(false);
        }

        foreach (RectTransform rt in loadingOrder)
        {
            rt.gameObject.SetActive(true);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
            loadingLayer.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        foreach (Mail m in mails)
        {
            MailRenderer mr = Instantiate(mailPrefab);
            mr.mail = m;
            m.mr = mr;
            mailRenderers.Insert(0,mr);
            mr.height = 2;
            UpdateLayout();
            yield return new WaitForSeconds(0.05f);
            int idx = 0;
            foreach (Mail m2 in m.chain)
            {
                MailRenderer mr2 = Instantiate(mailPrefab);
                mr2.mail = m2;
                m2.title = "RE: " + m.title;
                m2.mr = mr2;
                mailRenderers.Insert(++idx, mr2);
                mr2.height = 2;
                UpdateLayout();
                yield return new WaitForSeconds(0.05f);
            }
           
        }
        loadingLayer.SetActive(false);
    }
    public static void AddMail(Mail m, Mail ReplyingTo)
    {
        instance.StartCoroutine(instance.PerformAddEmail(m, ReplyingTo));
    }
    private IEnumerator PerformAddEmail(Mail m, Mail replyingTo)
    {
        loadingLayer.SetActive(true);
        
        MailRenderer mr = Instantiate(mailPrefab);
        mr.mail = m;
        int position = 0;
        if (replyingTo != null)
        {
            int parent=-1;
            for(int idx = 0; idx < mailRenderers.Count; ++idx)
            {
                if (mailRenderers[idx].id == replyingTo.mr.id)
                {
                    parent = idx;
                    break;
                }
            }
            if (parent != -1)
            {
                for (int idx2 = parent + 1; idx2 < mailRenderers.Count; ++idx2)
                {
                    if (!mailRenderers[idx2].mail.isResponse)
                    {
                        position = idx2;
                    }
                }
            }
            
        }
        mailRenderers.Insert(position,mr);
        mr.height = 2;
        UpdateLayout();
        yield return new WaitForSeconds(0.25f);

        loadingLayer.SetActive(false);
    }
    public void UpdateLayout()
    {
        float offset = -60;
        int idx = 0;
        foreach(MailRenderer mr in mailRenderers) {
            mr.index = idx++;
            offset = UpdateMailRenderer(mr, offset);
        }
        RectTransform container = emailRegion.parent.GetComponent<RectTransform>();
        container.sizeDelta = new Vector2(0, offset+180);
    }
    float UpdateMailRenderer(MailRenderer mr, float offset)
    {
        mr.transform.SetParent(emailRegion.transform);
        RectTransform rt = mr.rt;
        if (rt == null)
        {
            mr.rt = mr.GetComponent<RectTransform>();
            rt = mr.rt;
        }
        //rt.localScale = Vector3.one;
        float sizeY = true ? 120 / 2 * mr.height : 120;
        offset += sizeY + 20;
        rt.anchoredPosition = new Vector2(mr.mail.isResponse ? 20 : -10, -offset);
        rt.sizeDelta = new Vector2(0, sizeY);
        mr.inbox = this;
        return offset;
    }
    public void Reply(Mail mail)
    { 
        responseComposer.gameObject.SetActive(true);
        responseComposer.Reply(mail);
    } 
}
