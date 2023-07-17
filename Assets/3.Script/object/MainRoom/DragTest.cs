using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTest : MonoBehaviour
{
    public LayerMask drag;
    [SerializeField] GameObject selectedObject;
    public bool isDrag = false;
    public bool isInven = false;
    public bool isPot = false;
    public bool isScale = false;
    public int grinding = 0;
    [SerializeField] GameObject pile;
    [SerializeField] private float angleOffset;
    Vector3 dragOffset;

    //자식객체 선택할때 원위치로 돌아오는 변수
    [SerializeField] List<Vector3> positions = new List<Vector3>();
    [SerializeField] List<Vector3> rotations = new List<Vector3>();
    Vector3 speed = Vector3.zero;
    float rotateSpeed = 3;
    float z;
    float angle = 0;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrag = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, drag);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("ingredient"))
                {
                    selectedObject = hit.collider.transform.parent.gameObject;
                    selectedObject.GetComponent<SpriteRenderer>().sortingOrder = 1000;
                    grinding = selectedObject.transform.GetChild(selectedObject.transform.childCount - 1).GetComponent<ChildData>().grinding;
                    selectedObject.transform.GetChild(selectedObject.transform.childCount - 1).GetComponent<ChildData>().isDrag = isDrag;
                    if (selectedObject.GetComponent<CircleCollider2D>()) selectedObject.GetComponent<CircleCollider2D>().isTrigger = true;
                    if (selectedObject.transform.childCount > 0)
                    {
                        for (int i = 0; i < selectedObject.transform.childCount - 1; i++)
                        {
                            if (selectedObject.transform.GetChild(i).GetComponent<CircleCollider2D>()) selectedObject.transform.GetChild(i).GetComponent<CircleCollider2D>().isTrigger = true;
                            if (selectedObject.transform.GetChild(i).GetComponent<CapsuleCollider2D>()) selectedObject.transform.GetChild(i).GetComponent<CapsuleCollider2D>().isTrigger = true;
                        }
                    }
                    for (int i = 0; i < hit.collider.GetComponent<ChildData>().positions.Count; i++)
                    {
                        positions.Add(hit.collider.GetComponent<ChildData>().positions[i]);
                        rotations.Add(hit.collider.GetComponent<ChildData>().rotations[i]);
                        if (selectedObject.transform.GetChild(i).GetComponent<SpriteRenderer>()) selectedObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 999;
                    }

                    if (selectedObject.transform.GetChild(selectedObject.transform.childCount - 1).GetComponent<ChildData>().grinding > 0)
                    {//한번이라도 갈은거면 
                        selectedObject.transform.GetChild(selectedObject.transform.childCount - 2).gameObject.SetActive(true);
                        if (pile.activeSelf) pile.SetActive(false);
                    }
                }
                else if (hit.collider.CompareTag("handle"))
                {
                    selectedObject = hit.collider.transform.parent.gameObject;
                }
                else if (hit.collider.CompareTag("potion"))
                {
                    if (FindObjectOfType<CustomerManager>().currentPotion != null)
                    {
                        FindObjectOfType<CustomerManager>().currentPotion = null;
                        FindObjectOfType<CustomerManager>().currentPotionOb = null;
                        FindObjectOfType<CustomerManager>().CheckPotion();
                    }
                    hit.collider.isTrigger = true;
                    selectedObject = hit.collider.transform.parent.parent.gameObject;
                    selectedObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    selectedObject.transform.rotation = Quaternion.identity;
                    for (int i = 0; i < selectedObject.transform.childCount; i++)
                    {
                        if (selectedObject.transform.GetChild(i).GetComponent<SpriteRenderer>()) selectedObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder += 1000;
                        for (int j = 0; j < selectedObject.transform.GetChild(i).childCount; j++)
                        {
                            if (selectedObject.transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>()) selectedObject.transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().sortingOrder += 1000;
                        }
                    }
                }
                else //spoon
                {
                    if (hit.collider.CompareTag("spoon"))
                    {
                        selectedObject = hit.collider.gameObject;
                        Vector3 screenPos = Camera.main.WorldToScreenPoint(selectedObject.transform.parent.position);
                        Vector3 vec3 = Input.mousePosition - screenPos;
                        angleOffset = (Mathf.Atan2(selectedObject.transform.right.y, selectedObject.transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
                        selectedObject.transform.localRotation = Quaternion.identity;
                    }
                    else
                    {
                        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePos.z = 0;
                        selectedObject = hit.collider.gameObject;
                        dragOffset = selectedObject.transform.position - mousePos;
                    }
                }
                //selectedObject.transform.rotation = Quaternion.identity;
            }
            else
            {
                positions.Clear();
                rotations.Clear();
            }
        }

        if (isDrag)
        {
            if (selectedObject == null)
            {
                isDrag = false;
                return;
            }
            

            if (selectedObject.CompareTag("ingredient"))
            {
                Vector3 pos = mousePos();
                selectedObject.transform.position = pos;
                selectedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                selectedObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                if ((selectedObject.transform.position.x > 4.8f && selectedObject.transform.position.x < 12f) || (selectedObject.transform.position.x > 23f) || (selectedObject.transform.position.x > -13f && selectedObject.transform.position.x < -9f))
                {
                    isInven = true;
                }
                else
                {
                    isInven = false;
                }

                for (int i = 0; i < selectedObject.transform.childCount; i++) //잡는 영역 지정하는거 빼고 
                {
                    if (selectedObject.transform.GetChild(i).GetComponent<Rigidbody2D>())
                    {
                        selectedObject.transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        selectedObject.transform.GetChild(i).GetComponent<Rigidbody2D>().angularDrag = 0;
                    }

                    if (selectedObject.transform.GetChild(i).localPosition != positions[i])
                    {
                        selectedObject.transform.GetChild(i).localPosition = Vector3.SmoothDamp(selectedObject.transform.GetChild(i).localPosition, positions[i], ref speed, 0.01f);
                    }
                    if (selectedObject.transform.GetChild(i).localEulerAngles.z != rotations[i].z)
                    {
                        float xAngle = Mathf.SmoothDampAngle(selectedObject.transform.GetChild(i).localEulerAngles.x, rotations[i].x, ref rotateSpeed, 0.02f);
                        float yAngle = Mathf.SmoothDampAngle(selectedObject.transform.GetChild(i).localEulerAngles.y, rotations[i].y, ref rotateSpeed, 0.02f);
                        float zAngle = Mathf.SmoothDampAngle(selectedObject.transform.GetChild(i).localEulerAngles.z, rotations[i].z, ref rotateSpeed, 0.02f);
                        selectedObject.transform.GetChild(i).localEulerAngles = new Vector3(xAngle, yAngle, zAngle);
                    }
                }
            }
            else if (selectedObject.CompareTag("handle"))
            {
                Vector3 pos = mousePos();
                selectedObject.transform.position = pos;
                selectedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                selectedObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
            }
            else if (selectedObject.CompareTag("spoon"))
            {
                float originAngle = angle;
                selectedObject.transform.localRotation = Quaternion.identity;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 screenPos = Camera.main.WorldToScreenPoint(selectedObject.transform.parent.position);
                Vector3 vec3 = Input.mousePosition - screenPos;
                angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                if (angle != originAngle && FindObjectOfType<SpoonHandler>().move != null)
                {
                    FindObjectOfType<SpoonHandler>().MapMove();
                }
                z = angle + angleOffset;
                if (z < -10)
                {
                    z = -10f;
                }
                else if (z > 10)
                {
                    z = 10;
                }
                selectedObject.transform.localPosition = new Vector3(0, 7.05f, 0);
                selectedObject.transform.parent.eulerAngles = new Vector3(0, 0, z);
            }
            else if (selectedObject.CompareTag("potion"))
            {
                Vector3 pos = mousePos();
                selectedObject.transform.position = pos;
                selectedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                selectedObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                if ((selectedObject.transform.position.x > 4.8f && selectedObject.transform.position.x < 12f) || (selectedObject.transform.position.x > 23f) || (selectedObject.transform.position.x > -13f && selectedObject.transform.position.x < -9f))
                {
                    isInven = true;
                }
                else
                {
                    isInven = false;
                }
            }
            else
            {
                Vector3 pos = mousePos();
                selectedObject.transform.position = pos + dragOffset;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            //포션 놓기
            if (selectedObject != null && selectedObject.CompareTag("potion") && isScale) //저울에서 드래그 끝났다면
            {
                selectedObject.transform.localPosition = new Vector3(-21.2700005f, -3.5999999f, 0);
                selectedObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
                FindObjectOfType<CustomerManager>().currentPotion = selectedObject.GetComponent<Potion>().PotionData;
                FindObjectOfType<CustomerManager>().currentPotionOb = selectedObject;
                FindObjectOfType<CustomerManager>().CheckPotion();
                selectedObject = null;
            }
            else if (selectedObject != null && selectedObject.CompareTag("potion") && isInven)
            {
                DataManager.instance.nowData.PotionQuantity[selectedObject.GetComponent<Potion>().index]++;
                InvenItemManager.instance.UpdateInventory();
                Destroy(selectedObject);
                selectedObject = null;
            }
            else if (selectedObject != null && selectedObject.CompareTag("potion")) selectedObject.transform.GetChild(0).GetChild(0).GetComponent<PolygonCollider2D>().isTrigger = false;

            //재료 놓기
            if (selectedObject != null && selectedObject.GetComponent<CircleCollider2D>()) selectedObject.GetComponent<CircleCollider2D>().isTrigger = false;
            if (selectedObject != null && selectedObject.transform.childCount > 0)
            {
                for (int i = 0; i < selectedObject.transform.childCount - 1; i++)
                {
                    if (selectedObject.transform.GetChild(i).GetComponent<CircleCollider2D>()) selectedObject.transform.GetChild(i).GetComponent<CircleCollider2D>().isTrigger = false;
                }
            }
            if (selectedObject != null && selectedObject.CompareTag("ingredient") && selectedObject.transform.childCount > 0)
            {
                selectedObject.transform.GetChild(selectedObject.transform.childCount - 1).GetComponent<ChildData>().isDrag = isDrag;
                for (int i = 0; i < selectedObject.transform.childCount - 1; i++)
                {
                    if (selectedObject.transform.GetChild(i).GetComponent<CircleCollider2D>()) selectedObject.transform.GetChild(i).GetComponent<CircleCollider2D>().isTrigger = false;
                    if (selectedObject.transform.GetChild(i).GetComponent<CapsuleCollider2D>()) selectedObject.transform.GetChild(i).GetComponent<CapsuleCollider2D>().isTrigger = false;
                }
            }
            if (selectedObject != null && selectedObject.CompareTag("ingredient") && isInven) //인벤토리 위에서 놓으면
            {
                DataManager.instance.nowData.IngreQuantity[selectedObject.transform.GetChild(selectedObject.transform.childCount - 1).GetComponent<ChildData>().ingreType]++;
                InvenItemManager.instance.UpdateInventory();
                Destroy(selectedObject);
                selectedObject = null;
            }
            else if (selectedObject != null && selectedObject.CompareTag("ingredient") && isPot)
            { //냄비 위에서 놓으면
                SoundManager.instance.PlayEffect("pot");
                FindObjectOfType<Pot>().containIngredients[selectedObject.transform.GetChild(selectedObject.transform.childCount - 1).GetComponent<ChildData>().ingreType]++;
                FindObjectOfType<Pot>().transform.parent.GetChild(1).GetComponent<Animator>().SetTrigger("splash");
                FindObjectOfType<SpoonHandler>().ResetTarget(selectedObject.transform.GetChild(selectedObject.transform.childCount - 1).GetComponent<ChildData>().move); //이동경로 추가
                //이동경로 눈으로 보이게 추가
                Destroy(selectedObject);
            }
            else if (selectedObject !=null && selectedObject.CompareTag("ingredient"))
            {
                selectedObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
                for (int i = 0; i < selectedObject.transform.childCount; i++)
                {
                    if (selectedObject.transform.GetChild(i).GetComponent<SpriteRenderer>()) selectedObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 9;
                }
                selectedObject = null;
                grinding = 0;
                positions.Clear();
                rotations.Clear();
            }
            else if (selectedObject != null && selectedObject.CompareTag("spoon"))
            {
                selectedObject.transform.localPosition = new Vector3(0, 7.05f, 0);
                selectedObject = null;
            }
            else if (selectedObject != null && selectedObject.name.Equals("Whole"))
            {
                selectedObject.transform.localPosition = new Vector3(-0.940000296f, 2.14999986f, 0);
                selectedObject = null;
            }
            else if (selectedObject != null && selectedObject.CompareTag("potion"))
            {
                for (int i = 0; i < selectedObject.transform.childCount; i++)
                {
                    if (selectedObject.transform.GetChild(i).GetComponent<SpriteRenderer>()) selectedObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder -= 1000;
                    for (int j = 0; j < selectedObject.transform.GetChild(i).childCount; j++)
                    {
                        if (selectedObject.transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>()) selectedObject.transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().sortingOrder -= 1000;
                    }
                }
                selectedObject = null;
            }
        }
    }

    Vector3 mousePos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }

}
