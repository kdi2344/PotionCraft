using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionIngredientsShow : MonoBehaviour
{
    Pot pot;
    [SerializeField] private GameObject[] ingreSlots;
    [SerializeField] private Sprite[] ingreIcon;
    //{ WaterBloom = 0, WindBloom, LifeLeaf, MadMushroom, RainbowCap, Shadow, Thunder, WaterCap, WitchMushroom, Water }
    private void Awake()
    {
        pot = FindObjectOfType<Pot>();
    }
    private void Update()
    {
        ShowIngredient();
    }
    private void ShowIngredient()
    {
        int m = 0;
        for (int i =0; i < 9; i++)
        {
            if (pot.containIngredients[i] > 0)
            {
                transform.GetChild(m+1).gameObject.SetActive(true);
                ingreSlots[m].GetComponent<SpriteRenderer>().sprite = ingreIcon[i];
                ingreSlots[m].transform.GetChild(0).GetComponent<TextMesh>().text = pot.containIngredients[i].ToString();
                m++;
            }
        }
        if (m == 0)
        {
            for (int i = 0; i < 9; i++)
            {
                transform.GetChild(i+1).gameObject.SetActive(false);
            }
        }
    }

    public void ResetIngredient()
    {
        for (int i =0; i < pot.containIngredients.Length; i++)
        {
            pot.containIngredients[i] = 0;
        }
    }
}
