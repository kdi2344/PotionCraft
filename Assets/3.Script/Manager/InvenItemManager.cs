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
    public enum Potion { None =0, Heal, Poison, Sleep, Fire, Tree }
    public enum BottleShape { normal = 0, one, two, three}
    public enum BottleSticker { normal = 0, one, two, three}
    //public enum StickerIcon { heart, poison }

    public GameObject MakeRoom;
    public GameObject[] Prefabs;
    public bool showShadow = false;
    [SerializeField] private GameObject invenShadow;
    
    //public int[] IngreQuantity = { 2, 2, 0, 0, 0, 0, 0, 0, 0 };
    public int IngreType = 2; //몇 종류 가지고 있는지
    [SerializeField] private Sprite[] ingreIcons;
    [SerializeField] private MoveDetail[] moves;

    [Header("포션 인벤토리 설정용")]
    public GameObject PotionPrefab;
    //color shadow scratch cork outline
    [SerializeField] public Sprite[] potionBottleColor;
    [SerializeField] public Sprite[] potionBottleShadow;
    [SerializeField] public Sprite[] potionBottleScratch;
    [SerializeField] public Sprite[] potionBottleCork;
    [SerializeField] public Sprite[] potionBottleOutline;
    //color outline icon
    [SerializeField] public Sprite[] potionStickerColors;
    [SerializeField] public Sprite[] potionStickerOutline;
    [SerializeField] public Sprite[] potionStickerIcon;
    public int PotionType = 0;
    //public PotionDetail[] PotionDetails;
    //public int[] PotionQuantity = { 0, 0, 0, 0, 0, 0, 0, 0, 0}; //인벤토리는 9칸만 됨

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
        for (int i =0; i < GameManager.instance.IngreQuantity.Length; i++)
        {
            if (GameManager.instance.IngreQuantity[i] > 0)
            {
                IngreType++;
            }
        }
        PotionType = 0;
        for (int i =0; i < GameManager.instance.PotionQuantity.Length; i++)
        {
            if (GameManager.instance.PotionQuantity[i] > 0)
            {
                PotionType++;
            }
        }
    }

    public void UpdateInventory()
    {
        if (currentTab == Tab.Whole)
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
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnType = Type.Ingredient; //해당 버튼의 타입은 ingredient
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnIngre = (Ingredient)k; //버튼의 재료 타입 기억
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().count = GameManager.instance.IngreQuantity[k];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().move = moves[k];
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
                for (int i =0; i < 3; i++)
                {
                    int a = 0;
                    for (int j = 0; j < content.transform.GetChild(i+1).childCount; j++)
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
            if (PotionType > 0) //포션 부분
            {
                int m = 0;
                content.transform.GetChild(4).gameObject.SetActive(true); //potion Line 
                for (int i = 0; i < 9; i++)
                {
                    int j = i / 3;
                    if (i < PotionType)
                    {
                        content.transform.GetChild(j + 5).gameObject.SetActive(true);
                        content.transform.GetChild(j + 5).transform.GetChild(i % 3).gameObject.SetActive(true);
                        for (int k = 0 + m; k < GameManager.instance.PotionQuantity.Length; k++)
                        {
                            if (GameManager.instance.PotionQuantity[k] > 0)
                            {                                                                  //potion    //bottle //color shadow scratch cork outline
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = potionBottleColor[(int)GameManager.instance.PotionDetails[k].bottle];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = GameManager.instance.PotionColors[(int)(GameManager.instance.PotionDetails[k].effect[0])];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = potionBottleShadow[(int)GameManager.instance.PotionDetails[k].bottle];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().sprite = potionBottleScratch[(int)GameManager.instance.PotionDetails[k].bottle];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(3).GetComponent<Image>().sprite = potionBottleCork[(int)GameManager.instance.PotionDetails[k].bottle];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(4).GetComponent<Image>().sprite = potionBottleOutline[(int)GameManager.instance.PotionDetails[k].bottle];
                                //sticker //color outline icon
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = potionStickerColors[(int)GameManager.instance.PotionDetails[k].sticker];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>().sprite = potionStickerOutline[(int)GameManager.instance.PotionDetails[k].sticker];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>().sprite = potionStickerIcon[(int)GameManager.instance.PotionDetails[k].icon];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnType = Type.Potion; //해당 버튼의 타입은 ingredient
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnPotion = k; //해당 버튼의 타입은 ingredient
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetComponent<IngredientPotion>().count = GameManager.instance.PotionQuantity[k];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetComponent<IngredientPotion>().potionInfo = GameManager.instance.PotionDetails[k];
                                if (GameManager.instance.PotionQuantity[k] < 10)
                                {
                                    content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = GameManager.instance.PotionQuantity[k].ToString(); //해당 재료의 개수
                                }//한자리
                                else
                                {
                                    content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = GameManager.instance.PotionQuantity[k].ToString(); //해당 재료의 개수
                                }//두자리

                                m = k + 1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (content.transform.GetChild(j + 5).transform.GetChild(i % 3).gameObject.activeSelf)
                        {
                            content.transform.GetChild(j + 5).transform.GetChild(i % 3).gameObject.SetActive(false);
                        }
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    int a = 0;
                    for (int j = 0; j < content.transform.GetChild(i + 5).childCount; j++)
                    {
                        if (content.transform.GetChild(i + 5).GetChild(j).gameObject.activeSelf) a++;
                    }
                    if (a == 0) //자식들이 다 꺼져있으면
                    {
                        content.transform.GetChild(i + 5).gameObject.SetActive(false);
                    }
                    else
                    {
                        content.transform.GetChild(i + 5).gameObject.SetActive(true);
                    }
                } //slotLine 확인해서 키고 끄기
            }
            else
            {
                content.transform.GetChild(4).gameObject.SetActive(false); //potion Line 끄기
                content.transform.GetChild(5).GetChild(0).gameObject.SetActive(false);
                content.transform.GetChild(5).gameObject.SetActive(false);
                content.transform.GetChild(6).gameObject.SetActive(false);
                content.transform.GetChild(7).gameObject.SetActive(false);
            }
        }
        else if (currentTab == Tab.Ingre)
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
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnType = Type.Ingredient; //해당 버튼의 타입은 ingredient
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnIngre = (Ingredient)k; //버튼의 재료 타입 기억
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().count = GameManager.instance.IngreQuantity[k];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().move = moves[k];
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
        //else if (currentTab == Tab.Potion)
        //{
        //    CheckType();
        //    if (PotionType > 0)
        //    {
        //        int m = 0;
        //        content.transform.GetChild(0).gameObject.SetActive(true); //potion Line 키기
        //        for (int i = 0; i < 9; i++)
        //        {
        //            int j = i / 3;
        //            if (i < PotionType)
        //            {
        //                content.transform.GetChild(j + 1).gameObject.SetActive(true);
        //                content.transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.SetActive(true);
        //                for (int k = 0 + m; k < PotionType; k++)
        //                {
        //                    if (PotionQuantity[k] > 0) //여기 아래 포션관련으로 바꾸기
        //                    {
        //                        content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetComponent<Image>().sprite = ingreIcons[k]; //버튼 이미지 바꾸기
        //                        content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnType = Type.Ingredient; //해당 버튼의 타입은 ingredient
        //                        content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnIngre = (Ingredient)k; //버튼의 재료 타입 기억
        //                        content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().count = IngreQuantity[k];
        //                        content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().move = null;
        //                        if (IngreQuantity[k] < 10)
        //                        {
        //                            content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(true);
        //                            content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(false);
        //                            content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = IngreQuantity[k].ToString(); //해당 재료의 개수
        //                        }//한자리
        //                        else
        //                        {
        //                            content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(false);
        //                            content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(true);
        //                            content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = IngreQuantity[k].ToString(); //해당 재료의 개수
        //                        }//두자리

        //                        m = k + 1;
        //                        break;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (content.transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.activeSelf) content.transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.SetActive(false);
        //            }
        //        }
        //    }
        //}
        //else if (currentTab == Tab.Etc)
        //{

        //}
    }
}
