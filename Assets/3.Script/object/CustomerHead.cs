using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerHead : MonoBehaviour
{
    float angle;
    Vector2 target, mouse;

    private void Start()
    {
        target = transform.position;
    }
    private void Update() //-30 ~ 50
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //Debug.Log(transform.rotation.eulerAngles.z); //320 ~ 50
        if (transform.rotation.eulerAngles.z < 320 && transform.rotation.eulerAngles.z > 50) transform.rotation = Quaternion.Euler(0, 0, 360);
        //else if (transform.rotation.eulerAngles.z > 50) { transform.rotation = new Quaternion(0, 0, 90, 0); }

    } 
}
