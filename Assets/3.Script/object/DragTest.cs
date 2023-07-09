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
    public int grinding = 0;
    [SerializeField] GameObject pile;
    [SerializeField] private float angleOffset;

    //자식객체 선택할때 원위치로 돌아오는 변수
    [SerializeField] List<Vector3> positions = new List<Vector3>();
    [SerializeField] List<Vector3> rotations = new List<Vector3>();
    Vector3 speed = Vector3.zero;
    float rotateSpeed = 3;
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
                else //spoon
                {
                    selectedObject = hit.collider.gameObject;
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(selectedObject.transform.parent.position);
                    Vector3 vec3 = Input.mousePosition - screenPos;
                    angleOffset = (Mathf.Atan2(selectedObject.transform.right.y, selectedObject.transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
                    selectedObject.transform.localRotation = Quaternion.identity;
                }
                selectedObject.transform.rotation = Quaternion.identity;
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
                if (selectedObject.transform.position.x > 4.8f)
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
                selectedObject.transform.localRotation = Quaternion.identity;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 screenPos = Camera.main.WorldToScreenPoint(selectedObject.transform.parent.position);
                Vector3 vec3 = Input.mousePosition - screenPos;
                float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                float z = angle + angleOffset;
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
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            if (selectedObject != null && selectedObject.CompareTag("ingredient") && selectedObject.transform.childCount > 0) selectedObject.transform.GetChild(selectedObject.transform.childCount - 1).GetComponent<ChildData>().isDrag = isDrag;
            if (isInven) //인벤토리 위에서 놓으면
            {
                InvenItemManager.instance.IngreQuantity[selectedObject.transform.GetChild(selectedObject.transform.childCount - 1).GetComponent<ChildData>().ingreType]++;
                InvenItemManager.instance.UpdateInventory();
                Destroy(selectedObject);
                selectedObject = null;
            }
            else if (selectedObject != null && selectedObject.CompareTag("ingredient") && isPot)
            { //냄비 위에서 놓으면
                //GetComponent<Animator>().SetTrigger("grind"); //들어가는 애니메이션 재생
                FindObjectOfType<Pot>().containIngredients[selectedObject.transform.GetChild(selectedObject.transform.childCount - 1).GetComponent<ChildData>().ingreType]++;
                Destroy(selectedObject);
                FindObjectOfType<Pot>().transform.parent.GetChild(1).GetComponent<Animator>().SetTrigger("splash");
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
        }
    }

    Vector3 mousePos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }

}
