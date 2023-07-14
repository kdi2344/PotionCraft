using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInventory : MonoBehaviour
{
    public static InvenItemManager instance = null;
    [SerializeField] private GameObject content;
    //public enum Tab { Whole, Ingre, Potion, Etc }
    //[SerializeField] Tab currentTab;
    public enum Type { Ingredient, Potion, Etc }
    //Ingredient { WaterBloom = 0, WindBloom, LifeLeaf, MadMushroom, RainbowCap, Shadow, Thunder, WaterCap, WitchMushroom }

    public int[] ShopQuantity = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int IngreType = 2; //몇 종류 가지고 있는지
    [SerializeField] private Sprite[] ingreIcons;


    private void Awake()
    {
        UpdateInventory();
    }
    public void CheckType()
    {
        IngreType = 0;
        for (int i = 0; i < GameManager.instance.IngreQuantity.Length; i++)
        {
            if (GameManager.instance.IngreQuantity[i] > 0)
            {
                IngreType++;
            }
        }
    }

    public void UpdateInventory()
    {
        CheckType();
        if (IngreType > 0) //재료부분
        {
            int m = 0;
            content.transform.GetChild(0).gameObject.SetActive(true); //ingre Line 키기
            for (int i = 0; i < 9; i++)
            {
                int j = i / 3;
                if (i < IngreType)
                {
                    content.transform.GetChild(j + 1).gameObject.SetActive(true);
                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.SetActive(true);
                    for (int k = 0 + m; k < GameManager.instance.IngreQuantity.Length; k++)
                    {
                        if (GameManager.instance.IngreQuantity[k] > 0)
                        {
                            content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetComponent<Image>().sprite = ingreIcons[k]; //버튼 이미지 바꾸기
                            content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnType = InvenItemManager.Type.Ingredient; //해당 버튼의 타입은 ingredient
                            content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnIngre = (InvenItemManager.Ingredient)k; //버튼의 재료 타입 기억
                            content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().count = GameManager.instance.IngreQuantity[k];
                            //content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().move = InvenItemManager.moves[k];
                            if (GameManager.instance.IngreQuantity[k] < 10)
                            {
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(true);
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(false);
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = GameManager.instance.IngreQuantity[k].ToString(); //해당 재료의 개수
                            }//한자리
                            else
                            {
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(false);
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(true);
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = GameManager.instance.IngreQuantity[k].ToString(); //해당 재료의 개수
                            }//두자리

                            m = k + 1;
                            break;
                        }
                    }
                }
                else
                {
                    if (content.transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.activeSelf) content.transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.SetActive(false);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                int a = 0;
                for (int j = 0; j < content.transform.GetChild(i + 1).childCount; j++)
                {
                    if (content.transform.GetChild(i + 1).GetChild(j).gameObject.activeSelf) a++;
                }
                if (a == 0) //자식들이 다 꺼져있으면
                {
                    content.transform.GetChild(i + 1).gameObject.SetActive(false);
                }
                else
                {
                    content.transform.GetChild(i + 1).gameObject.SetActive(true);
                }
            } //slotLine 키고 끄기
        }
        else
        {
            content.transform.GetChild(0).gameObject.SetActive(false); //ingre Line 끄기
            content.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            content.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
