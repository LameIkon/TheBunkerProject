using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    protected Camera mainCam;
    protected Vector3 mousePos;

    // Start is called before the first frame update
    void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
}
