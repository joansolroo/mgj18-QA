using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inbox : MonoBehaviour {

    [SerializeField] List<Mail> mails;
    [SerializeField] MailRenderer mailPrefab;

    Transform emailRegion;
    List<MailRenderer> mailRenderers;
    // Use this for initialization
    void Start () {

        emailRegion = this.transform;
        mailRenderers = new List<MailRenderer>();
        foreach (Mail m in mails)
        {
            MailRenderer mr = Instantiate(mailPrefab);
            mr.mail = m;
            mailRenderers.Add(mr);
            mr.height = 2;
        }
        UpdateLayout();
    }
	
	public void UpdateLayout()
    {
        int idx = 0;
        float offset = -60;
        foreach(MailRenderer mr in mailRenderers) { 
            mr.transform.SetParent(this.transform);
            RectTransform rt = mr.GetComponent<RectTransform>();
            rt.localScale = Vector3.one;
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
