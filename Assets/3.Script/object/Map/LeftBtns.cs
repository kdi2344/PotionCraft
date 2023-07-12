using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeftBtns : MonoBehaviour
{
    [SerializeField] private Transform MapBottle;
    [SerializeField] private Transform Map;
    [SerializeField] public PotionDetail[] Potions; //Scriptable Object
    public void BtnRecipeSave() //포션 완료 버튼,, 이름 잘못지음
    {
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
            FindObjectOfType<SpoonHandler>().move = null;
        //}
    }

    public void BtnCancel() //취소 버튼
    {
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
        for (int i = 0; i < GameManager.instance.PotionDetails.Length; i++)
        {
            if (GameManager.instance.PotionDetails[i].ingredients.Count == 0)
            {
                GameManager.instance.PotionDetails[i].PotionName = FindObjectOfType<BellowHandle>().input.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text;
                if (GameManager.instance.PotionDetails[i].PotionName == null) GameManager.instance.PotionDetails[i].PotionName = "새 포션";
                GameManager.instance.PotionDetails[i].effect.Add(GameManager.instance.currentPotionEffect);

                //bottle 설정 (일단은 기본) 추후에 수정
                GameManager.instance.PotionDetails[i].bottle = InvenItemManager.BottleShape.normal;
                GameManager.instance.PotionDetails[i].sticker = InvenItemManager.BottleSticker.normal;
                GameManager.instance.PotionDetails[i].icon = (InvenItemManager.Potion)(int)GameManager.instance.currentPotionEffect;
                //level설정 기본 1렙
                GameManager.instance.PotionDetails[i].level = 1;
                GameManager.instance.PotionQuantity[i]++;
                FindObjectOfType<InvenItemManager>().UpdateInventory();

                for (int j = 0; j < FindObjectOfType<Pot>().containIngredients.Length; j++)
                {
                    if (FindObjectOfType<Pot>().containIngredients[j] > 0)
                    {
                        GameManager.instance.PotionDetails[i].ingredients.Add((InvenItemManager.Ingredient)j);
                    }
                }
                break;
            }
        }
    }
}
