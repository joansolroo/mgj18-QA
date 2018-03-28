using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ToggleUIItem))]
public class UnreadEmailNotifier : MonoBehaviour {

    [SerializeField] public int unreadCount = 0;
    [SerializeField] UnityEngine.UI.Text label;
    ToggleUIItem toggle;

    void Start()
    {
        toggle = GetComponent<ToggleUIItem>();
    }
    public void SetCount(int count)
    {
        if (count!=unreadCount){

            if(unreadCount>0 && count == 0)
            {
                toggle.Hide();
            }else if (unreadCount == 0 && count > 0)
            {
                toggle.Show();
            }
            else if(count> unreadCount )
            {
                toggle.Poke();   
            }
        }
        unreadCount = count;
        label.text = count.ToString();
    }
}
