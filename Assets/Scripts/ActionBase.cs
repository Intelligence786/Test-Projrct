using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ActionBase : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Physics.Raycast(Input.mousePosition, Vector3.forward, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out Character character))
            {
                DoSomething();
            }
        }
    }

    protected virtual void DoSomething()
    {

    }
}
