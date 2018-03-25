using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    Vector2 oldPos;
    Vector2 newPos;
    Vector3 euler;

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
            euler.y += newPos.x - oldPos.x;
            oldPos = m_Event.mousePosition;
            this.transform.localEulerAngles = euler;
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
