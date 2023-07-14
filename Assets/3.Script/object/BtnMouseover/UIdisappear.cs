using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIdisappear : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        while (GetComponent<CanvasGroup>().alpha > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
            GetComponent<CanvasGroup>().alpha = GetComponent<CanvasGroup>().alpha - Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(gameObject);
    }
}
