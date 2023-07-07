using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderCollider : MonoBehaviour
{
    public GameObject pile;
    public GameObject activeIngredient;
    [SerializeField] private Color[] colors;
    //{ WaterBloom = 0, WindBloom, LifeLeaf, MadMushroom, RainbowCap, Shadow, Thunder, WaterCap, WitchMushroom }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ingredient") && collision.GetComponent<IngreDrag>())
        {
            if (collision.GetComponent<IngreDrag>().grinding > 0 && !collision.GetComponent<IngreDrag>().isDrag)
            {
                collision.transform.GetChild(collision.transform.childCount - 1).gameObject.SetActive(false);
                pile.SetActive(true);
            }
            if (activeIngredient != null && activeIngredient != collision)
            {
                ResetPile(activeIngredient);
            }
            collision.GetComponent<IngreDrag>().isInGrinder = true;
            activeIngredient = collision.gameObject;
            pile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[collision.GetComponent<IngreDrag>().ingreType];
        }

        if (collision.CompareTag("handle") && activeIngredient!= null && activeIngredient.GetComponent<IngreDrag>().isInGrinder)
        {
            activeIngredient.GetComponent<Animator>().SetTrigger("grind");
            activeIngredient.GetComponent<IngreDrag>().grinding += 1;
            CheckPile(activeIngredient.GetComponent<IngreDrag>());
        }
        if (collision.gameObject.CompareTag("handle"))
        {
            pile.GetComponent<Animator>().SetTrigger("grind");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ingredient") && collision.GetComponent<IngreDrag>()) //갈던 애를 빼내고 새로 갈려고 한다면? 해보고 예외처리 하기
        {
            collision.GetComponent<IngreDrag>().isInGrinder = false;
            activeIngredient = null;
        }
    }

    private void ResetPile(GameObject active)
    {
        if (active.GetComponent<IngreDrag>().grinding == 0)
        {
            pile.transform.localPosition = new Vector3(pile.transform.localPosition.x, -3, 0);
        }
        pile.SetActive(true);
    }
    private void CheckPile(IngreDrag drag)
    {
        float y;
        if (drag.grinding == 1)
        {
            y = -0.15f;
        }
        else if (drag.grinding == 2)
        {
            y = 0f;
        }
        else if (drag.grinding == 3)
        {
            y = 0.04f;
        }
        else if (drag.grinding == 4)
        {
            y = 0.07f;
        }
        else if (drag.grinding == 5)
        {
            y = 0.14f;
        }
        else if (drag.grinding == 6)
        {
            y = 0.21f;
        }
        else if (drag.grinding == 7)
        {
            y = 0.3f;
        }
        else if (drag.grinding == 8)
        {
            y = 0.4f;
        }
        else if (drag.grinding == 9)
        {
            y = 0.5f;
        }
        else if (drag.grinding == 10)
        {
            y = 0.6f;
        }
        else
        {
            y = pile.transform.localPosition.y;
        }
        pile.transform.localPosition = new Vector3(pile.transform.localPosition.x, y, 0);
    }
}
