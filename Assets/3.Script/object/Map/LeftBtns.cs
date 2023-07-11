using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBtns : MonoBehaviour
{
    [SerializeField] private Transform MapBottle;
    [SerializeField] private Transform Map;
    public void BtnRecipeSave()
    {
        //책에 저장하기
        //포션 저장에 필요한 내용: 포션 이름, 포션 병 종류 (하나), 포션 스티커 모양 (하나), 포션 효과들 (힐 / 독 etc 5개 까지), 포션 스티커 아이콘, 이용한 재료랑 개수, 맵에서 포션 자리
        //reset -> 왼쪽에 재료창 비우기, 포션 위치 제자리로, 포션 색깔 원래대로, 왼쪽 포션 색, 아이콘 원래대로 돌려놓기
        GameManager.instance.CanMakePotion = false;
        GameManager.instance.CanSavePotion = false;
        FindObjectOfType<PotionIngredientsShow>().ResetIngredient(); //왼쪽 재료창 비움
        MapBottle.localPosition = Vector3.zero; //맵 원위치
        Map.localPosition = new Vector3(1.56666708f, -3.58333302f, 0); //포션 제자리로
        FindObjectOfType<BellowHandle>().ResetPotion();
    }
}
