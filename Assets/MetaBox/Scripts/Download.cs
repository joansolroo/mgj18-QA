using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Download : ScriptableObject {

    [SerializeField] public string filename;
    [SerializeField] public string description;
    [SerializeField] public string game; //probably this will be something else, like a scene

    [SerializeField] public MailRenderer mailRenderer;

}
