using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    //public PotionDetail PotionData;
    public InvenItemManager.BottleShape bottle;
    public InvenItemManager.Potion effect;
    public InvenItemManager.Potion icon;
    public InvenItemManager.BottleSticker sticker;
    public int index;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.instance.PlayEffect("bottle");
    }
}
