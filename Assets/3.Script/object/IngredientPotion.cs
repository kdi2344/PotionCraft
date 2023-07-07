using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPotion : MonoBehaviour
{
    public InvenItemManager.Type btnType;
    public InvenItemManager.Ingredient btnIngre;
    public int count;
    GameObject made;
    private Vector3 dragOffset;
    List<Vector3> positions = new List<Vector3>();
    List<Vector3> rotations = new List<Vector3>();
    public void OnMouseDown() //클릭
    {
        dragOffset = transform.position - GetMousePos();
        if (gameObject.GetComponent<IngredientPotion>().btnType == InvenItemManager.Type.Ingredient) //꺼내는게 재료이면
        {
            Vector3 pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            made = Instantiate(InvenItemManager.instance.Prefabs[(int)GetComponent<IngredientPotion>().btnIngre], pos, Quaternion.identity, InvenItemManager.instance.MakeRoom.transform);
            InvenItemManager.instance.IngreQuantity[(int)GetComponent<IngredientPotion>().btnIngre] -= 1;
            made.GetComponent<SpriteRenderer>().sortingOrder = 1000;
            made.GetComponent<Rigidbody2D>().gravityScale = 0;
            if (made.transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    positions.Add(transform.GetChild(i).localPosition);
                    rotations.Add(transform.GetChild(i).localEulerAngles);
                    made.transform.GetChild(i).GetComponent<Rigidbody2D>().gravityScale = 0;
                    made.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 999;
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

        }
        InvenItemManager.instance.UpdateInventory();
    }
    public void OnMouseUp()
    {
        made.GetComponent<IngreDrag>().isDrag = false;
        if (!made.GetComponent<IngreDrag>().isInven)
        {
            made.GetComponent<SpriteRenderer>().sortingOrder = 7;
            made.GetComponent<Rigidbody2D>().gravityScale = 1;
            if (made.transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    made.transform.GetChild(i).GetComponent<Rigidbody2D>().gravityScale = 1;
                    made.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 6;
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
            InvenItemManager.instance.IngreQuantity[made.GetComponent<IngreDrag>().ingreType]++;
            InvenItemManager.instance.UpdateInventory();
            Destroy(made);
            made = null;
        }
    }
    public void OnMouseDrag() //드래그중
    {
        made.transform.position = GetMousePos() + dragOffset;
        made.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        made.GetComponent<Rigidbody2D>().angularVelocity = 0;
        for (int i = 0; i < made.transform.childCount; i++)
        {
            made.transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            made.transform.GetChild(i).GetComponent<Rigidbody2D>().angularVelocity = 0;
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
        if (made != null)
        {
            made.GetComponent<SpriteRenderer>().sortingOrder = 7;
            made.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
            made.transform.localPosition = new Vector3(5.32000017f, 0.0599999987f, 90);
            if (made.transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    made.transform.GetChild(i).GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                    made.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 6;
                }
            }
            else
            {
                positions.Clear();
                rotations.Clear();
            }
        }
    }
}
