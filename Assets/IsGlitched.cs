using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGlitched : MonoBehaviour {

    [SerializeField] GlitchController glitchController;
    [Range(0, 10)]
    [SerializeField]
    int level;
    private void OnEnable()
    {
        glitchController.Activate(level);
    }
    private void OnDisable()
    {
        glitchController.Deactivate();
    }
}
