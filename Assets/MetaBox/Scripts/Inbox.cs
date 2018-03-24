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

    [SerializeField] ResponseComposer responseComposer;
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
       
        foreach (RectTransform rt in loadingOrder)
        {
            rt.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(5);
        
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
