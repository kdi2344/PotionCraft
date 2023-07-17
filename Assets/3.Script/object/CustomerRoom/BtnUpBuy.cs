using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnUpBuy : MonoBehaviour
{
    public int index;

    public void BtnLeftClick()
    {
        SoundManager.instance.PlayEffect("btn");
        FindObjectOfType<CustomerManager>().SellQuantity[index]--;
        FindObjectOfType<CustomerManager>().ShopperQuantity[index]++;
        FindObjectOfType<ShopInventory>().UpdateInventory();
        FindObjectOfType<SellInventory>().UpdateInventory();
    }
}
