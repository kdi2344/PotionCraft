using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField] Animator anim;
    private Collider2D delete;

    //public List<InvenItemManager.Ingredient> potionIngredients = new List<InvenItemManager.Ingredient>();
    public int[] containIngredients = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //{ WaterBloom = 0, WindBloom, LifeLeaf, MadMushroom, RainbowCap, Shadow, Thunder, WaterCap, WitchMushroom }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<DragDrop>() && !other.GetComponent<DragDrop>().isDrag && other.CompareTag("ingredient") && other.GetComponent<DragDrop>().canActive)
        {
            other.GetComponent<DragDrop>().canActive = false;
            Invoke("Splash", 0.3f);
            delete = other;
            Invoke("Delete", 0.5f);
            containIngredients[other.GetComponent<DragDrop>().ingreType]++; //개수 추가
        }

        if (other.gameObject.CompareTag("ingredient"))
        {
            FindObjectOfType<DragTest>().isPot = true;
        }
    }
    private void Splash()
    {
        anim.SetTrigger("splash");
    }
    private void Delete()
    {
        Destroy(delete.gameObject);
    }
    public void ClearPot()
    {
        for (int i = 0; i < 9; i++)
        {
            containIngredients[i] = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ingredient"))
        {
            FindObjectOfType<DragTest>().isPot = false;
        }
    }

}
