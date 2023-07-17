using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInventory : MonoBehaviour
{
    public static InvenItemManager instance = null;
    //[SerializeField] private GameObject content;
    //public enum Tab { Whole, Ingre, Potion, Etc }
    //[SerializeField] Tab currentTab;
    //public enum Type { Ingredient, Potion, Etc }
    //Ingredient { WaterBloom = 0, WindBloom, LifeLeaf, MadMushroom, RainbowCap, Shadow, Thunder, WaterCap, WitchMushroom }

    //public int[] ShopQuantity = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int IngreType = 0; //몇 종류 가지고 있는지
    [SerializeField] private Sprite[] ingreIcons;


    private void Awake()
    {
        UpdateInventory();
    }
    public void CheckType()
    {
        IngreType = 0;
        for (int i = 0; i < CustomerManager.instance.ShopperQuantity.Length; i++)
        {
            if (CustomerManager.instance.ShopperQuantity[i] > 0)
            {
                IngreType++;
            }
        }
    }

    public void UpdateInventory()
    {
        CheckType();
        if (IngreType > 0) 
        {
            int m = 0;
            transform.GetChild(0).gameObject.SetActive(true); //ingre Line 키기
            for (int i = 0; i < 9; i++)
            {
                int j = i / 3;
                if (i < IngreType)
                {
                    transform.GetChild(j + 1).gameObject.SetActive(true);
                    transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.SetActive(true);
                    for (int k = 0 + m; k < CustomerManager.instance.ShopperQuantity.Length; k++)
                    {
                        if (CustomerManager.instance.ShopperQuantity[k] > 0)
                        {
                            transform.GetChild(j + 1).GetChild(i % 3).GetChild(0).GetComponent<Image>().sprite = ingreIcons[k]; //버튼 이미지 바꾸기
                            transform.GetChild(j + 1).GetChild(i % 3).GetComponent<BtnLeftBuy>().index = k;
                            //content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().move = InvenItemManager.moves[k];
                            transform.GetChild(j + 1).GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(true);
                            transform.GetChild(j + 1).GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(false);
                            transform.GetChild(j + 1).GetChild(i % 3).GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = CustomerManager.instance.ShopperQuantity[k].ToString(); //해당 재료의 개수
                            transform.GetChild(j + 1).GetChild(i % 3).GetChild(2).GetChild(4).GetComponent<Text>().text = CustomerManager.instance.ShopperPrice[k].ToString(); //해당 재료의 가격
                            m = k + 1;
                            break;
                        }
                    }
                }
                else
                {
                    if (transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.activeSelf) transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.SetActive(false);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                int a = 0;
                for (int j = 0; j < transform.GetChild(i + 1).childCount; j++)
                {
                    if (transform.GetChild(i + 1).GetChild(j).gameObject.activeSelf) a++;
                }
                if (a == 0) //자식들이 다 꺼져있으면
                {
                    transform.GetChild(i + 1).gameObject.SetActive(false);
                }
                else
                {
                    transform.GetChild(i + 1).gameObject.SetActive(true);
                }
            } //slotLine 키고 끄기
        }
        else
        {
            for (int i =0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
