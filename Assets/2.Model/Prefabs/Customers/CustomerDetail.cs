using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customer Detail", menuName = "Scriptable Object/Customer Detail")]

public class CustomerDetail : ScriptableObject
{
    public int number; //고유의 숫자
    public enum CustomerType { Normal = 0, Shop = 1 } //일반손님, 상점 뭐시기
    public CustomerType type;
    public InvenItemManager.Potion needPotion; //필요한 포션 종류
    public int RandomMent; //멘트 몇가지들중 몇번째인지
}
