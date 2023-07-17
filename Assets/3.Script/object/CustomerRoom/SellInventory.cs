using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellInventory : MonoBehaviour
{
    public static InvenItemManager instance = null;
    public int IngreType = 0; //몇 종류 가지고 있는지
    [SerializeField] private Sprite[] ingreIcons;
    public int SellTotal = 0;
    [SerializeField] public GameObject BtnBuy;

    private void Awake()
    {
        UpdateInventory();
    }
    public void CheckType()
    {
        IngreType = 0;
        for (int i = 0; i < CustomerManager.instance.SellQuantity.Length; i++)
        {
            if (CustomerManager.instance.SellQuantity[i] > 0)
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
            SellTotal = 0;
            for (int i = 0; i < 8; i++)
            {
                int j = i / 4;
                if (i < IngreType)
                {
                    transform.GetChild(j).gameObject.SetActive(true);
                    transform.GetChild(j).transform.GetChild(i % 4).gameObject.SetActive(true);
                    for (int k = 0 + m; k < CustomerManager.instance.SellQuantity.Length; k++)
                    {
                        if (CustomerManager.instance.SellQuantity[k] > 0)
                        {
                            transform.GetChild(j).GetChild(i % 4).GetChild(1).GetComponent<Image>().sprite = ingreIcons[k]; //버튼 이미지 바꾸기
                            transform.GetChild(j).GetChild(i % 4).GetComponent<BtnUpBuy>().index = k;
                            transform.GetChild(j).GetChild(i % 4).GetChild(2).GetChild(0).gameObject.SetActive(true);
                            transform.GetChild(j).GetChild(i % 4).GetChild(2).GetChild(1).gameObject.SetActive(false);
                            transform.GetChild(j).GetChild(i % 4).GetChild(2).GetChild(0).GetChild(2).GetComponent<Text>().text = CustomerManager.instance.SellQuantity[k].ToString(); //해당 재료의 개수
                            transform.GetChild(j).GetChild(i % 4).GetChild(3).GetChild(4).GetComponent<Text>().text = (CustomerManager.instance.ShopperPrice[k] * CustomerManager.instance.SellQuantity[k]).ToString(); //가격
                            SellTotal += CustomerManager.instance.ShopperPrice[k] * CustomerManager.instance.SellQuantity[k];
                            m = k + 1;
                            break;
                        }
                    }
                }
                else
                {
                    if (transform.GetChild(j).GetChild(i % 4).gameObject.activeSelf) transform.GetChild(j).transform.GetChild(i % 4).gameObject.SetActive(false);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                int a = 0;
                for (int j = 0; j < transform.GetChild(i).childCount; j++)
                {
                    if (transform.GetChild(i).GetChild(j).gameObject.activeSelf) a++;
                }
                if (a == 0) //자식들이 다 꺼져있으면
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            } //slotLine 키고 끄기
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        if (SellTotal < 1 || SellTotal > DataManager.instance.nowData.Coin) { BtnBuy.GetComponent<Button>().interactable = false; }
        else { BtnBuy.GetComponent<Button>().interactable = true; }
        BtnBuy.transform.GetChild(0).GetComponent<Text>().text = string.Format("거래가 성립되었습니다! 골드(        {0:D3}개)를 내게 됩니다", SellTotal);
        
    }
}
