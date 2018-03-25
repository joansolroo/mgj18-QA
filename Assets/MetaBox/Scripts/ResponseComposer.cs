using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseComposer : MonoBehaviour
{

    [SerializeField] Sprite avatar;

    [SerializeField] UnityEngine.UI.Dropdown to;

    [SerializeField] UnityEngine.UI.Text header;

    [SerializeField] UnityEngine.UI.Dropdown introduction;
    [SerializeField] UnityEngine.UI.Text introduction_underline;
    [SerializeField] UnityEngine.UI.Dropdown subject;
    [SerializeField] UnityEngine.UI.Text subject_underline;
    [SerializeField] UnityEngine.UI.Dropdown auxiliar;
    [SerializeField] UnityEngine.UI.Text auxiliar_underline;
    [SerializeField] UnityEngine.UI.Dropdown problem;
    [SerializeField] UnityEngine.UI.Text problem_underline;


    bool initialized;
    private void OnEnable()
    {
        introduction.value = 8;
        // introduction.captionText.text = "[write an answer]";

    }
    Mail replyingTo;
    public void Reply(Mail m)
    {
        replyingTo = m;
        header.text = "RE:" + m.title;
        //   Changed();
    }
    public void Changed()
    {
        Debug.Log("chang");
        subject.gameObject.SetActive(introduction.value == 1);
        auxiliar.gameObject.SetActive(introduction.value == 1);
        problem.gameObject.SetActive(introduction.value == 1);

        introduction.captionText.text = introduction.options[introduction.value].text;
        if (auxiliar.value != 0)
        {
            subject.captionText.text = subject.options[subject.value].text + "'s";
        }
        else
        {
            subject.captionText.text = subject.options[subject.value].text;
        }
        introduction_underline.text = DottedLine(introduction.captionText.text.Length);
        subject_underline.text = DottedLine(subject.captionText.text.Length);
        auxiliar_underline.text = DottedLine(auxiliar.captionText.text.Length);
        problem_underline.text = DottedLine(problem.captionText.text.Length);
    }
    string DottedLine(int length)
    {
        string result = "";
        for(int i = 0; i < length+1; i++)
        {
            result += ". ";
        }
        return result;
    }
    public string AsString()
    {
        string result = "Hi, \n" + introduction.captionText.text;
        if (introduction.value == 1)
        {
            result += subject.captionText.text + " ";
            if (auxiliar.value != 0)
            {
                result += auxiliar.captionText.text + " ";
            }
            result += problem.captionText.text;
        }
        result += "\nBest,\n*PAC";

        return result;
    }
    public void Send()
    {
        Mail m = new Mail();
        m.title = header.text;
        m.avatar = avatar;
        m.content = AsString();
        m.isResponse = true;
        m.replyingTo = replyingTo;
        m.fromPlayer = true;
        m.height = 4;
        Debug.Log(m.title + "//" + m.content);
        Inbox.AddMail(m);
        gameObject.SetActive(false);

    }
    public void Close()
    {
        gameObject.SetActive(false);

    }
}
