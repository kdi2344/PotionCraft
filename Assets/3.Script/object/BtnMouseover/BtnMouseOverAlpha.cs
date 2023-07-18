using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnMouseOverAlpha : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float originalAlpha = 0.5f; 
    [SerializeField] float changeAlpha = 0.8f; 
    private void OnEnable()
    {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, originalAlpha);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, changeAlpha);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, originalAlpha);
    }
}
