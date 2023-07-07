using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvenButtonHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
{
    [SerializeField] ScrollRect ParentSR;
    private void Awake()
    {
        ParentSR = transform.parent.parent.parent.parent.GetComponent<ScrollRect>();
    }
    public void OnBeginDrag(PointerEventData e)
    {
        ParentSR.OnBeginDrag(e);
    }
    public void OnDrag(PointerEventData e)
    {
        ParentSR.OnDrag(e);
    }
    public void OnEndDrag(PointerEventData e)
    {
        ParentSR.OnEndDrag(e);
    }
    public void OnScroll(PointerEventData e)
    {
        ParentSR.OnScroll(e);
    }
}
