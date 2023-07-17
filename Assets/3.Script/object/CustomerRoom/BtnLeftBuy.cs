using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLeftBuy : MonoBehaviour
{
    public int index;

    public void BtnLeftClick()
    {
        FindObjectOfType<CustomerManager>().ShopperQuantity[index]--;
        FindObjectOfType<CustomerManager>().SellQuantity[index]++;
        FindObjectOfType<ShopInventory>().UpdateInventory();
        FindObjectOfType<SellInventory>().UpdateInventory();
    }
}
