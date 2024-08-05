using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurRenderer : MonoBehaviour
{
    [SerializeField] private Camera _blurCamera;
    [SerializeField] private Material _blurMaterial;

    // Start is called before the first frame update
    void Start()
    {
        if (_blurCamera.targetTexture != null)
        {
            _blurCamera.targetTexture.Release();
        }
        _blurCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        _blurMaterial.SetTexture("_RenTex", _blurCamera.targetTexture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
