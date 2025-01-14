using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingame_CanvasCtrl : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.back,
                         mainCamera.transform.rotation * Vector3.up);
    }
}
