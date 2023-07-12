using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogDisappear : MonoBehaviour
{
    [SerializeField] Transform MapBottle;
    [SerializeField] int Close = 0;
    [SerializeField] Sprite[] Fogs;
    [SerializeField] float distance;
    private void Awake()
    {
        distance = Vector2.Distance(MapBottle.position, transform.position);
        if (distance <= 3 && distance > 2.8f) { Close = 1; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 2.8f && distance > 2.7f) { Close = 2; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 2.7f && distance > 2.6f ) { Close = 3; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 2.6f && distance > 2.5f ) { Close = 4; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 2.5f && distance > 2.4f ) { Close = 5; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 2.4f && distance > 2.3f ) { Close = 6; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 2.3f && distance > 2.2f ) { Close = 7; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 2.2f && distance > 2.1f ) { Close = 8; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 2.2f && distance > 2.0f ) { Close = 9; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 2.0f && distance > 1.9f ) { Close = 10; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 1.9f && distance > 1.8f ) { Close = 11; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 1.8f && distance > 1.7f ) { Close = 12; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 1.7f && distance > 1.6f ) { Close = 13; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 1.6f && distance > 1.5f ) { Close = 14; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 1.5f && distance > 1.4f ) { Close = 15; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 1.4f && distance > 1.3f ) { Close = 16; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 1.3f && distance > 1.2f ) { Close = 17; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 1.2f && distance > 1.1f ) { Close = 18; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 1.1f && distance > 1.0f ) { Close = 19; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 1.0f)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        distance = Vector2.Distance(MapBottle.position, transform.position);
        if (distance <= 3 && distance > 2.8f && Close == 0) { Close = 1; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 2.8f && distance > 2.7f && Close == 1) { Close = 2; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 2.7f && distance > 2.6f && Close == 2) { Close = 3; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close];}
        else if (distance <= 2.6f && distance > 2.5f && Close == 3) { Close = 4; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close];}
        else if (distance <= 2.5f && distance > 2.4f && Close == 4) { Close = 5; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close];}
        else if (distance <= 2.4f && distance > 2.3f && Close == 5) { Close = 6; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close];}
        else if (distance <= 2.3f && distance > 2.2f && Close == 6) { Close = 7; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close];}
        else if (distance <= 2.2f && distance > 2.1f && Close == 7) { Close = 8; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close];}
        else if (distance <= 2.2f && distance > 2.0f && Close == 8) { Close = 9; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 2.0f && distance > 1.9f && Close == 9) { Close = 10; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 1.9f && distance > 1.8f && Close == 10) { Close = 11; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close];}
        else if (distance <= 1.8f && distance > 1.7f && Close == 11) { Close = 12; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close];}
        else if (distance <= 1.7f && distance > 1.6f && Close == 12) { Close = 13; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close];}
        else if (distance <= 1.6f && distance > 1.5f && Close == 13) { Close = 14; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close];}
        else if (distance <= 1.5f && distance > 1.4f && Close == 14) { Close = 15; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close];}
        else if (distance <= 1.4f && distance > 1.3f && Close == 15) { Close = 16; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close];}
        else if (distance <= 1.3f && distance > 1.2f && Close == 16) { Close = 17; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close];}
        else if (distance <= 1.2f && distance > 1.1f && Close == 17) { Close = 18; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close];}
        else if (distance <= 1.1f && distance > 1.0f && Close == 18) { Close = 19; GetComponent<SpriteRenderer>().sprite = Fogs[19 - Close]; }
        else if (distance <= 1.0f)
        {
            Destroy(gameObject);
        }

    }
}
