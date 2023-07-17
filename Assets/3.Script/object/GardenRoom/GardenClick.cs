using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenClick : MonoBehaviour
{
    [SerializeField] private int ingreType;
    private void Awake()
    {
        transform.parent.GetChild(transform.parent.childCount - 1).gameObject.SetActive(false);
    }
    private void OnMouseEnter()
    {
        if (GetComponent<SpriteRenderer>().color.a == 1) transform.parent.GetChild(transform.parent.childCount - 1).gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        transform.parent.GetChild(transform.parent.childCount - 1).gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        DataManager.instance.nowData.IngreQuantity[ingreType]++;
        FindObjectOfType<InvenItemManager>().UpdateInventory();
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        transform.parent.GetChild(transform.parent.childCount - 1).gameObject.SetActive(false);
    }
}
