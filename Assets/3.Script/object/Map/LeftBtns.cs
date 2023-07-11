using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBtns : MonoBehaviour
{
    [SerializeField] private Transform MapBottle;
    [SerializeField] private Transform Map;
    public void BtnRecipeSave()
    {
        //å�� �����ϱ�
        //���� ���忡 �ʿ��� ����: ���� �̸�, ���� �� ���� (�ϳ�), ���� ��ƼĿ ��� (�ϳ�), ���� ȿ���� (�� / �� etc 5�� ����), ���� ��ƼĿ ������, �̿��� ���� ����, �ʿ��� ���� �ڸ�
        //reset -> ���ʿ� ���â ����, ���� ��ġ ���ڸ���, ���� ���� �������, ���� ���� ��, ������ ������� ��������
        GameManager.instance.CanMakePotion = false;
        GameManager.instance.CanSavePotion = false;
        FindObjectOfType<PotionIngredientsShow>().ResetIngredient(); //���� ���â ���
        MapBottle.localPosition = Vector3.zero; //�� ����ġ
        Map.localPosition = new Vector3(1.56666708f, -3.58333302f, 0); //���� ���ڸ���
        FindObjectOfType<BellowHandle>().ResetPotion();
    }
}
