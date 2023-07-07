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
        transform.position = GetMousePos() + dragOffset; //ÁÂÇ¥Â÷ÀÌ¸¦ »ó¼âÇØÁÖ±â
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
            if (collision.gameObject.GetComponent<DragDrop>())
            {
                pile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[collision.gameObject.GetComponent<DragDrop>().ingreType];
            }
            collision.gameObject.GetComponent<Animator>().SetTrigger("grind");
            if (collision.gameObject.GetComponent<DragDrop>())
            {
                pile.GetComponent<Animator>().SetTrigger("grind");
                collision.gameObject.GetComponent<DragDrop>().grinding += 1;
                CheckPile(collision.gameObject.GetComponent<DragDrop>());
                if (collision.gameObject.GetComponent<DragDrop>().grinding > 9)
                {
                    collision.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
                }
            }
        }
    }
    private void CheckPile(DragDrop drag)
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
