using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellowBody : MonoBehaviour
{
    [SerializeField] float z;
    [SerializeField] Sprite[] bodies;
    private void Awake()
    {
        z = transform.parent.GetChild(2).eulerAngles.z;
    }
    private void Update()
    {
        z = transform.parent.GetChild(2).eulerAngles.z;
        int m = 0;
        for (float i =331.2f; i < 359; i += 0.13f)
        {
            if (CheckRange(z, i))
            {
                GetComponent<SpriteRenderer>().sprite = bodies[m];
                break;
            }
            m++;
        }
    }
    private bool CheckRange(float z, float i)
    {
        if (z >=i && z < i + 0.13f)
        {
            return true;
        }
        return false;
    }
}