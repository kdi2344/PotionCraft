using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeftBtns : MonoBehaviour
{
    [SerializeField] private Transform MapBottle;
    [SerializeField] private Transform Map;
    [SerializeField] public PotionDetail[] Potions; //Scriptable Object
    public void BtnRecipeSave() //���� �Ϸ� ��ư,, �̸� �߸�����
    {
        //if (GameManager.instance.CanSavePotion)
        //{
            //å�� �����ϱ�
            //���� ���忡 �ʿ��� ����: ���� �̸�, ���� �� ���� (�ϳ�), ���� ��ƼĿ ��� (�ϳ�), ���� ȿ���� (�� / �� etc 5�� ����), ���� ��ƼĿ ������, �̿��� ���� ����, �ʿ��� ���� �ڸ�


            //�κ��丮�� �߰��ؼ� ǥ���ϱ�
            AddPotionInven();

            //reset -> ���ʿ� ���â ����, ���� ��ġ ���ڸ���, ���� ���� �������, ���� ���� ��, ������ ������� ��������
            GameManager.instance.CanMakePotion = false;
            GameManager.instance.CanSavePotion = false;
        FindObjectOfType<BellowHandle>().input.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = "";
            FindObjectOfType<PotionIngredientsShow>().ResetIngredient(); //���� ���â ���
            MapBottle.localPosition = Vector3.zero; //�� ����ġ
            Map.localPosition = new Vector3(1.56666708f, -3.58333302f, 0); //���� ���ڸ���
            FindObjectOfType<BellowHandle>().ResetPotion();
            GameManager.instance.CanSavePotion = false;
            FindObjectOfType<SpoonHandler>().move = null;
        //}
    }

    public void BtnCancel() //��� ��ư
    {
        GameManager.instance.CanMakePotion = false;
        GameManager.instance.CanSavePotion = false;
        FindObjectOfType<PotionIngredientsShow>().ResetIngredient(); //���� ���â ���
        MapBottle.localPosition = Vector3.zero; //�� ����ġ
        Map.localPosition = new Vector3(1.56666708f, -3.58333302f, 0); //���� ���ڸ���
        FindObjectOfType<BellowHandle>().ResetPotion();
        FindObjectOfType<SpoonHandler>().ResetTarget(null);
    }

    private void AddPotionInven() //�κ��丮�� ���� �߰� 
    {
        for (int i = 0; i < GameManager.instance.PotionDetails.Length; i++)
        {
            if (GameManager.instance.PotionDetails[i].ingredients.Count == 0)
            {
                GameManager.instance.PotionDetails[i].PotionName = FindObjectOfType<BellowHandle>().input.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text;
                if (GameManager.instance.PotionDetails[i].PotionName == null) GameManager.instance.PotionDetails[i].PotionName = "�� ����";
                GameManager.instance.PotionDetails[i].effect.Add(GameManager.instance.currentPotionEffect);

                //bottle ���� (�ϴ��� �⺻) ���Ŀ� ����
                GameManager.instance.PotionDetails[i].bottle = InvenItemManager.BottleShape.normal;
                GameManager.instance.PotionDetails[i].sticker = InvenItemManager.BottleSticker.normal;
                GameManager.instance.PotionDetails[i].icon = (InvenItemManager.Potion)(int)GameManager.instance.currentPotionEffect;
                //level���� �⺻ 1��
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
