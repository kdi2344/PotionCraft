using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion Data", menuName = "Scriptable Object/Potion data")]


public class PotionDetail : ScriptableObject
{
    public string PotionName;
    //public enum Ingredient { WaterBloom, WindBloom, LifeLeaf, MadMushroom, RainbowCap, Shadow, Thunder, WaterCap, WitchMushroom }
    //public enum Potion { Heal, Poison }
    public List<InvenItemManager.Ingredient> ingredients;
    public List<InvenItemManager.Potion> effect;
    public InvenItemManager.BottleShape bottle;
    public InvenItemManager.BottleSticker sticker;
    public InvenItemManager.Potion icon;
    public int level;   
}
