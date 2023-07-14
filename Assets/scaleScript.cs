using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("potion"))
        {
            FindObjectOfType<DragTest>().isScale = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("potion"))
        {
            FindObjectOfType<DragTest>().isScale = false;
            FindObjectOfType<CustomerManager>().currentPotion = null;
        }
    }
}
