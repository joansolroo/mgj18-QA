using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseComposer : MonoBehaviour {


    [SerializeField] UnityEngine.UI.Dropdown to;

    [SerializeField] UnityEngine.UI.Text header;

    [SerializeField] UnityEngine.UI.Dropdown subject;
    [SerializeField] UnityEngine.UI.Dropdown auxiliar;
    [SerializeField] UnityEngine.UI.Dropdown problem;


    public void Reply(Mail m)
    {
        header.text = "RE:"+m.title;
    }
    public void Changed()
    {
        if (auxiliar.value != 0)
        {
            subject.captionText.text = subject.options[subject.value].text+"'s";
        }
        else
        {
            subject.captionText.text = subject.options[subject.value].text;
        }
        Debug.Log("changed: "+auxiliar.value);
    }
    public void Send()
    {
        gameObject.SetActive(false);
       
    }
}
