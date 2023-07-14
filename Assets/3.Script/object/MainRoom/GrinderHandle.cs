using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderHandle : MonoBehaviour
{
    bool isDrag = false;
    private Vector3 dragOffset;
    [SerializeField] GameObject pile;
    [SerializeField] GameObject Grinder;

    [SerializeField] Color[] colors;
    private void Awake()
    {
        pile.transform.localPosition = new Vector3(pile.transform.localPosition.x, -0.8f, 0);
    }

    public void OnMouseDown()
    {
        transform.localRotation = Quaternion.identity;
        dragOffset = transform.position - GetMousePos();
        isDrag = true;
        Grinder.GetComponent<Animator>().SetTrigger("appear");
    }
    public void OnMouseUp()
    {
        isDrag = false;
        Grinder.GetComponent<Animator>().SetTrigger("appear");
    }

    public void OnMouseDrag()
    {
        transform.position = GetMousePos() + dragOffset; //좌표차이를 상쇄해주기
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
    }
    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ingredient"))
        {
            if (collision.transform.childCount > 0)
            {
                GameObject activeObject = collision.gameObject;
                pile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[collision.transform.GetChild(collision.transform.childCount -1).GetComponent<ChildData>().ingreType];
                activeObject.GetComponent<Animator>().SetTrigger("grind");
                Debug.Log("재료 갈리는중");
                activeObject.transform.GetChild(activeObject.transform.childCount-1) .GetComponent<ChildData>().grinding += 1;
                CheckPile(activeObject.transform.GetChild(activeObject.transform.childCount - 1).GetComponent<ChildData>());
                //if (collision.gameObject.GetComponent<DragDrop>().grinding > 9)
                //{
                //    collision.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
                //}
            }
        }
    }
    private void CheckPile(ChildData drag)
    {
        float y;
        if (drag.grinding == 1)
        {
            y = -0.3f;
        }
        else if (drag.grinding == 2)
        {
            y = -0.2f;
        }
        else if (drag.grinding == 3)
        {
            y = -0.1f;
        }
        else if (drag.grinding == 4)
        {
            y = 0f;
        }
        else if (drag.grinding == 5)
        {
            y = 0.1f;
        }
        else if (drag.grinding == 6)
        {
            y = 0.2f;
        }
        else if (drag.grinding == 7)
        {
            y = 0.3f;
        }
        else if (drag.grinding == 8)
        {
            y = 0.4f;
        }
        else if (drag.grinding == 9)
        {
            y = 0.5f;
        }
        else
        {
            y = pile.transform.localPosition.y;
        }
        pile.transform.localPosition = new Vector3(pile.transform.localPosition.x, y, 0);
    }

}
