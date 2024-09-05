using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PlaneDirection
{
    Vertical, Horizontal
}


public class ControlHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlaneDirection direction;
    [SerializeField] private int value;

    private bool isHolding = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        SetDirectionValue(value);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetDirectionValue(0);
        isHolding = false;
    }

    private void Update()
    {
        if (isHolding)
        {
            SetDirectionValue(value);
        }
    }

    private void SetDirectionValue(int v)
    {
        if (direction == PlaneDirection.Vertical)
        {
            InputController.Instance.SetMobileVertical(v);
        }
        else
        {
            InputController.Instance.SetMobileHorizontal(v);
        }
    }
}
