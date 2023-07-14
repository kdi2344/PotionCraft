using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager instance = null;
    public int CurrentCustomer = 0;
    public int order = 0;
    public bool isMent = false; //쓸모없는 멘트 한 경우
    [SerializeField] private GameObject SpeechBubble;
    [SerializeField] private GameObject ShopUIs;
    public bool isRightPotion = false;
    [SerializeField] GameObject ment;
    [SerializeField] GameObject coin;
    [SerializeField] Transform CustomerCanvas;
    [SerializeField] GameObject UIPrefabs;

    public string[] healMents;
    public string[] poisonMents;
    public string[] sleepMents;
    public string[] fireMents;
    public string[] treeMents;

    public PotionDetail currentPotion;

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
                    if (!isMent)
                    {
                        ShopUIs.transform.GetChild(0).gameObject.SetActive(true);
                        ShopUIs.transform.GetChild(1).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(2).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(3).gameObject.SetActive(false);
                    }
                    else
                    {
                        ShopUIs.transform.GetChild(0).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(1).gameObject.SetActive(true);
                        ShopUIs.transform.GetChild(2).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(3).gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (!isMent)
                    {
                        ShopUIs.transform.GetChild(0).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(1).gameObject.SetActive(false);
                        ShopUIs.transform.GetChild(2).gameObject.SetActive(true);
                        ShopUIs.transform.GetChild(3).gameObject.SetActive(false);
                    }
                    else
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

    public void CheckPotion()
    {
        if (currentPotion == null)
        {
            coin.SetActive(false);
            isRightPotion = false;
        }
        else
        {
            if (currentPotion.effect[0].ToString() == GameManager.instance.customerDetails[CurrentCustomer].needPotion.ToString())
            {
                isRightPotion = true;
                coin.SetActive(true);
            }
            else
            {
                isRightPotion = false;
                coin.SetActive(false);
            }
        }
    }
    public void BtnSell()
    {

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

    public void NextCustomer()
    {
        //Instantiate(); //그 ui 생성
        GameManager.instance.Success--;//실적 하나 감소
        if (GameManager.instance.customerDetails[CurrentCustomer].RandomMent == 2) //나쁜놈 보내줌
        {
            GameManager.instance.Karma += 1;
        }
        else //그냥 손님 보내버림
        {
            GameManager.instance.Karma -= 2;
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
