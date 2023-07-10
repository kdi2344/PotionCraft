using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterArrow : MonoBehaviour
{
    [SerializeField] Transform target;
    private void Update()
    {

        if (transform.position.x < target.position.x + 1f && transform.position.x > target.position.x - 1f && transform.position.y < target.position.y + 1f && transform.position.y > target.position.y - 1f)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
