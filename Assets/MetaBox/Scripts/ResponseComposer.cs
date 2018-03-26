using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] int answerIdx;
    public void Reply(Mail m)
    {
        replyingTo = m;
        header.text = "RE:" + m.title;

        introduction.options.Clear();
        if (Inbox.indexedEmails[m.id].played)
        {
            foreach (string a in m.answerOptionsAfterPlaying)
            {
                introduction.options.Add(new Dropdown.OptionData(a));
                introduction.value = 0;
            }
        }
        else
        {
            foreach (string a in m.answerOptions)
            {
                introduction.options.Add(new Dropdown.OptionData(a));
                introduction.value = 0;
            }
        }

        //   Changed();
    }
    public void Changed()
    {
        Debug.Log("chang");
        answerIdx = introduction.value;
        subject.gameObject.SetActive(false/*introduction.value == 1*/);
        auxiliar.gameObject.SetActive(false/*introduction.value == 1*/);
        problem.gameObject.SetActive(false/*introduction.value == 1*/);

        introduction.captionText.text = introduction.options[introduction.value].text;
        introduction_underline.text = DottedLine(introduction.captionText.text.Length);
        /* if (auxiliar.value != 0)
         {
             subject.captionText.text = subject.options[subject.value].text + "'s";
         }
         else
         {
             subject.captionText.text = subject.options[subject.value].text;
         }

         subject_underline.text = DottedLine(subject.captionText.text.Length);
         auxiliar_underline.text = DottedLine(auxiliar.captionText.text.Length);
         problem_underline.text = DottedLine(problem.captionText.text.Length);*/
    }
    string DottedLine(int length)
    {
        string result = "";
        for (int i = 0; i < length + 1; i++)
        {
            result += ". ";
        }
        return result;
    }
    public string AsString()
    {
        string result = "Hi, \n" + introduction.captionText.text;
        /*if (introduction.value == 1)
        {
            result += subject.captionText.text + " ";
            if (auxiliar.value != 0)
            {
                result += auxiliar.captionText.text + " ";
            }
            result += problem.captionText.text;
        }*/ // WE REMOVED THE CUSTOM ANSWER COMPOSITION GIVEN TIME CONSTRAINTS
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
        Inbox.AddMail(m, 0);
        int realIdx = Inbox.indexedEmails[replyingTo.id].played ? answerIdx + replyingTo.answerOptions.Length : answerIdx;
        if (replyingTo.replyToAnswers != null && replyingTo.replyToAnswers.Length >= realIdx && replyingTo.replyToAnswers[realIdx] != null)
        {
            Inbox.AddMail(replyingTo.replyToAnswers[realIdx]);
        }
        Debug.Log(m.id);
        //Inbox.AddMail();
        if (Inbox.indexedEmails[replyingTo.id].expanded)
        {
            Inbox.indexedEmails[replyingTo.id].Toggle();
        }
        gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
