using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailRenderer : MonoBehaviour {

    [SerializeField] public Mail mail;
    [SerializeField] UnityEngine.UI.Image avatar;
    [SerializeField] UnityEngine.UI.Text title;
    [SerializeField] UnityEngine.UI.Text content;
    [SerializeField] UnityEngine.UI.Text readMore;
    [SerializeField] UnityEngine.RectTransform downloadButton;
    [SerializeField] UnityEngine.UI.Image notDownloadedBackground;

    [SerializeField] UnityEngine.UI.Button reply;
    [SerializeField] bool downloaded;

    bool read = false;
    public bool expanded = false;

    [HideInInspector] public Inbox inbox;

    [HideInInspector] public  float height = 2;

    public RectTransform rt;

    public int index;
    // Use this for initialization
    void Start () {
        avatar.sprite = mail.avatar;
        title.text = mail.title;
        content.text = mail.content;
        downloadButton.gameObject.SetActive(mail.download!=null);
        readMore.enabled = !expanded && mail.height > 2;

        height = 2;
        targetHeight = height;

        reply.gameObject.SetActive(!mail.isResponse);

        rt = GetComponent<RectTransform>(); 
        StartCoroutine("CreateAnimation");
    }

   
    
    float targetHeight;
    public void Toggle()
    {
        if (!read)
        {
            read = true;
            title.fontStyle = FontStyle.Italic;
            title.color = new Color32(114, 114, 114, 255);
        }
        expanded = !expanded;
        readMore.enabled = !expanded && mail.height > 2;
        targetHeight = expanded ? mail.height : 2;
        StartCoroutine("ToggleAnimation");
    }

    private void Update()
    {
        
        
    }
    private IEnumerator ToggleAnimation()
    {

        while (height != targetHeight)
        {
            height = Mathf.MoveTowards(height, targetHeight, 5 * Time.deltaTime * Mathf.Abs(height - targetHeight));
            yield return new WaitForSeconds(Time.deltaTime);
            inbox.UpdateLayout();
        }
    }

    Vector3 scale;
    private IEnumerator CreateAnimation()
    {
        float scale = 0;
        while (scale != 1)
        {
            scale = Mathf.MoveTowards(scale, 1, 2*Time.deltaTime);
            rt.localScale = new Vector3(scale,1,scale);
            yield return new WaitForSeconds(Time.deltaTime*.1f);
        }
    }

    public void Download()
    {
        if (!downloaded)
        {
            notDownloadedBackground.enabled = false;
            DownloadManager.AddDownload(mail.download);
        }
    }
    public void Reply()
    {
        inbox.Reply(mail);
    }
}
