using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeftBtns : MonoBehaviour
{
    [SerializeField] private Transform MapBottle;
    [SerializeField] private Transform Map;
    [SerializeField] public PotionDetail[] Potions; //Scriptable Object

    [SerializeField] GameObject SelectBottleUI;
    [SerializeField] GameObject SelectStickerUI;
    [SerializeField] GameObject SelectIconUI;

    public void BtnSelectBottle()
    {
        if (SelectBottleUI.activeSelf) SelectBottleUI.SetActive(false);
        else SelectBottleUI.SetActive(true);
    }

    public void BtnSelectSticker()
    {
        if (SelectStickerUI.activeSelf) SelectStickerUI.SetActive(false);
        else SelectStickerUI.SetActive(true);
    }

    public void BtnSelectIcon()
    {
        if (SelectIconUI.activeSelf) SelectIconUI.SetActive(false);
        else SelectIconUI.SetActive(true);
    }
    public void BtnRecipeSave() //포션 완료 버튼,, 이름 잘못지음
    {
        SoundManager.instance.PlayEffect("btn");
        SoundManager.instance.PlayEffect("boil");
        //if (GameManager.instance.CanSavePotion)
        //{
        //책에 저장하기
        //포션 저장에 필요한 내용: 포션 이름, 포션 병 종류 (하나), 포션 스티커 모양 (하나), 포션 효과들 (힐 / 독 etc 5개 까지), 포션 스티커 아이콘, 이용한 재료랑 개수, 맵에서 포션 자리


        //인벤토리에 추가해서 표시하기
        AddPotionInven();

        //reset -> 왼쪽에 재료창 비우기, 포션 위치 제자리로, 포션 색깔 원래대로, 왼쪽 포션 색, 아이콘 원래대로 돌려놓기
        GameManager.instance.CanMakePotion = false;
        GameManager.instance.CanSavePotion = false;
        FindObjectOfType<BellowHandle>().input.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = "";
        FindObjectOfType<PotionIngredientsShow>().ResetIngredient(); //왼쪽 재료창 비움
        MapBottle.localPosition = Vector3.zero; //맵 원위치
        Map.localPosition = new Vector3(1.56666708f, -3.58333302f, 0); //포션 제자리로
        FindObjectOfType<BellowHandle>().ResetPotion();
        GameManager.instance.CanSavePotion = false;
        GameManager.instance.currentBottleShape = InvenItemManager.BottleShape.normal;
        GameManager.instance.currentPotionEffect = InvenItemManager.Potion.None;
        GameManager.instance.currentPotionIcon = InvenItemManager.Potion.None;
        GameManager.instance.currentSticker = InvenItemManager.BottleSticker.normal;
        FindObjectOfType<LeftPotion>().UpdatePotion();
        //초기화
            FindObjectOfType<SpoonHandler>().move = null;
        //}
    }

    public void BtnCancel() //취소 버튼
    {
        SoundManager.instance.PlayEffect("btn");
        GameManager.instance.CanMakePotion = false;
        GameManager.instance.CanSavePotion = false;
        FindObjectOfType<PotionIngredientsShow>().ResetIngredient(); //왼쪽 재료창 비움
        MapBottle.localPosition = Vector3.zero; //맵 원위치
        Map.localPosition = new Vector3(1.56666708f, -3.58333302f, 0); //포션 제자리로
        FindObjectOfType<BellowHandle>().ResetPotion();
        FindObjectOfType<SpoonHandler>().ResetTarget(null);
    }

    private void AddPotionInven() //인벤토리에 포션 추가 
    {
        for (int i = 0; i < DataManager.instance.nowData.PotionQuantity.Count; i++)
        {
            // 병, 스티커, 아이콘, effect 다 똑같은게 만들어지면 원래거에 개수 증가
            if (GameManager.instance.currentBottleShape == DataManager.instance.nowData.PotionBottle[i] && GameManager.instance.currentSticker == DataManager.instance.nowData.PotionSticker[i] && GameManager.instance.currentPotionIcon == DataManager.instance.nowData.PotionIcon[i] && GameManager.instance.currentPotionEffect == DataManager.instance.nowData.PotionEffect[i])
            {
                DataManager.instance.nowData.PotionQuantity[i]++;
                FindObjectOfType<InvenItemManager>().UpdateInventory();
                return;
            }
        }
        DataManager.instance.nowData.PotionQuantity.Add(1); //한개 추가
        DataManager.instance.nowData.PotionEffect.Add(GameManager.instance.currentPotionEffect);
        DataManager.instance.nowData.PotionBottle.Add(GameManager.instance.currentBottleShape);
        DataManager.instance.nowData.PotionSticker.Add(GameManager.instance.currentSticker);
        DataManager.instance.nowData.PotionIcon.Add(GameManager.instance.currentPotionIcon);
        FindObjectOfType<InvenItemManager>().UpdateInventory();
        //else //기존에 존재하지 않는 포션이면
        //{

        //}


        //if (DataManager.instance.nowData.PotionDetails[i].ingredients.Count == 0)
        //{
        //    DataManager.instance.nowData.PotionDetails[i].PotionName = FindObjectOfType<BellowHandle>().input.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text;
        //    if (DataManager.instance.nowData.PotionDetails[i].PotionName == null) DataManager.instance.nowData.PotionDetails[i].PotionName = "새 포션";
        //    DataManager.instance.nowData.PotionDetails[i].effect = GameManager.instance.currentPotionEffect;

        //    //bottle 설정 (일단은 기본) 추후에 수정
        //    DataManager.instance.nowData.PotionDetails[i].bottle = InvenItemManager.BottleShape.normal;
        //    DataManager.instance.nowData.PotionDetails[i].sticker = InvenItemManager.BottleSticker.normal;
        //    DataManager.instance.nowData.PotionDetails[i].icon = (InvenItemManager.Potion)(int)GameManager.instance.currentPotionEffect;
        //    //level설정 기본 1렙
        //    DataManager.instance.nowData.PotionDetails[i].level = 1;
        //    DataManager.instance.nowData.PotionQuantity[i]++;
        //    FindObjectOfType<InvenItemManager>().UpdateInventory();

        //    for (int j = 0; j < FindObjectOfType<Pot>().containIngredients.Length; j++)
        //    {
        //        if (FindObjectOfType<Pot>().containIngredients[j] > 0)
        //        {
        //            DataManager.instance.nowData.PotionDetails[i].ingredients.Add((InvenItemManager.Ingredient)j);
        //        }
        //    }
        //    break;
        //}
        //}
    }
}
