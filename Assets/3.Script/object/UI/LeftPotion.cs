using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftPotion : MonoBehaviour
{
    [SerializeField] Sprite[] bottleOutlines;
    [SerializeField] Sprite[] bottleColors;
    [SerializeField] Sprite[] bottleCorks;
    [SerializeField] Sprite[] bottleShadows;
    [SerializeField] Sprite[] bottleScratches;
    [SerializeField] Sprite[] stickerColors;
    [SerializeField] Sprite[] stickerOutlines;
    [SerializeField] Sprite[] icons;

    [SerializeField] Image bottle;
    [SerializeField] Image bottleColor;
    [SerializeField] Image sticker;
    [SerializeField] Image stickerColor;
    [SerializeField] Image icon;

    public void UpdatePotion()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = bottleOutlines[(int)GameManager.instance.currentBottleShape];
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = bottleColors[(int)GameManager.instance.currentBottleShape];
        transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = bottleCorks[(int)GameManager.instance.currentBottleShape];
        transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = bottleShadows[(int)GameManager.instance.currentBottleShape];
        transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = bottleScratches[(int)GameManager.instance.currentBottleShape];

        transform.GetChild(5).GetChild(0).GetComponent<SpriteRenderer>().sprite = stickerOutlines[(int)GameManager.instance.currentSticker];
        transform.GetChild(5).GetChild(1).GetComponent<SpriteRenderer>().sprite = stickerColors[(int)GameManager.instance.currentSticker];
        transform.GetChild(6).GetComponent<SpriteRenderer>().sprite = icons[(int)GameManager.instance.currentPotionIcon];

        bottle.sprite = bottleOutlines[(int)GameManager.instance.currentBottleShape];
        bottleColor.sprite = bottleScratches[(int)GameManager.instance.currentBottleShape];
        sticker.sprite = stickerOutlines[(int)GameManager.instance.currentSticker];
        stickerColor.sprite = stickerColors[(int)GameManager.instance.currentSticker];
        icon.sprite = icons[(int)GameManager.instance.currentPotionIcon];
    }
}
