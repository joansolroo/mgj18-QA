using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailRenderer : MonoBehaviour
{


    [SerializeField] public Mail mail;
    [SerializeField] UnityEngine.UI.Image avatar;
    [SerializeField] UnityEngine.UI.Text title;
    [SerializeField] UnityEngine.UI.Text content;
    [SerializeField] UnityEngine.UI.Text readMore;
    [SerializeField] UnityEngine.RectTransform downloadButton;
    [SerializeField] UnityEngine.UI.Image notDownloadedBackground;

    [SerializeField] UnityEngine.UI.Button reply;

    [SerializeField] public bool read = false;
    [SerializeField] public bool downloaded = false;
    [SerializeField] public bool played = false;

    public bool expanded = false;

    [HideInInspector] public Inbox inbox;

    [HideInInspector] public float height = 2;

    public RectTransform rt;

    public int index;
    // Use this for initialization
    void Start()
    {
        avatar.sprite = mail.avatar;
        title.text = mail.title;
        content.text = mail.content.Replace("\\n", "\n");
        if (mail.download != null)
        {
            downloadButton.gameObject.SetActive(true);
            mail.download.mailRenderer = this;
        }
        else
        {
            downloadButton.gameObject.SetActive(false);
        }
        readMore.enabled = !expanded && mail.height > 2;

        if (mail.customFont != null)
        {
            title.font = mail.customFont;
            content.font = mail.customFont;
        }

        height = 2;
        targetHeight = height;

        reply.gameObject.SetActive(!mail.isResponse && this.read);

        rt = GetComponent<RectTransform>();
        StartCoroutine("CreateAnimation");
    }



    float targetHeight;
    public void Toggle()
    {
        if (!this.read)
        {
            Read();
            title.fontStyle = FontStyle.Italic;
            title.color = new Color32(114, 114, 114, 255);
        }
        expanded = !expanded;
        readMore.enabled = !expanded && mail.height > 2;
        targetHeight = expanded ? mail.height : 2;
        StartCoroutine("ToggleAnimation");
    }

    private void Read()
    {
        if (!this.read)
        {
            this.read = true;
            reply.gameObject.SetActive(!mail.isResponse && this.read);
        }
        if (mail.openOnRead != null)
        {
            Inbox.AddMail(mail.openOnRead);
        }
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
            scale = Mathf.MoveTowards(scale, 1, 2 * Time.deltaTime);
            rt.localScale = new Vector3(scale, 1, scale);
            yield return new WaitForSeconds(Time.deltaTime * .1f);
        }
    }

    public void Download()
    {
        if (!this.downloaded)
        {
            notDownloadedBackground.enabled = false;
            DownloadManager.AddDownload(mail.download);
        }
    }
    public void Reply()
    {
        inbox.Reply(mail);
    }
    public void AttachmentWasOpen()
    {
        if (mail.openOnRead != null)
        {
            Inbox.AddMail(mail.openAfterPlaying);
        }
    }
}
