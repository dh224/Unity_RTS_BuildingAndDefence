using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }
    [SerializeField] private RectTransform canvasRectTransform;
    private TextMeshProUGUI textMeshProUGUI;
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    private void Awake()
    {
       rectTransform = GetComponent<RectTransform>();
       textMeshProUGUI = transform.Find("Text").GetComponent<TextMeshProUGUI>();
       backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
       Instance = this;
       Hide();
    }

    private void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x ;
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }
        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetText(string tooltipText)
    {
        textMeshProUGUI.SetText(tooltipText);
        textMeshProUGUI.ForceMeshUpdate();
        Vector2 textSize = textMeshProUGUI.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }

    public void Show(string tooltipText)
    {
        gameObject.SetActive(true);
        SetText(tooltipText);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
