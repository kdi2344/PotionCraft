using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoonHandler : MonoBehaviour
{
    [SerializeField] bool isDrag = false;
    [SerializeField] Vector3 mousePos;
    [SerializeField] private float angleOffset;
    private PolygonCollider2D col;
    private Vector3 screenPos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
