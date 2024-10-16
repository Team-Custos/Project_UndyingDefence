using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_VirtualCamManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform cameraPivot;

    public float moveSpeed = 10.0f;
    public float rotationSpeed = 100.0f;
    public float zoomSpeed = 2.0f;
    public float zoomMin = 5.0f;
    public float zoomMax = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        CameraZoom();
    }

    void CameraMove()
    {
        // ī�޶� �̵�
        // W, A, S, D Ű �Է�
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        cameraPivot.position += moveDirection * moveSpeed * Time.deltaTime;

        // ī�޶� ȸ��
        Vector3 currentRotation = virtualCamera.transform.eulerAngles;

        if (Input.GetKey(KeyCode.Q))
        {
            currentRotation.y -= rotationSpeed * Time.deltaTime;
        }

        // E Ű�� Y�� ȸ�� ����
        if (Input.GetKey(KeyCode.E))
        {
            currentRotation.y += rotationSpeed * Time.deltaTime;
        }

        virtualCamera.transform.eulerAngles = new Vector3(45, currentRotation.y, 0);
    }

    void CameraZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        
        if(scrollInput != 0)
        {
            float currentFov = virtualCamera.m_Lens.FieldOfView;
            currentFov -= scrollInput * zoomSpeed * 100f * Time.deltaTime;
            currentFov = Mathf.Clamp(currentFov, zoomMin, zoomMax);
            virtualCamera.m_Lens.FieldOfView = currentFov;
        }
    }
}
