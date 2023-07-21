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
    public enum BottleShape { normal = 0, two, three, four, five, six, seven, eight }
    public enum BottleSticker { normal = 0, two, three, four, five, six, seven, eight }
    //public enum StickerIcon { heart, poison }

    public GameObject MakeRoom;
    public GameObject[] Prefabs;
    public bool showShadow = false;
    [SerializeField] private GameObject invenShadow;
    
    //public int[] IngreQuantity = { 2, 2, 0, 0, 0, 0, 0, 0, 0 };
    public int IngreType = 2; //�� ���� ������ �ִ���
    [SerializeField] private Sprite[] ingreIcons;
    [SerializeField] private MoveDetail[] moves;

    [Header("���� �κ��丮 ������")]
    public GameObject[] PotionPrefabs;
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
    //public int[] PotionQuantity = { 0, 0, 0, 0, 0, 0, 0, 0, 0}; //�κ��丮�� 9ĭ�� ��

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
        for (int i =0; i < DataManager.instance.nowData.IngreQuantity.Length; i++)
        {
            if (DataManager.instance.nowData.IngreQuantity[i] > 0)
            {
                IngreType++;
            }
        }
        PotionType = 0;
        for (int i =0; i < DataManager.instance.nowData.PotionQuantity.Count; i++)
        {
            if (DataManager.instance.nowData.PotionQuantity[i] > 0)
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
            if (IngreType > 0) //���κ�
            {
                int m = 0;
                content.transform.GetChild(0).gameObject.SetActive(true); //ingre Line Ű��
                for (int i = 0; i < 9; i++)
                {
                    int j = i / 3;
                    if (i < IngreType)
                    {
                        content.transform.GetChild(j + 1).gameObject.SetActive(true);
                        content.transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.SetActive(true);
                        for (int k = 0 + m; k < DataManager.instance.nowData.IngreQuantity.Length; k++)
                        {
                            if (DataManager.instance.nowData.IngreQuantity[k] > 0)
                            {
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetComponent<Image>().sprite = ingreIcons[k]; //��ư �̹��� �ٲٱ�
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnType = Type.Ingredient; //�ش� ��ư�� Ÿ���� ingredient
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnIngre = (Ingredient)k; //��ư�� ��� Ÿ�� ���
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().count = DataManager.instance.nowData.IngreQuantity[k];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().move = moves[k];
                                if (DataManager.instance.nowData.IngreQuantity[k] < 10)
                                {
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = DataManager.instance.nowData.IngreQuantity[k].ToString(); //�ش� ����� ����
                                }//���ڸ�
                                else
                                {
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = DataManager.instance.nowData.IngreQuantity[k].ToString(); //�ش� ����� ����
                                }//���ڸ�

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
                    if (a == 0) //�ڽĵ��� �� ����������
                    {
                        content.transform.GetChild(i + 1).gameObject.SetActive(false);
                    }
                    else
                    {
                        content.transform.GetChild(i + 1).gameObject.SetActive(true);
                    }
                } //slotLine Ű�� ����
            }
            else
            {
                content.transform.GetChild(0).gameObject.SetActive(false); //ingre Line ����
                content.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                content.transform.GetChild(1).gameObject.SetActive(false);
            }
            if (PotionType > 0) //���� �κ�
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
                        for (int k = 0 + m; k < DataManager.instance.nowData.PotionQuantity.Count; k++)
                        {
                            if (DataManager.instance.nowData.PotionQuantity[k] > 0)
                            {                                                                  //potion    //bottle //color shadow scratch cork outline
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = potionBottleColor[(int)DataManager.instance.nowData.PotionBottle[k]];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = GameManager.instance.PotionColors[(int)(DataManager.instance.nowData.PotionEffect[k])];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = potionBottleShadow[(int)DataManager.instance.nowData.PotionBottle[k]];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().sprite = potionBottleScratch[(int)DataManager.instance.nowData.PotionBottle[k]];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(3).GetComponent<Image>().sprite = potionBottleCork[(int)DataManager.instance.nowData.PotionBottle[k]];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(4).GetComponent<Image>().sprite = potionBottleOutline[(int)DataManager.instance.nowData.PotionBottle[k]];
                                //sticker //color outline icon
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = potionStickerColors[(int)DataManager.instance.nowData.PotionSticker[k]];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>().sprite = potionStickerOutline[(int)DataManager.instance.nowData.PotionSticker[k]];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>().sprite = potionStickerIcon[(int)DataManager.instance.nowData.PotionIcon[k]];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnType = Type.Potion; //�ش� ��ư�� Ÿ���� ingredient
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnPotion = k; //�ش� ��ư�� Ÿ���� ingredient
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetComponent<IngredientPotion>().count = DataManager.instance.nowData.PotionQuantity[k];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetComponent<IngredientPotion>().bottle = DataManager.instance.nowData.PotionBottle[k];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetComponent<IngredientPotion>().sticker = DataManager.instance.nowData.PotionSticker[k];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetComponent<IngredientPotion>().effect = DataManager.instance.nowData.PotionEffect[k];
                                content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetComponent<IngredientPotion>().icon = DataManager.instance.nowData.PotionIcon[k];
                                if (DataManager.instance.nowData.PotionQuantity[k] < 10)
                                {
                                    content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = DataManager.instance.nowData.PotionQuantity[k].ToString(); //�ش� ����� ����
                                }//���ڸ�
                                else
                                {
                                    content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 5).transform.GetChild(i % 3).GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = DataManager.instance.nowData.PotionQuantity[k].ToString(); //�ش� ����� ����
                                }//���ڸ�

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
                    if (a == 0) //�ڽĵ��� �� ����������
                    {
                        content.transform.GetChild(i + 5).gameObject.SetActive(false);
                    }
                    else
                    {
                        content.transform.GetChild(i + 5).gameObject.SetActive(true);
                    }
                } //slotLine Ȯ���ؼ� Ű�� ����
            }
            else
            {
                content.transform.GetChild(4).gameObject.SetActive(false); //potion Line ����
                content.transform.GetChild(5).GetChild(0).gameObject.SetActive(false);
                content.transform.GetChild(5).gameObject.SetActive(false);
                content.transform.GetChild(6).gameObject.SetActive(false);
                content.transform.GetChild(7).gameObject.SetActive(false);
            }
        }
        else if (currentTab == Tab.Ingre)
        {
            CheckType();
            if (IngreType > 0) //���κ�
            {
                int m = 0;
                content.transform.GetChild(0).gameObject.SetActive(true); //ingre Line Ű��
                for (int i = 0; i < 9; i++)
                {
                    int j = i / 3;
                    if (i < IngreType)
                    {
                        content.transform.GetChild(j + 1).gameObject.SetActive(true);
                        content.transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.SetActive(true);
                        for (int k = 0 + m; k < DataManager.instance.nowData.IngreQuantity.Length; k++)
                        {
                            if (DataManager.instance.nowData.IngreQuantity[k] > 0)
                            {
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetComponent<Image>().sprite = ingreIcons[k]; //��ư �̹��� �ٲٱ�
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnType = Type.Ingredient; //�ش� ��ư�� Ÿ���� ingredient
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnIngre = (Ingredient)k; //��ư�� ��� Ÿ�� ���
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().count = DataManager.instance.nowData.IngreQuantity[k];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().move = moves[k];
                                if (DataManager.instance.nowData.IngreQuantity[k] < 10)
                                {
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = DataManager.instance.nowData.IngreQuantity[k].ToString(); //�ش� ����� ����
                                }//���ڸ�
                                else
                                {
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = DataManager.instance.nowData.IngreQuantity[k].ToString(); //�ش� ����� ����
                                }//���ڸ�

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
                    if (a == 0) //�ڽĵ��� �� ����������
                    {
                        content.transform.GetChild(i + 1).gameObject.SetActive(false);
                    }
                    else
                    {
                        content.transform.GetChild(i + 1).gameObject.SetActive(true);
                    }
                } //slotLine Ű�� ����

            }
            else
            {
                content.transform.GetChild(0).gameObject.SetActive(false); //ingre Line ����
                content.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                content.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        else if (currentTab == Tab.Potion)
        {
            CheckType();
            if (PotionType > 0) //���� �κ�
            {
                int m = 0;
                content.transform.GetChild(0).gameObject.SetActive(true); //potion Line 
                for (int i = 0; i < 9; i++)
                {
                    int j = i / 3;
                    if (i < PotionType)
                    {
                        content.transform.GetChild(j + 1).gameObject.SetActive(true);
                        content.transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.SetActive(true);
                        for (int k = 0 + m; k < DataManager.instance.nowData.PotionQuantity.Count; k++)
                        {
                            if (DataManager.instance.nowData.PotionQuantity[k] > 0)
                            {                                                                  //potion    //bottle //color shadow scratch cork outline
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = potionBottleColor[(int)DataManager.instance.nowData.PotionBottle[k]];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = GameManager.instance.PotionColors[(int)(DataManager.instance.nowData.PotionEffect[k])];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = potionBottleShadow[(int)DataManager.instance.nowData.PotionBottle[k]];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().sprite = potionBottleScratch[(int)DataManager.instance.nowData.PotionBottle[k]];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(3).GetComponent<Image>().sprite = potionBottleCork[(int)DataManager.instance.nowData.PotionBottle[k]];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetChild(0).GetChild(4).GetComponent<Image>().sprite = potionBottleOutline[(int)DataManager.instance.nowData.PotionBottle[k]];
                                //sticker //color outline icon
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = potionStickerColors[(int)DataManager.instance.nowData.PotionSticker[k]];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>().sprite = potionStickerOutline[(int)DataManager.instance.nowData.PotionSticker[k]];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>().sprite = potionStickerIcon[(int)DataManager.instance.nowData.PotionIcon[k]];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnType = Type.Potion; //�ش� ��ư�� Ÿ���� ingredient
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().btnPotion = k; //�ش� ��ư�� Ÿ���� ingredient
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().count = DataManager.instance.nowData.PotionQuantity[k];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().bottle = DataManager.instance.nowData.PotionBottle[k];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().sticker = DataManager.instance.nowData.PotionSticker[k];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().effect = DataManager.instance.nowData.PotionEffect[k];
                                content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetComponent<IngredientPotion>().icon = DataManager.instance.nowData.PotionIcon[k];
                                if (DataManager.instance.nowData.PotionQuantity[k] < 10)
                                {
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = DataManager.instance.nowData.PotionQuantity[k].ToString(); //�ش� ����� ����
                                }//���ڸ�
                                else
                                {
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(0).gameObject.SetActive(false);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).gameObject.SetActive(true);
                                    content.transform.GetChild(j + 1).transform.GetChild(i % 3).GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = DataManager.instance.nowData.PotionQuantity[k].ToString(); //�ش� ����� ����
                                }//���ڸ�

                                m = k + 1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (content.transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.activeSelf)
                        {
                            content.transform.GetChild(j + 1).transform.GetChild(i % 3).gameObject.SetActive(false);
                        }
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    int a = 0;
                    for (int j = 0; j < content.transform.GetChild(i + 1).childCount; j++)
                    {
                        if (content.transform.GetChild(i + 1).GetChild(j).gameObject.activeSelf) a++;
                    }
                    if (a == 0) //�ڽĵ��� �� ����������
                    {
                        content.transform.GetChild(i + 1).gameObject.SetActive(false);
                    }
                    else
                    {
                        content.transform.GetChild(i + 1).gameObject.SetActive(true);
                    }
                } //slotLine Ȯ���ؼ� Ű�� ����
            }
            else
            {
                content.transform.GetChild(0).gameObject.SetActive(false); //potion Line ����
                content.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                content.transform.GetChild(1).gameObject.SetActive(false);
                content.transform.GetChild(2).gameObject.SetActive(false);
                content.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        else if (currentTab == Tab.Etc)
        {
            content.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
