using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : ScriptableObject {

    public enum MailType
    {
        Bughunt, filling
    }
    [SerializeField] public Sprite avatar;
    [SerializeField] public string title;
    [SerializeField] public string content;
    [SerializeField] public Download download;
    [SerializeField] public int height= 2;
    [SerializeField] public List<Mail> chain;
    [SerializeField] public bool isResponse = false;
    [SerializeField] public MailRenderer mr;

    [SerializeField] public Mail helpAnswer;
    [SerializeField] public Mail successAnswer;
    [SerializeField] public Mail failAnswer;

    [SerializeField] public Font customFont;
}
