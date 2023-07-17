using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionActive : MonoBehaviour
{
    public InvenItemManager.Potion effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("mapBottle"))
        {
            GameManager.instance.CanMakePotion = true;
            collision.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            collision.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            collision.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            GameManager.instance.currentPotionEffect = effect;
            GameManager.instance.currentPotionIcon = effect;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("mapBottle"))
        {
            GameManager.instance.CanMakePotion = false;
            collision.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            collision.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            collision.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            GameManager.instance.currentPotionEffect = InvenItemManager.Potion.None;
            GameManager.instance.currentPotionIcon = InvenItemManager.Potion.None;
        }
    }
}
