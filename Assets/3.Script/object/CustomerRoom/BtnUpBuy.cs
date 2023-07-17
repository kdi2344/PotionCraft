using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnUpBuy : MonoBehaviour
{
    public int index;

    public void BtnLeftClick()
    {
        FindObjectOfType<CustomerManager>().SellQuantity[index]--;
        FindObjectOfType<CustomerManager>().ShopperQuantity[index]++;
        FindObjectOfType<ShopInventory>().UpdateInventory();
        FindObjectOfType<SellInventory>().UpdateInventory();
    }
}
