using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    private void OnMouseDown()
    {
        FindObjectOfType<BedRoomManager>().ClickBed();
    }
}
