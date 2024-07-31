using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UD_UIClickEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // UI ��Ҹ� Ŭ���� ���� �̺�Ʈ�� ���޵ǵ��� ����
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // Ŭ���� ���� UI ��Ҷ�� ���� ������Ʈ�� Ŭ�� �̺�Ʈ ���޵��� �ʵ��� ����
                Debug.Log("UI Element Clicked");
            }
            else
            {
                // UI ��Ұ� �ƴ� �ٸ� ���� ������Ʈ�� Ŭ���� ���
                Debug.Log("Game Object Clicked");
            }
        }
    }
}
