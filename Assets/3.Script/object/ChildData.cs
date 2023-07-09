using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildData : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();
    public List<Vector3> rotations = new List<Vector3>();
    public int ingreType;
    public int grinding = 0;
    public bool isDrag = false;
    public bool isInven = false;
    public bool isPot = false;
    public bool isInGrinder = false;

    private void Awake()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            positions.Add(transform.parent.GetChild(i).localPosition);
            rotations.Add(transform.parent.GetChild(i).localEulerAngles);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("pot"))
        {
            FindObjectOfType<DragTest>().isPot = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("pot"))
        {
            FindObjectOfType<DragTest>().isPot = false;
        }
    }
}
