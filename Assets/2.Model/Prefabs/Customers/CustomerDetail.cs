using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customer Detail", menuName = "Scriptable Object/Customer Detail")]

public class CustomerDetail : ScriptableObject
{
    public int number; //������ ����
    public enum CustomerType { Normal = 0, Shop = 1 } //�Ϲݼմ�, ���� ���ñ�
    public CustomerType type;
    public InvenItemManager.Potion needPotion; //�ʿ��� ���� ����
    public int RandomMent; //��Ʈ ������� ���°����
}
