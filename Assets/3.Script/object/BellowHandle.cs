using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BellowHandle : MonoBehaviour
{
    [SerializeField] bool isDrag = false;
    [SerializeField] Vector3 mousePos;
    [SerializeField] private float angleOffset;
    private PolygonCollider2D col;
    private Vector3 screenPos;

    [Header("�ϼ��Ҷ� �ʿ��� ��")]
    [SerializeField] private int pump = 0;
    private float time;
    private Coroutine currentCo;
    [SerializeField] private GameObject FinSmoke;
    [SerializeField] private GameObject MapBottleLiquid;
    [SerializeField] private GameObject PotionStickerIcon;
    [SerializeField] private GameObject PotionColor;
    [SerializeField] private Sprite[] PotionStickerIcons; //? ��Ʈ �� etc
    [SerializeField] public TMP_InputField input;
    [SerializeField] private GameObject lines;
    private void Awake()
    {
        col = GetComponent<PolygonCollider2D>();
    }

    void OnMouseDown()
    {
        isDrag = true;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (col == Physics2D.OverlapPoint(mousePos))
        {
            screenPos = Camera.main.WorldToScreenPoint(transform.parent.parent.position);
            Vector3 vec3 = Input.mousePosition - screenPos;
            angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
        }
    }

    private void OnMouseUp()
    {
        isDrag = false;
        transform.parent.parent.eulerAngles = new Vector3(0, 0, -28.8f);
    }
    private void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (col == Physics2D.OverlapPoint(mousePos))
        {
            Vector3 vec3 = Input.mousePosition - screenPos;
            float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
            float z = angle + angleOffset;
            if (z < -28.8)
            {
                z = -28.8f;
            }
            else if (z > -10)
            {
                z = -10;
            }
            transform.parent.parent.eulerAngles = new Vector3(0, 0, z);
        }
        if (currentCo == null && GameManager.instance.CanMakePotion)
        {
            currentCo = StartCoroutine(CheckPump());
        }
    }
    IEnumerator CheckPump()
    {
        time = 0;
        pump = 0;
        while (time < 2)
        {
            if (pump > 1)
            {
                for (int i = 0; i < FinSmoke.transform.parent.GetChild(FinSmoke.transform.parent.childCount - 1).childCount; i++)
                {
                    FinSmoke.transform.parent.GetChild(FinSmoke.transform.parent.childCount - 1).GetChild(i).GetComponent<Animator>().SetTrigger("boil");
                }
                
            }
            time += Time.deltaTime;
            Boom();
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return 0;
        currentCo = null;
    }

    private void Boom()
    {
        if (pump >=3)
        {
            FinSmoke.GetComponent<Animator>().SetTrigger("fin");
            pump = 0;
            StopAllCoroutines();
            currentCo = null;
            GameManager.instance.CanMakePotion = false;
            ChangeColor();
        }
    }
    private void ChangeColor()
    {
        GameManager.instance.CanSavePotion = true;
        if (GameManager.instance.currentPotionEffect == InvenItemManager.Potion.Heal)
        {
            MapBottleLiquid.GetComponent<SpriteRenderer>().color = GameManager.instance.PotionColors[1];

            input.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "�̾��� ġ���� ����"; // level�� ���� �ٸ��� �̸� 1: �̾��� ġ���� ����
            //�� 5���߿� �ϳ� ������ �ٲ��ֱ�
            PotionStickerIcon.GetComponent<SpriteRenderer>().sprite = PotionStickerIcons[1]; //���� ��ƼĿ ��Ʈ�� �ٲ��ֱ�
            PotionColor.GetComponent<SpriteRenderer>().color = GameManager.instance.PotionColors[1]; ; //���� �� ������ �ٲٱ�
        }
        else if (GameManager.instance.currentPotionEffect == InvenItemManager.Potion.Poison)
        {
            MapBottleLiquid.GetComponent<SpriteRenderer>().color = GameManager.instance.PotionColors[2];

            input.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "�̾��� �͵��� ����"; // level�� ���� �ٸ��� �̸� 1: �̾��� 
            //�� 5���߿� �ϳ� ������ �ٲ��ֱ�
            PotionStickerIcon.GetComponent<SpriteRenderer>().sprite = PotionStickerIcons[2]; //���� ��ƼĿ ��Ʈ�� �ٲ��ֱ�
            PotionColor.GetComponent<SpriteRenderer>().color = GameManager.instance.PotionColors[2]; ; //���� �� ������ �ٲٱ�
        }
        else if (GameManager.instance.currentPotionEffect == InvenItemManager.Potion.Sleep)
        {
            MapBottleLiquid.GetComponent<SpriteRenderer>().color = GameManager.instance.PotionColors[3];

            input.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "�̾��� ������ ����"; 
            //�� 5���߿� �ϳ� ������ �ٲ��ֱ�
            PotionStickerIcon.GetComponent<SpriteRenderer>().sprite = PotionStickerIcons[3]; //���� ��ƼĿ ��Ʈ�� �ٲ��ֱ�
            PotionColor.GetComponent<SpriteRenderer>().color = GameManager.instance.PotionColors[3]; ; //���� �� ������ �ٲٱ�
        }
        else if (GameManager.instance.currentPotionEffect == InvenItemManager.Potion.Fire)
        {
            MapBottleLiquid.GetComponent<SpriteRenderer>().color = GameManager.instance.PotionColors[4];

            input.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "�̾��� ȭ���� ����"; 
            //�� 5���߿� �ϳ� ������ �ٲ��ֱ�
            PotionStickerIcon.GetComponent<SpriteRenderer>().sprite = PotionStickerIcons[4]; //���� ��ƼĿ ��Ʈ�� �ٲ��ֱ�
            PotionColor.GetComponent<SpriteRenderer>().color = GameManager.instance.PotionColors[4]; ; //���� �� ������ �ٲٱ�
        }
        else if (GameManager.instance.currentPotionEffect == InvenItemManager.Potion.Tree)
        {
            MapBottleLiquid.GetComponent<SpriteRenderer>().color = GameManager.instance.PotionColors[5];

            input.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "�̾��� ������ ����"; 
            //�� 5���߿� �ϳ� ������ �ٲ��ֱ�
            PotionStickerIcon.GetComponent<SpriteRenderer>().sprite = PotionStickerIcons[5]; //���� ��ƼĿ ��Ʈ�� �ٲ��ֱ�
            PotionColor.GetComponent<SpriteRenderer>().color = GameManager.instance.PotionColors[5]; ; //���� �� ������ �ٲٱ�
        }
    }

    public void ResetPotion()
    {
        MapBottleLiquid.GetComponent<SpriteRenderer>().color = GameManager.instance.PotionColors[0];
        PotionStickerIcon.GetComponent<SpriteRenderer>().sprite = PotionStickerIcons[0]; //���� ��ƼĿ ����ǥ�� �ٲ��ֱ�
        PotionColor.GetComponent<SpriteRenderer>().color = GameManager.instance.PotionColors[0]; //���� �� �Ķ��� �ٲٱ�
        input.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "�� ����";
        GameManager.instance.currentPotionEffect = InvenItemManager.Potion.None;
        for (int i = 0; i < lines.transform.childCount; i++)
        {
            Destroy(lines.transform.GetChild(i).gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("circle"))
        {
            pump++;
        }
    }


    //1 ~ -28.8
}
