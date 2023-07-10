using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move Detail", menuName = "Scriptable Object/Move Detail")]

public class MoveDetail : ScriptableObject
{
    public Transform[] fixPoints;
    public GameObject[] fixLines;
    public Transform[] addPoints;
    public GameObject[] addLines;
    public Sprite preview;
    public int addRange = 0;
}
