using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWithActions : Button
{

    public Action OnHoverEnter;
    public Action OnHoverExit;
    public Action OnClickBegin;
    public Action OnClickEnd;

    
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        OnHoverEnter?.Invoke();
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        OnHoverExit?.Invoke();
    }
}
