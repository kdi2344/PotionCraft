using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngreDrag : MonoBehaviour
{
    private Vector3 dragOffset;
    public bool isInven = false;
    public bool isDrag = false;
    public bool isOnPot = false;
    public bool isOnInv = false;
    public int ingreType;

    //�׶��δ� �ȿ� �ִ���
    public bool isInGrinder = false;
    public int grinding = 0;

    //�ڽİ�ü �����Ҷ� ����ġ�� ���ƿ��� ����
    List<Vector3> positions = new List<Vector3>();
    List<Vector3> rotations = new List<Vector3>();
    Vector3 speed = Vector3.zero;
    float rotateSpeed = 3;
    private void Awake()
    {
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
        if (isDrag)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).localPosition != positions[i])
                {
                    transform.GetChild(i).transform.localPosition = Vector3.SmoothDamp(transform.GetChild(i).localPosition, positions[i], ref speed, 0.01f);
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
    }
    public void OnMouseDown() //Ŭ��
    {
        transform.localRotation = Quaternion.identity;
        isDrag = true;
        dragOffset = transform.position - GetMousePos();
        if (grinding > 0) //�ѹ��̶� ���Ҵٸ�
        {
            transform.GetChild(transform.childCount-1).gameObject.SetActive(true);
            FindObjectOfType<GrinderCollider>().pile.SetActive(false);
        }
    }
    public void OnMouseUp()
    {
        isDrag = false;
        if (isOnPot) //���� ������ ������
        {
            GetComponent<Animator>().SetTrigger("grind"); //���� �ִϸ��̼� ���
            FindObjectOfType<Pot>().containIngredients[ingreType]++;
            DeleteSelf();
            FindObjectOfType<Pot>().transform.parent.GetChild(1).GetComponent<Animator>().SetTrigger("splash");
        }
    }

    public void OnMouseDrag() //�巡����
    {
        transform.position = GetMousePos() + dragOffset;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.GetChild(i).GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }

    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pot"))
        {
            isOnPot = true;
        }
        else if (collision.CompareTag("inven"))
        {
            isInven = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("pot"))
        {
            isOnPot = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("pot"))
        {
            isOnPot = false;
        }
        else if (collision.CompareTag("inven"))
        {
            isInven = false;
        }
    }

    private void DeleteSelf()
    {
        Destroy(gameObject);
    }
}
