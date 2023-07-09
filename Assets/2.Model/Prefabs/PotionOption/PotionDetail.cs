using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDetail : ScriptableObject
{
    string PotionName;
    //public enum Ingredient { WaterBloom, WindBloom, LifeLeaf, MadMushroom, RainbowCap, Shadow, Thunder, WaterCap, WitchMushroom }
    //public enum Potion { Heal, Poison }
    List<InvenItemManager.Ingredient> ingredients;
    List<InvenItemManager.Potion> effect;
    InvenItemManager.BottleShape bottle;
    InvenItemManager.BottleSticker sticker;
    InvenItemManager.StickerIcon icon;

    Transform[] points;
}
