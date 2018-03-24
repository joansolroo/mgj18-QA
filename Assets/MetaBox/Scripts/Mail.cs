using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : ScriptableObject {

    [SerializeField] public Sprite avatar;
    [SerializeField] public string title;
    [SerializeField] public string content;
    [SerializeField] public Download download;
    [SerializeField] public int height= 2;
}
