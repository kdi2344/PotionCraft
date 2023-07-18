using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBottleShow : MonoBehaviour
{
    public int type = 0;

    [SerializeField] GameObject select1;
    [SerializeField] GameObject select2;
    [SerializeField] GameObject select3;
    [SerializeField] GameObject select4;
    [SerializeField] GameObject select5;
    [SerializeField] GameObject select6;
    [SerializeField] GameObject select7;
    [SerializeField] GameObject select8;
    private void Update()
    {
        if (type ==0)
        {
            switch (GameManager.instance.currentBottleShape)
            {
                case InvenItemManager.BottleShape.normal:
                    select1.SetActive(true);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.BottleShape.two:
                    select1.SetActive(false);
                    select2.SetActive(true);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.BottleShape.three:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(true);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.BottleShape.four:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(true);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.BottleShape.five:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(true);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.BottleShape.six:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(true);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.BottleShape.seven:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(true);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.BottleShape.eight:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(true);
                    break;
            }
        } //bottle
        else if (type == 1) //Sticker
        {
            switch (GameManager.instance.currentSticker)
            {
                case InvenItemManager.BottleSticker.normal:
                    select1.SetActive(true);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.BottleSticker.two:
                    select1.SetActive(false);
                    select2.SetActive(true);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.BottleSticker.three:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(true);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.BottleSticker.four:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(true);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.BottleSticker.five:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(true);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.BottleSticker.six:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(true);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.BottleSticker.seven:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(true);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.BottleSticker.eight:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(true);
                    break;
            }
        }
        else if (type == 2) //Icon
        {
            switch (GameManager.instance.currentPotionIcon)
            {
                case InvenItemManager.Potion.None:
                    select1.SetActive(true);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.Potion.Heal:
                    select1.SetActive(false);
                    select2.SetActive(true);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.Potion.Poison:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(true);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.Potion.Sleep:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(true);
                    select5.SetActive(false);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.Potion.Fire:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(true);
                    select6.SetActive(false);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
                case InvenItemManager.Potion.Tree:
                    select1.SetActive(false);
                    select2.SetActive(false);
                    select3.SetActive(false);
                    select4.SetActive(false);
                    select5.SetActive(false);
                    select6.SetActive(true);
                    select7.SetActive(false);
                    select8.SetActive(false);
                    break;
            }
        }
    }

    public void BtnClick(int bottle)
    {
        GameManager.instance.currentBottleShape = (InvenItemManager.BottleShape)bottle;
    }

    public void BtnStickerClick(int sticker)
    {
        GameManager.instance.currentSticker = (InvenItemManager.BottleSticker)sticker;
    }
    public void BtnIconClick(int icon)
    {
        GameManager.instance.currentPotionIcon = (InvenItemManager.Potion)icon;
    }
}
