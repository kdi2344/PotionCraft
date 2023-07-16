using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager instance = null;
    public int CurrentCustomer = 0;
    public int order = 0;
    public bool isMent = false; //������� ��Ʈ �� ���
    public bool isShopping = false; //��ǰ ���� �ִ� ���
    [SerializeField] private GameObject SpeechBubble;
    [SerializeField] private GameObject ShopUIs;
    public bool isRightPotion = false;
    [SerializeField] GameObject ment;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject coinIcon;
    [SerializeField] GameObject sellerUI;

    [SerializeField] Text CoinText;
    [SerializeField] Text SubText;
    [SerializeField] Transform CustomerCanvas;
    [SerializeField] GameObject[] UIPrefabs;

    private int ShopperNum; //�������� ���� 
    public int[] ShopperType; //0~9�߿� ������ ���� ShopperType[ShopperNum]
    public int[] ShopperQuantity = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] SellQuantity = { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //��ٰ� ������ ��
    public int[] ShopperPrice = { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //�Ѱ��� ����

    public string[] healMents;
    public string[] poisonMents;
    public string[] sleepMents;
    public string[] fireMents;
    public string[] treeMents;

    public int PotionMoney = -1;

    public PotionDetail currentPotion;
    public GameObject currentPotionOb;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        ShoppingList();
    }
    private void Update()
    {
        if (CurrentCustomer > 4) 
        { 
            SpeechBubble.SetActive(false);
            ShopUIs.SetActive(false);
        }
        else if (CurrentCustomer != 2 && (GameManager.instance.MadeCustomers[CurrentCustomer].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("customerFirstWait") || GameManager.instance.MadeCustomers[CurrentCustomer].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("customerSecondWait")))
        {
            ShopUIs.SetActive(false);
            SpeechBubble.SetActive(true);
            if (isRightPotion)
            {
                SpeechBubble.transform.GetChild(1).GetChild(0).GetComponent<Button>().interactable = true;
            }
            else
            {
                CoinText.text = "(�Ǹ�)";
                coinIcon.SetActive(false);
                SpeechBubble.transform.GetChild(1).GetChild(0).GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            SpeechBubble.SetActive(false);
            if (CurrentCustomer == 2 && (GameManager.instance.MadeCustomers[CurrentCustomer].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("customerFirstWait") || GameManager.instance.MadeCustomers[CurrentCustomer].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("customerSecondWait")))
            {
                ShopUIs.SetActive(true);
                if (GameManager.instance.DayCount == 1)
                {
                    if (!isMent && !isShopping)
                    {
                        ShopUIs.transform.GetChild(0).gameObject.SetActive(true);
                        ShopUIs.transform.GetChild(1).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(2).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(3).gameObject.SetActive(false);
                    }
                    else if (isMent && !isShopping)
                    {
                        ShopUIs.transform.GetChild(0).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(1).gameObject.SetActive(true);
                        ShopUIs.transform.GetChild(2).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(3).gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (!isMent && !isShopping)
                    {
                        ShopUIs.transform.GetChild(0).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(1).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(2).gameObject.SetActive(true);
                        ShopUIs.transform.GetChild(3).gameObject.SetActive(false);
                    }
                    else if (isMent && !isShopping)
                    {
                        ShopUIs.transform.GetChild(0).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(1).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(2).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(3).gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                ShopUIs.SetActive(false);
            }
        }
    }
    public void BtnBuy()
    {
        GameManager.instance.Coin -= FindObjectOfType<SellInventory>().SellTotal;
        FindObjectOfType<SellInventory>().BtnBuy.GetComponent<Button>().interactable = false;
        FindObjectOfType<SellInventory>().SellTotal = 0;
        for (int i =0; i < 9; i++)
        {
            GameManager.instance.IngreQuantity[i] += SellQuantity[i];
            SellQuantity[i] = 0;
        }
        FindObjectOfType<InvenItemManager>().UpdateInventory();
        FindObjectOfType<SellInventory>().UpdateInventory();
    }
    private void ShoppingList()
    {
        ShopperNum = Random.Range(3, 7);  //�Ĵ� ���� ����
        ShopperType = new int[ShopperNum];  //�Ĵ� ���� ���ϱ�

        for (int i = 0; i < ShopperNum; i++)
        {
            ShopperType[i] = Random.Range(0, 9); 
            bool isSame = false; 

            for (int j = 0; j < i; j++)
            {
                if (ShopperType[i] == ShopperType[j])
                {
                    isSame = true; 
                    break;
                }
            }
            if (isSame) continue; // �ٽ� Q�̾�
        }

        for (int i =0; i< ShopperNum; i++)
        {
            ShopperQuantity[ShopperType[i]] = Random.Range(3, 8); //3������ 7������ ���� ����
            ShopperPrice[ShopperType[i]] = Random.Range(5, 26); //5~25�� ���̿� ���� ����
        }

    }

    public void CheckPotion()
    {
        if (currentPotion == null)
        {
            coin.SetActive(false);
            isRightPotion = false;
            CoinText.text = "(�Ǹ�)";
            coinIcon.SetActive(false);
            SubText.text = "(������ �����Ϸ��� ���￡ �ø�����)";
        }
        else
        {
            if (currentPotion.effect[0].ToString() == GameManager.instance.customerDetails[CurrentCustomer].needPotion.ToString())
            {
                if (PotionMoney < 0) PotionMoney = Random.Range(10, 20);
                CoinText.text = "(��� " + PotionMoney + "     ���� �Ǹ�)";
                coinIcon.SetActive(true);
                isRightPotion = true;
                coin.SetActive(true);
            }
            else
            {
                CoinText.text = "(�Ǹ�)";
                coinIcon.SetActive(false);
                isRightPotion = false;
                coin.SetActive(false);
                SubText.text = "�� ������ �ʿ����� �ʾƿ�...";
            }
        }
    }
    public void BtnSell()
    {
        GameManager.instance.Coin += PotionMoney; //�����ϰ��ִ� �� ����
        GameManager.instance.Success++;//���� �ϳ� ����
        if (GameManager.instance.customerDetails[CurrentCustomer].RandomMent == 2) //���۳� �޾���
        {
            GameObject madeUI = Instantiate(UIPrefabs[1], CustomerCanvas);
            madeUI.SetActive(true);
            madeUI.transform.GetChild(5).GetComponent<Text>().text = "+" + PotionMoney;
            GameManager.instance.Karma -= 3;
        }
        else //�մ� �޾���
        {
            GameObject madeUI = Instantiate(UIPrefabs[0], CustomerCanvas);
            madeUI.SetActive(true);
            madeUI.transform.GetChild(5).GetComponent<Text>().text = "+" + PotionMoney;
            GameManager.instance.Karma += 1;
        }

        PotionMoney = -1;
        SuccessCustomer();
    }

    public void BtnSeeObs()
    {
        isShopping = true;
        sellerUI.SetActive(true);
        for (int i =0; i < ShopUIs.transform.childCount; i++)
        {
            ShopUIs.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void BtnBack()
    {
        sellerUI.SetActive(false);
        isShopping = false;
    }

    public void BtnShopMent() 
    {
        if (isMent) isMent = false;
        else
        {
            isMent = true;
        }
    }

    private void SetText()
    {
        if (CurrentCustomer < 5 && CurrentCustomer != 2)
        {
            if (GameManager.instance.customerDetails[CurrentCustomer].needPotion == InvenItemManager.Potion.Heal) { ment.GetComponent<Text>().text = healMents[GameManager.instance.customerDetails[CurrentCustomer].RandomMent]; }
            else if (GameManager.instance.customerDetails[CurrentCustomer].needPotion == InvenItemManager.Potion.Poison) { ment.GetComponent<Text>().text = poisonMents[GameManager.instance.customerDetails[CurrentCustomer].RandomMent]; }
            else if (GameManager.instance.customerDetails[CurrentCustomer].needPotion == InvenItemManager.Potion.Sleep) { ment.GetComponent<Text>().text = sleepMents[GameManager.instance.customerDetails[CurrentCustomer].RandomMent]; }
            else if (GameManager.instance.customerDetails[CurrentCustomer].needPotion == InvenItemManager.Potion.Fire) { ment.GetComponent<Text>().text = fireMents[GameManager.instance.customerDetails[CurrentCustomer].RandomMent]; }
            else if (GameManager.instance.customerDetails[CurrentCustomer].needPotion == InvenItemManager.Potion.Tree) { ment.GetComponent<Text>().text = treeMents[GameManager.instance.customerDetails[CurrentCustomer].RandomMent]; }
        }
    }

    public void GoFirstCustomer()
    {
        Invoke("FirstGo", 0.5f);
    }

    private void FirstGo()
    {
        GameManager.instance.MadeCustomers[order].GetComponent<Animator>().SetTrigger("firstIn");
        order++;
        Invoke("SecondGo", 1.3f);
        CurrentCustomer = 0;
        SetText();
    }
    private void SecondGo()
    {
        if (order < 5)
        {
            GameManager.instance.MadeCustomers[order].GetComponent<Animator>().SetTrigger("go");
            order++;
        }
    }
    private void SuccessCustomer()
    {
        Destroy(currentPotionOb);
        coin.SetActive(false);
        if (order < 6)
        {
            order--;
            GameManager.instance.MadeCustomers[order - 1].GetComponent<Animator>().SetTrigger("go");
            CurrentCustomer++;
            SetText();
            Invoke("SecondGo", 1f);
            Invoke("SecondGo", 2f);
        }
        else if (order == 6)
        {
            CurrentCustomer++;
            SetText();
            order--;
            GameManager.instance.MadeCustomers[order - 1].GetComponent<Animator>().SetTrigger("go");
        }
        isRightPotion = false;
    }

    public void NextCustomer()
    {
        if (CurrentCustomer != 2) //������ �ƴ϶��
        {
            GameManager.instance.Success--;//���� �ϳ� ����
            if (GameManager.instance.customerDetails[CurrentCustomer].RandomMent == 2) //���۳� ������
            {
                GameObject madeUI = Instantiate(UIPrefabs[2], CustomerCanvas);
                madeUI.SetActive(true);
                GameManager.instance.Karma += 1;
            }
            else //�׳� �մ� ��������
            {
                GameObject madeUI = Instantiate(UIPrefabs[3], CustomerCanvas);
                madeUI.SetActive(true);
                GameManager.instance.Karma -= 2;
            }
        }
        if (order < 6)
        {
            order--;
            GameManager.instance.MadeCustomers[order - 1].GetComponent<Animator>().SetTrigger("go");
            CurrentCustomer++;
            SetText();
            Invoke("SecondGo", 1f);
            Invoke("SecondGo", 2f);
        }
        else if (order == 6)
        {
            CurrentCustomer++;
            SetText();
            order--;
            GameManager.instance.MadeCustomers[order-1].GetComponent<Animator>().SetTrigger("go");
        }
    }
}
