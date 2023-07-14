using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientPotion : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InvenItemManager.Type btnType;
    public InvenItemManager.Ingredient btnIngre;
    public int btnPotion;
    public int count;
    GameObject made;
    private Vector3 dragOffset;
    List<Vector3> positions = new List<Vector3>();
    List<Vector3> rotations = new List<Vector3>();
    public MoveDetail move;
    GameObject line;

    [SerializeField] GameObject map;

    private void Awake()
    {
        map = FindObjectOfType<map>().gameObject;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!line && btnType == InvenItemManager.Type.Ingredient)
        {
            line = Instantiate(move.fixLines[0], map.transform.parent.position, move.fixLines[0].transform.rotation, map.transform.GetChild(0));
            for (int z = 0; z < line.transform.childCount; z++)
            {
                line.transform.GetChild(z).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        else if (btnType == InvenItemManager.Type.Potion)
        {
            //정보 띄우기
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (line) Destroy(line);
    }
    public void OnMouseDown() //클릭
    {
        dragOffset = transform.position - GetMousePos();
        if (gameObject.GetComponent<IngredientPotion>().btnType == InvenItemManager.Type.Ingredient) //꺼내는게 재료이면
        {
            Vector3 pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            made = Instantiate(InvenItemManager.instance.Prefabs[(int)GetComponent<IngredientPotion>().btnIngre], pos, Quaternion.identity, InvenItemManager.instance.MakeRoom.transform);
            GameManager.instance.IngreQuantity[(int)GetComponent<IngredientPotion>().btnIngre] -= 1;
            made.GetComponent<SpriteRenderer>().sortingOrder += 1000;
            made.GetComponent<Rigidbody2D>().gravityScale = 0;
            if (made.GetComponent<CircleCollider2D>()) made.GetComponent<CircleCollider2D>().isTrigger = true;

            if (made.transform.childCount > 0)
            {
                for (int i = 0; i < made.transform.childCount; i++)
                {
                    positions.Add(made.transform.GetChild(i).localPosition);
                    rotations.Add(made.transform.GetChild(i).localEulerAngles);
                    if (made.transform.GetChild(i).GetComponent<Rigidbody2D>()) made.transform.GetChild(i).GetComponent<Rigidbody2D>().gravityScale = 0;
                    if (made.transform.GetChild(i).GetComponent<SpriteRenderer>()) made.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 999;
                }
                for (int i = 0; i < made.transform.childCount-1; i++)
                {
                    if (made.transform.GetChild(i).GetComponent<CircleCollider2D>()) made.transform.GetChild(i).GetComponent<CircleCollider2D>().isTrigger = true;
                }
            }
            else
            {
                positions.Clear();
                rotations.Clear();
            }
        }
        else //Potion이면 
        {
            Vector3 pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            made = Instantiate(InvenItemManager.instance.PotionPrefab, pos, Quaternion.identity, InvenItemManager.instance.MakeRoom.transform);

            //만들어진 potion 생긴모습 바꿔주기
            made.GetComponent<Potion>().PotionData = GameManager.instance.PotionDetails[btnPotion];
            made.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = InvenItemManager.instance.potionBottleColor[(int)made.GetComponent<Potion>().PotionData.bottle];
            made.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = GameManager.instance.PotionColors[(int)made.GetComponent<Potion>().PotionData.effect[0]];
            made.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = InvenItemManager.instance.potionBottleShadow[(int)made.GetComponent<Potion>().PotionData.bottle];
            made.transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().sprite = InvenItemManager.instance.potionBottleScratch[(int)made.GetComponent<Potion>().PotionData.bottle];
            made.transform.GetChild(0).GetChild(3).GetComponent<SpriteRenderer>().sprite = InvenItemManager.instance.potionBottleCork[(int)made.GetComponent<Potion>().PotionData.bottle];
            made.transform.GetChild(0).GetChild(4).GetComponent<SpriteRenderer>().sprite = InvenItemManager.instance.potionBottleOutline[(int)made.GetComponent<Potion>().PotionData.bottle];
            made.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = InvenItemManager.instance.potionStickerColors[(int)made.GetComponent<Potion>().PotionData.sticker];
            made.transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = InvenItemManager.instance.potionStickerOutline[(int)made.GetComponent<Potion>().PotionData.sticker];
            made.transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().sprite = InvenItemManager.instance.potionStickerIcon[(int)made.GetComponent<Potion>().PotionData.icon];

            GameManager.instance.PotionQuantity[btnPotion] -= 1;
            if (made.CompareTag("potion"))
            {
                for (int i = 0; i < made.transform.childCount; i++)
                {
                    if (made.transform.GetChild(i).GetComponent<SpriteRenderer>()) made.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder += 1000;
                    for (int j = 0; j < made.transform.GetChild(i).childCount; j++)
                    {
                        if (made.transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>()) made.transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().sortingOrder += 1000;
                    }
                }
            }
            made.GetComponent<Rigidbody2D>().gravityScale = 0;
            if (made.transform.GetChild(0).GetChild(0).GetComponent<PolygonCollider2D>()) made.transform.GetChild(0).GetChild(0).GetComponent<PolygonCollider2D>().isTrigger = true;

        }
        InvenItemManager.instance.UpdateInventory();
    }
    public void OnMouseUp()
    {
        FindObjectOfType<DragTest>().isDrag = false;
        if (line) Destroy(line);
        if (!FindObjectOfType<DragTest>().isInven)
        {
            if (made.GetComponent<CircleCollider2D>()) made.GetComponent<CircleCollider2D>().isTrigger = false;
            
            if (made.CompareTag("potion"))
            {
                for (int i = 0; i < made.transform.childCount; i++)
                {
                    if (made.transform.GetChild(i).GetComponent<SpriteRenderer>()) made.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder -= 1000;
                    for (int j = 0; j < made.transform.GetChild(i).childCount; j++)
                    {
                        if (made.transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>()) made.transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().sortingOrder -= 1000;
                    }
                }
            }
            else
            {
                made.GetComponent<SpriteRenderer>().sortingOrder -= 1000;
            }
            made.GetComponent<Rigidbody2D>().gravityScale = 1;
            if (made.transform.childCount > 0)
            {
                for (int i = 0; i < made.transform.childCount; i++)
                {
                    if (made.transform.GetChild(i).GetComponent<Rigidbody2D>()) made.transform.GetChild(i).GetComponent<Rigidbody2D>().gravityScale = 1;
                    if (made.transform.GetChild(i).GetComponent<SpriteRenderer>()) made.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 9;
                }
                for (int i = 0; i < made.transform.childCount - 1; i++)
                {
                    if (made.transform.GetChild(i).GetComponent<CircleCollider2D>()) made.transform.GetChild(i).GetComponent<CircleCollider2D>().isTrigger = false;
                }
            }
            else
            {
                positions.Clear();
                rotations.Clear();
            }
        }
        else //마우스 인벤토리 위에서 놓으면 다시 들어가게
        {
            //InvenItemManager.instance.IngreQuantity[made.transform.GetChild(made.transform.childCount-1).GetComponent<ChildData>().ingreType]++;
            //InvenItemManager.instance.UpdateInventory();
            //Destroy(made);
            made = null;
        }
    }
    public void OnMouseDrag() //드래그중
    {
        if (made.CompareTag("ingredient")) //재료 드래그중
        {
            if (!line)
            {
                line = Instantiate(move.fixLines[0], map.transform.parent.position, move.fixLines[0].transform.rotation, map.transform.GetChild(0));
                for (int z = 0; z < line.transform.childCount; z++)
                {
                    line.transform.GetChild(z).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                }
            }
            made.transform.position = GetMousePos() + dragOffset;
            made.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            made.GetComponent<Rigidbody2D>().angularVelocity = 0;
            for (int i = 0; i < made.transform.childCount; i++)
            {
                if (made.transform.GetChild(i).GetComponent<Rigidbody2D>())
                {
                    made.transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    made.transform.GetChild(i).GetComponent<Rigidbody2D>().angularVelocity = 0;
                }
            }
        }
        else //포션 드래그중
        {
            made.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            made.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }
    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
    private void OnDisable()
    {
        //인벤토리에서 놔서 꺼질때
        if (made != null && made.CompareTag("ingredient"))
        {
            made.GetComponent<SpriteRenderer>().sortingOrder = 7;
            made.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
            made.transform.localPosition = new Vector3(5.32000017f, 0.0599999987f, 90);
            if (made.transform.childCount > 0)
            {
                for (int i = 0; i < made.transform.childCount; i++)
                {
                    if (made.transform.GetChild(i).GetComponent<Rigidbody2D>())  made.transform.GetChild(i).GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                    if (made.transform.GetChild(i).GetComponent<SpriteRenderer>()) made.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 6;
                }
            }
            else
            {
                positions.Clear();
                rotations.Clear();
            }
        }
        else if (made != null && made.CompareTag("potion"))
        {
            if (made.transform.GetChild(0).GetChild(0).GetComponent<PolygonCollider2D>()) made.transform.GetChild(0).GetChild(0).GetComponent<PolygonCollider2D>().isTrigger = false;
            made.transform.localPosition = new Vector3(5.32000017f, 0.0599999987f, 90);
            made.GetComponent<Rigidbody2D>().gravityScale = 1;
            for (int i = 0; i < made.transform.childCount; i++)
            {
                if (made.transform.GetChild(i).GetComponent<SpriteRenderer>()) made.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder -= 1000;
                for (int j = 0; j < made.transform.GetChild(i).childCount; j++)
                {
                    if (made.transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>()) made.transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().sortingOrder -= 1000;
                }
            }
        }
    }
}
