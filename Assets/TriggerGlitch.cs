using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGlitch : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(HardCrash());
        }
    }

    void OnTriggerExit(Collider other)
    {
    }

    IEnumerator HardCrash()
    {

        // Debug.Log("introooo");
        // yield return new WaitForSeconds(timeUntilCrash);
        GlitchController.instance.Activate(4);
        yield return new WaitForSeconds(2);
        OSHandler.instance.StartCoroutine("HardCrash");
        OSHandler.Close("world1");
    }
}
