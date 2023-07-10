using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPdisappear : MonoBehaviour
{
    GameObject select;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision);
        if (collision.CompareTag("exp"))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("disappear");
            //exp Ãß°¡
            Invoke("Delete", 1f);
            select = collision.gameObject;
        }
    }

    private void Delete()
    {
        Destroy(select);
    }
}
