using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenItemManager : MonoBehaviour
{
    public static InvenItemManager instance = null;
    [SerializeField] private GameObject content;
    public enum Tab { Whole, Ingre, Potion, Etc }
    [SerializeField] Tab currentTab;
    public enum Ingredient { WaterBloom = 0, WindBloom, LifeLeaf, MadMushroom, RainbowCap, Shadow, Thunder, WaterCap, WitchMushroom }
    public enum Type { Ingredient, Potion, Etc }
    public enum Potion { Heal, Poison }
    public enum BottleShape { normal = 0, one, two, three}
    public enum BottleSticker { normal = 0, one, two, three}
    public enum StickerIcon { heart, poison }

    public GameObject MakeRoom;
    public GameObject[] Prefabs;
    public bool showShadow = false;
    [SerializeField] private GameObject invenShadow;
    
    public int[] IngreQuantity = { 2, 2, 0, 0, 0, 0, 0, 0, 0 };
    public int IngreType = 2; //몇 종류 가지고 있는지
    [SerializeField] private Sprite[] ingreIcons;
    [SerializeField] private MoveDetail[] moves;

    [SerializeField] private Sprite[] potionBottle;
    [SerializeField] private Sprite[] potionLabel;
    public int PotionType = 0;
    public List<PotionDetail> Potions = new List<PotionDetail>();
    public List<int> PotionQuantity = new List<int>();

    public int EtcType = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(this.gameObject);
        }
        UpdateInventory();
    }
    private void Update()
    {
        if (showShadow)
        {
            invenShadow.SetActive(true);
        }
        else
        {
            invenShadow.SetActive(false);
        }
    }
    public void CheckType()
    {
        IngreType = 0;
        for (int i =0; i < IngreQuantity.Length; i++)
        {
            if (IngreQuantity[i] > 0)
            {
                IngreType++;
            }
        }
        PotionType = Potions.Count;
        
    }

    public void UpdateInventory()
    {
        if (currentTab == Tab.Whole)
        {
            CheckType();
            if (IngreType > 0)
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
                        for (int k = 0 + m; k < IngreQuantity.Length; k++)
                        {
                            if (IngreQuantity[k] > 0)
                            {
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetComponent<Image>().sprite = ingreIcons[k]; //버튼 이미지 바꾸기
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnType = Type.Ingredient; //해당 버튼의 타입은 ingredient
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnIngre = (Ingredient)k; //버튼의 재료 타입 기억
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().count = IngreQuantity[k];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().move = moves[k];
                                if (IngreQuantity[k] < 10)
                                {
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = IngreQuantity[k].ToString(); //해당 재료의 개수
                                }//한자리
                                else
                                {
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = IngreQuantity[k].ToString(); //해당 재료의 개수
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
            }
            else
            {
                content.transform.GetChild(0).gameObject.SetActive(false); //ingre Line 끄기
            }
            if (PotionType > 0)
            {
                int m = 0;
                content.transform.GetChild(4).gameObject.SetActive(true); //potion Line 키기
            }
        }
        else if (currentTab == Tab.Ingre)
        {
            CheckType();
            if (IngreType > 0)
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
                        for (int k = 0 + m; k < IngreQuantity.Length; k++)
                        {
                            if (IngreQuantity[k] > 0)
                            {
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetComponent<Image>().sprite = ingreIcons[k]; //버튼 이미지 바꾸기
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnType = Type.Ingredient; //해당 버튼의 타입은 ingredient
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnIngre = (Ingredient)k; //버튼의 재료 타입 기억
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().count = IngreQuantity[k];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().move = moves[k]; //이동 미리보기용
                                if (IngreQuantity[k] < 10)
                                {
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = IngreQuantity[k].ToString(); //해당 재료의 개수
                                }//한자리
                                else
                                {
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = IngreQuantity[k].ToString(); //해당 재료의 개수
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
            }
        }
        else if (currentTab == Tab.Potion)
        {
            CheckType();
            if (PotionType > 0)
            {
                int m = 0;
                content.transform.GetChild(0).gameObject.SetActive(true); //potion Line 키기
                for (int i = 0; i < 9; i++)
                {
                    int j = i / 3;
                    if (i < PotionType)
                    {
                        content.transform.GetChild(j + 1).gameObject.SetActive(true);
                        content.transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.SetActive(true);
                        for (int k = 0 + m; k < PotionType; k++)
                        {
                            if (PotionQuantity[k] > 0) //여기 아래 포션관련으로 바꾸기
                            {
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetComponent<Image>().sprite = ingreIcons[k]; //버튼 이미지 바꾸기
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnType = Type.Ingredient; //해당 버튼의 타입은 ingredient
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnIngre = (Ingredient)k; //버튼의 재료 타입 기억
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().count = IngreQuantity[k];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().move = null;
                                if (IngreQuantity[k] < 10)
                                {
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = IngreQuantity[k].ToString(); //해당 재료의 개수
                                }//한자리
                                else
                                {
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = IngreQuantity[k].ToString(); //해당 재료의 개수
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
            }
        }
        else if (currentTab == Tab.Etc)
        {

        }
    }
}
