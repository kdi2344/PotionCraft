using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoonDrag : MonoBehaviour
{
    public bool isDrag = false;
    private Vector3 dragOffset;
    public float rightRange = 45;
    public float leftRange = -45;
    public bool canDrag = false;
    [SerializeField] Vector3 mousePos, offset, rotation;

    public Transform Center;

    [SerializeField] private Vector3 FirstTouch;
    [SerializeField] private Vector3 LastTouch;
    public void OnMouseDown() //클릭
    {
        //transform.localRotation = Quaternion.identity;
        //dragOffset = transform.position - GetMousePos();
        //transform.localRotation = Quaternion.identity;
        isDrag = true;
        canDrag = true;
        //mousePos = Input.mousePosition;
        FirstTouch.x = Input.mousePosition.x;
        FirstTouch.y = Input.mousePosition.y;
    }
    public void OnMouseUp()
    {
        isDrag = false;
        canDrag = false;
        //GetComponent<SpriteRenderer>().sortingOrder = 6;
    }


    public void OnMouseDrag() //드래그중
    {
        isDrag = true;
        if (canDrag)
        {
            //transform.eulerAngles = GetMousePos() + dragOffset; //좌표차이를 상쇄해주기
            //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //offset = (Input.mousePosition - mousePos);
            //rotation.z = -(offset.z) * Time.deltaTime;

            //transform.Rotate(rotation);


            LastTouch.x = Input.mousePosition.x;
            LastTouch.y = Input.mousePosition.y;
            float angle = Mathf.Atan2(LastTouch.x - FirstTouch.x, LastTouch.y - FirstTouch.y) * Mathf.Rad2Deg;
            //Debug.Log(angle + 180);

            if (angle + 180 == 180)
            {
                angle += 180;
            }
            Quaternion target = Quaternion.LookRotation(Center.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 3f);
            //transform.rotation = Quaternion.Euler(transform.rotation.x, angle + 180, transform.rotation.z);
        }

        //Debug.Log(transform.localEulerAngles.z);
        if (transform.localEulerAngles.z > rightRange && transform.localEulerAngles.z < 330)
        {
            canDrag = false;
        }
        else if (transform.localEulerAngles.z < leftRange && transform.localEulerAngles.z > 30)
        {
            canDrag = false;
        }
        else
        {
            canDrag = true;
        }

        mousePos = Input.mousePosition;
    }

    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos.z = 0;
        return mousePos;
    }

    Vector3 MousePosition()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
