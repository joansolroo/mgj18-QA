﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseComposer : MonoBehaviour
{

    [SerializeField] Sprite avatar;

    [SerializeField] UnityEngine.UI.Dropdown to;

    [SerializeField] UnityEngine.UI.Text header;

    [SerializeField] UnityEngine.UI.Dropdown introduction;
    [SerializeField] UnityEngine.UI.Dropdown subject;
    [SerializeField] UnityEngine.UI.Dropdown auxiliar;
    [SerializeField] UnityEngine.UI.Dropdown problem;


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
    }
    public string AsString()
    {
        string result = "Hi, \n"+introduction.captionText.text;
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
        m.height = 4;
        Debug.Log(m.title + "//" + m.content);
        Inbox.AddMail(m,replyingTo);
        gameObject.SetActive(false);

    }
    public void Close()
    {
        gameObject.SetActive(false);

    }
}
