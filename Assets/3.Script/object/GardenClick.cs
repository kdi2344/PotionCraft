using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenClick : MonoBehaviour
{
    [SerializeField] private int ingreType;
    private void OnMouseDown()
    {
        FindObjectOfType<InvenItemManager>().IngreQuantity[ingreType]++;
        FindObjectOfType<InvenItemManager>().UpdateInventory();
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
}
