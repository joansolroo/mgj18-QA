﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    Vector2 oldPos;
    Vector2 newPos;
    Vector3 euler;

    public static bool used = false;
    public static CameraRotation instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] Transform follow;
    void OnGUI()
    {
        Event m_Event = Event.current;

        if (m_Event.type == EventType.MouseDown)
        {
            oldPos = m_Event.mousePosition;
            euler = this.transform.localEulerAngles;
          //  Debug.Log("Mouse Down.");
        }

        if (m_Event.type == EventType.MouseDrag)
        {
            newPos = m_Event.mousePosition;
            if (newPos.x != oldPos.x)
            {
                used = true;
                euler.y += (newPos.x - oldPos.x)*0.25f;
                oldPos = m_Event.mousePosition;
                this.transform.localEulerAngles = euler;
            }
         //   Debug.Log("Mouse Dragged.");
        }
        if (follow != null)
        {
            transform.position = follow.position;
        }

        if (m_Event.type == EventType.MouseUp)
        {
//Debug.Log("Mouse Up.");
        }
    }
}
