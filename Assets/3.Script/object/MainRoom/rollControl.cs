using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollControl : MonoBehaviour
{
    private Animator anim;
    [SerializeField] GameObject roll1;
    Transform pos;

    private void Awake()
    {
        TryGetComponent(out anim);
        pos = gameObject.transform;
    }
    public void RollClose()
    {
        anim.SetTrigger("Okay");
        Invoke("SelfDestroyAndNew", 1f);
    }
    private void SelfDestroyAndNew()
    {
        if (roll1 != null)
        {
            Instantiate(roll1, gameObject.transform.parent);
        }
        else
        {
            transform.parent.gameObject.SetActive(false);
        }
        Destroy(gameObject);
    }
}
