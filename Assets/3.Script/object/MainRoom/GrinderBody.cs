using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderBody : MonoBehaviour
{
    [SerializeField] GameObject pile;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ingredient") && collision.transform.childCount > 0 && collision.transform.GetChild(collision.transform.childCount - 1).GetComponent<ChildData>() && collision.transform.GetChild(collision.transform.childCount-1).GetComponent<ChildData>().grinding > 0 && collision.transform.GetChild(collision.transform.childCount - 1).GetComponent<ChildData>().isDrag)
        {
            collision.transform.GetChild(collision.transform.childCount - 2).gameObject.SetActive(false);
            pile.SetActive(true);
        }
    }
}

