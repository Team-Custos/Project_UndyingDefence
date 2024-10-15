using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform buttonRectTransform;
    public Text buttonText;
    public Vector3 pressedScale = new Vector3(0.9f, 0.9f, 1f);
    public Vector3 normalScale = Vector3.one;
    public float scaleDuration = 0.1f;
    public float textScaleFactor = 1.2f; 

    private Vector3 originalTextScale;

    private void Start()
    {
        // �ʱ� �ؽ�Ʈ ũ�� ����
        if (buttonText != null)
        {
            originalTextScale = buttonText.transform.localScale;
        }

        buttonRectTransform.localScale = normalScale;
    }

    // ��ư�� ������ ��
    public void OnPointerDown(PointerEventData eventData)
    {
        // ��ư ũ�⸦ �۰� ����
        StartCoroutine(ScaleButton(pressedScale));
        if (buttonText != null)
        {
            StartCoroutine(ScaleText(originalTextScale * 0.9f));  // �ؽ�Ʈ ũ�⵵ ����
        }
    }


    // ��ư���� ���콺�� ���� ��
    public void OnPointerUp(PointerEventData eventData)
    {
        // ��ư ũ�⸦ ������� �ǵ���
        StartCoroutine(ScaleButton(normalScale));
        if (buttonText != null)
        {
            StartCoroutine(ScaleText(originalTextScale));  // �ؽ�Ʈ ũ�⵵ ������� �ǵ���
        }
    }

    // ��ư ũ�� ���� �ڷ�ƾ
    private IEnumerator ScaleButton(Vector3 targetScale)
    {
        Vector3 currentScale = buttonRectTransform.localScale;
        float time = 0f;

        while (time < scaleDuration)
        {
            time += Time.deltaTime;
            buttonRectTransform.localScale = Vector3.Lerp(currentScale, targetScale, time / scaleDuration);
            yield return null;
        }

        buttonRectTransform.localScale = targetScale;
    }

    // �ؽ�Ʈ ũ�� ���� �ڷ�ƾ
    private IEnumerator ScaleText(Vector3 targetScale)
    {
        Vector3 currentScale = buttonText.transform.localScale;
        float time = 0f;

        while (time < scaleDuration)
        {
            time += Time.deltaTime;
            buttonText.transform.localScale = Vector3.Lerp(currentScale, targetScale, time / scaleDuration);
            yield return null;
        }

        buttonText.transform.localScale = targetScale;
    }

    

}
