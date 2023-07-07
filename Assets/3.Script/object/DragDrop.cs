using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Sprite grindedCircle;
    public int ingreType;
    private Vector3 dragOffset; // 클릭했을때, object의 중앙좌표와과 클릭한 좌표사이의 차이
    public bool isContact = false;
    public bool isDrag = false;
    List<Vector3> positions = new List<Vector3>();
    List<Vector3> rotations = new List<Vector3>();
    Vector3 speed = Vector3.zero;
    float rotateSpeed = 1;
    public bool canActive = true;
    public bool isInven = true;
    public int grinding = 0;
    void Awake()
    {
        cam = Camera.main;
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                positions.Add(transform.GetChild(i).localPosition);
                rotations.Add(transform.GetChild(i).localEulerAngles);
            }
        }
    }
    private void Update()
    {
        if (isDrag && transform.childCount > 0)
        {
            for (int i =0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).localPosition != positions[i])
                {
                    transform.GetChild(i).transform.localPosition = Vector3.SmoothDamp(transform.GetChild(i).localPosition, positions[i], ref speed, 0.02f);
                    
                }
                if (transform.GetChild(i).localEulerAngles.z != rotations[i].z)
                {
                    float xAngle = Mathf.SmoothDampAngle(transform.GetChild(i).localEulerAngles.x, rotations[i].x, ref rotateSpeed, 0.02f);
                    float yAngle = Mathf.SmoothDampAngle(transform.GetChild(i).localEulerAngles.y, rotations[i].y, ref rotateSpeed, 0.02f);
                    float zAngle = Mathf.SmoothDampAngle(transform.GetChild(i).localEulerAngles.z, rotations[i].z, ref rotateSpeed, 0.02f);
                    transform.GetChild(i).localEulerAngles = new Vector3(xAngle, yAngle, zAngle);
                }
            }
            
        }
        if (isDrag && isInven)
        {
            InvenItemManager.instance.showShadow = true;
        }
        else if (isDrag && !isInven)
        {
            InvenItemManager.instance.showShadow = false;
        }
        if (!isDrag && grinding >= 10)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularDrag = 0;
        }
    }

    void OnMouseDown() //클릭
    {
        if (canActive)
        {
            if (grinding > 0)//한번이라도 갈렸으면 동그란 모양 키자
            {
                transform.GetChild(gameObject.transform.childCount - 1).gameObject.SetActive(true);
                transform.GetComponent<CircleCollider2D>().isTrigger = true;
            }
            transform.localRotation = Quaternion.identity;
            dragOffset = transform.position - GetMousePos();
            isDrag = true;
            GetComponent<SpriteRenderer>().sortingOrder += 10;
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount-1; i++)
                {
                    transform.GetChild(i).GetComponent<Rigidbody2D>().gravityScale = 0;
                    if (transform.GetChild(i).GetComponent<SpriteRenderer>()) transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder += 10;
                }
            }
        }
    }
    private void OnMouseUp()
    {
        isDrag = false;
        if (!isInven)
        {
            if (grinding >= 10)
            {
                GetComponent<CircleCollider2D>().isTrigger = false;
            }
            GetComponent<SpriteRenderer>().sortingOrder = 6;
            isContact = false;
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount-1; i++)
                {
                    transform.GetChild(i).GetComponent<Rigidbody2D>().gravityScale = 1;
                    if (transform.GetChild(i).GetComponent<SpriteRenderer>()) transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder -= 10;
                }
            }
        }
        else
        {
            InvenItemManager.instance.IngreQuantity[ingreType]++;
            InvenItemManager.instance.UpdateInventory();
            Destroy(gameObject);
        }
        
    }

    void OnMouseDrag() //드래그중
    {
        isDrag = true;
        if (!isContact)
        {
            transform.position = GetMousePos() + dragOffset; //좌표차이를 상쇄해주기
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    transform.GetChild(i).GetComponent<Rigidbody2D>().angularDrag = 0;
                }
            }
        }
    }

    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDrag && collision.gameObject.CompareTag("wall"))
        {
            isContact = true;
        }
        if (!isDrag && collision.gameObject.CompareTag("grinder") && grinding > 1)
        {
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isDrag && collision.gameObject.CompareTag("wall"))
        {
            isContact = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("inven"))
        {
            isInven = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("inven"))
        {
            isInven = false;
        }
    }
}
