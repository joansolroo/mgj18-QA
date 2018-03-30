using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGlitched : MonoBehaviour {

    [SerializeField] GlitchController glitchController;
    [Range(0, 10)]
    [SerializeField]
    float level;
    [SerializeField] bool vsync = false;
    [SerializeField] bool UseTrigger = false;
    private void OnEnable()
    {
        glitchController = GlitchController.instance;
        if (!UseTrigger)
        {
            glitchController.Activate(level, vsync);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (UseTrigger && other.gameObject.tag == "Player")
        {
            glitchController.Activate(level, vsync);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (UseTrigger && other.gameObject.tag == "Player")
        {
            glitchController.Activate(level, vsync);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (UseTrigger && other.gameObject.tag == "Player")
        {
            glitchController.Deactivate();
        }
    }
    private void OnDisable()
    {
        if (glitchController)
        {
            glitchController.Deactivate();
        }
    }
}
