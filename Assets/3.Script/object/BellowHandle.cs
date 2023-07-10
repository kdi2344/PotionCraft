using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellowHandle : MonoBehaviour
{
    [SerializeField] bool isDrag = false;
    [SerializeField] Vector3 mousePos;
    [SerializeField] private float angleOffset;
    private PolygonCollider2D col;
    private Vector3 screenPos;
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
    } 


    //1 ~ -28.8
}
