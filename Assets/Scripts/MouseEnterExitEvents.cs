using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEnterExitEvents : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

    public event EventHandler OnMouseEnter, OnMouseExit;
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        OnMouseEnter?.Invoke(this,EventArgs.Empty);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        OnMouseExit?.Invoke(this,EventArgs.Empty);
    }
}
