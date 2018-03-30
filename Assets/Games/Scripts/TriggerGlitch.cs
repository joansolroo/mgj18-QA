﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGlitch : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Startup.instance.NextChapter();
        }
    }
   
}
