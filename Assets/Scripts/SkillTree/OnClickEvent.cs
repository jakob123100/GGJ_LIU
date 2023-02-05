using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickEvent : MonoBehaviour, IPointerClickHandler
{
    public event EventHandler Clicked;

    public static OnClickEvent CreateComponent(GameObject gameObject)
    {
        OnClickEvent onClickEvent = gameObject.AddComponent<OnClickEvent>();
        return onClickEvent;
    }

	public void OnPointerClick(PointerEventData eventData)
	{
        Clicked?.Invoke(this, EventArgs.Empty);
	}
}
