using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoonHandler : MonoBehaviour
{
    [SerializeField] GameObject linesParent;
    [SerializeField] public MoveDetail move;
    [SerializeField] GameObject map;
    float speed = 1f;
    [SerializeField] int point = 0;
    [SerializeField] int finalPoint = 0;
    [SerializeField] Vector3 targetPos;
    [SerializeField] Vector3 dir;
    public void ResetTarget(MoveDetail move)
    {
        point = 0;
        if (move != null)
        {
            this.move = move;
            finalPoint = move.fixPoints.Length + move.addPoints.Length;
            Instantiate(move.fixLines[0], map.transform.parent.position, move.fixLines[0].transform.rotation, linesParent.transform);
            MoveTo();
        }
    }
    private void MoveTo()
    {
        if (point < finalPoint)
        {
            if (point < move.fixPoints.Length)
            {
                targetPos = map.transform.localPosition + move.fixPoints[point].localPosition;
            }
            else if (point - move.fixPoints.Length < move.addPoints.Length)
            {
                targetPos = map.transform.localPosition + move.addPoints[point - move.fixPoints.Length].localPosition;
            }
            dir = targetPos - map.transform.localPosition;
        }
    }

    public void MapMove()
    {
        if (point < finalPoint)
        {
            if (map.transform.localPosition.x < targetPos.x + 0.1f && map.transform.localPosition.x > targetPos.x - 0.1f && map.transform.localPosition.y < targetPos.y + 0.1f && map.transform.localPosition.y > targetPos.y - 0.1f)
            {
                point += 1;
                if (point != finalPoint) MoveTo();
            }
            else
            {
                map.transform.localPosition += dir * speed * Time.deltaTime;
                
            }
        }
        else
        {
            return;
        }
    }
}
