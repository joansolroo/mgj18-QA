using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseComposer : MonoBehaviour
{


    [SerializeField] UnityEngine.UI.Dropdown to;

    [SerializeField] UnityEngine.UI.Text header;

    [SerializeField] UnityEngine.UI.Dropdown introduction;
    [SerializeField] UnityEngine.UI.Dropdown subject;
    [SerializeField] UnityEngine.UI.Dropdown auxiliar;
    [SerializeField] UnityEngine.UI.Dropdown problem;


    private void OnEnable()
    {
        Changed();
    }
    public void Reply(Mail m)
    {
        header.text = "RE:" + m.title;
    }
    public void Changed()
    {

        subject.gameObject.SetActive(introduction.value == 1);
        auxiliar.gameObject.SetActive(introduction.value == 1);
        problem.gameObject.SetActive(introduction.value == 1);

        if (auxiliar.value != 0)
        {
            subject.captionText.text = subject.options[subject.value].text + "'s";
        }
        else
        {
            subject.captionText.text = subject.options[subject.value].text;
        }
    }
    public void Send()
    {
        gameObject.SetActive(false);

    }
    public void Close()
    {
        gameObject.SetActive(false);

    }
}
