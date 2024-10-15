using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonState
    {
        Default,
        Hovered,
        Clicked,
        ClickedAfter,
        Disabled
    }

    public Image buttonImage;
    public Color defaultColor = new Color(1f, 0.5f, 0f);
    public Color hoveredColor = new Color(1f, 0.8f, 0.6f);
    public Color clickedColor = Color.yellow;
    public Color disabledColor = Color.gray;
    public float animationDuration = 0.2f;

    private ButtonState currentState = ButtonState.Default;


    public RectTransform buttonRectTransform;
    public Text buttonText;
    public Vector3 pressedScale = new Vector3(1.2f, 1.2f, 1f);
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
    }

    // ��ư�� ������ ��
    public void OnPointerDown(PointerEventData eventData)
    {
        // ��ư ũ��� �ؽ�Ʈ ũ�� Ȯ��
        StartCoroutine(ScaleButton(pressedScale));
        if (buttonText != null)
        {
            StartCoroutine(ScaleText(originalTextScale * textScaleFactor));
        }
    }

    // ��ư���� ���콺�� ���� ��
    public void OnPointerUp(PointerEventData eventData)
    {
        // ��ư ũ��� �ؽ�Ʈ ũ�⸦ ������� �ǵ���
        StartCoroutine(ScaleButton(normalScale));
        if (buttonText != null)
        {
            StartCoroutine(ScaleText(originalTextScale));
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

    // ��ư ���¿� ���� ���� ����
    public void ChangeButtonState(ButtonState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case ButtonState.Default:
                StartCoroutine(ChangeColor(defaultColor));
                break;
            case ButtonState.Hovered:
                StartCoroutine(ChangeColor(hoveredColor));
                break;
            case ButtonState.Clicked:
                StartCoroutine(ChangeColor(clickedColor));
                break;
            case ButtonState.ClickedAfter:
                StartCoroutine(ChangeColor(defaultColor));
                break;
            case ButtonState.Disabled:
                StartCoroutine(ChangeColor(disabledColor));
                break;
        }
    }

    // ���� ���� �ڷ�ƾ
    private IEnumerator ChangeColor(Color targetColor)
    {
        Color initialColor = buttonImage.color;
        float time = 0f;

        while (time < animationDuration)
        {
            time += Time.deltaTime;
            //buttonImage.color = Color.Lerp(initialColor, targetColor, time / animationDuration);
            yield return null;
        }

        buttonImage.color = targetColor; // ���� ���� ����
    }


    // ���콺�� ��ư ���� �ö�� �� ȣ��
    public void OnPointerEnter()
    {
        if (currentState != ButtonState.Disabled)
            ChangeButtonState(ButtonState.Hovered);
    }

    // ���콺�� ��ư���� ��� �� ȣ��
    public void OnPointerExit()
    {
        if (currentState != ButtonState.Disabled)
            ChangeButtonState(ButtonState.Default);
    }

    // ��ư�� Ŭ���� �� ȣ��
    public void OnPointerClick()
    {
        if (currentState != ButtonState.Disabled)
            ChangeButtonState(ButtonState.Clicked);
    }

    // ��ư ��Ȱ��ȭ
    public void DisableButton()
    {
        ChangeButtonState(ButtonState.Disabled);
    }


    // ��ư Ȱ��ȭ
    public void EnableButton()
    {
        ChangeButtonState(ButtonState.Default);
    }


}
