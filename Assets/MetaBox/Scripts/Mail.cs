using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : ScriptableObject {

    private static int MAX_ID = 0;
    public int id = ++MAX_ID;
    public enum MailType
    {
        Bughunt, filling
    }
    [SerializeField] public Sprite avatar;
    [SerializeField] public bool fromPlayer = false;
    [SerializeField] public string title;
    [SerializeField] public string content;
    [SerializeField] public Download download;
    [SerializeField] public int height= 2;

    [SerializeField] public List<Mail> chain;
    [SerializeField] public bool isResponse = false;
    [SerializeField] public Mail replyingTo = null;

    [SerializeField] public MailRenderer mr;

    [SerializeField] public string[] answerOptions;
    [SerializeField] public string[] answerOptionsAfterPlaying;
    [SerializeField] public Mail[] replyToAnswers;

    [SerializeField] public Mail openOnRead;
    [SerializeField] public Mail openAfterPlaying;

    [SerializeField] public Font customFont;
}
