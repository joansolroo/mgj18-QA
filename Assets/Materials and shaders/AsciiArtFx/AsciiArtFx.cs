using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class AsciiArtFx : MonoBehaviour
{
    public Color colorTint = Color.white;

    [Range(0, 1)]
    public float blendRatio = 1.0f;
    [Range(0, 1)]
    public float saturation = 1.0f;
    [Range(0.5f, 10.0f)]
    public float scaleFactor = 1.0f;

    [SerializeField] Shader shader;

    private Material _material;

    [SerializeField] Camera MaskCamera;
    RenderTexture mask;

    Material material {
        get {
            if (_material == null)
            {
                _material = new Material(shader);
                _material.hideFlags = HideFlags.HideAndDontSave;   
            }
            return _material;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.color = colorTint;
        material.SetFloat("_Alpha", blendRatio);
        material.SetFloat("_UseMask", MaskCamera == null?0:1);
        material.SetFloat("_Scale", scaleFactor);
        material.SetFloat("_Saturation", saturation);
        material.SetTexture("_Mask", mask);
        Graphics.Blit(source, destination, material);
    }

    void OnDisable ()
    {
        if (_material) DestroyImmediate(_material);   
    }

    private void Start()
    {
        if (MaskCamera != null)
        {
            mask = new RenderTexture(Camera.main.pixelWidth, Camera.main.pixelHeight, 0);
            MaskCamera.targetTexture = mask;
        }
    }
}
