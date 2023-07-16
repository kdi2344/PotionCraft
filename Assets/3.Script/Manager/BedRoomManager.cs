using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BedRoomManager : MonoBehaviour
{
    [SerializeField] GameObject DayPanel;
    [SerializeField] Image DarkPanel;
    float alpha = 0;
    public void BtnNextDay()
    {
        DayPanel.SetActive(false);
        DarkPanel.gameObject.SetActive(true);
        StartCoroutine(BeNextDay());
    }
    IEnumerator BeNextDay()
    {
        while (alpha < 0.4)
        {
            alpha += Time.deltaTime*0.5f;
            DarkPanel.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        while(alpha > 0)
        {
            alpha -= Time.deltaTime*0.5f;
            DarkPanel.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        DarkPanel.gameObject.SetActive(false);
        GameManager.instance.DayCount++;
        GameManager.instance.GardenReset();
        GameManager.instance.CustomerSet();
        GameManager.instance.isFirstCustomer = true;
        CustomerManager.instance.CurrentCustomer = 0;
        CustomerManager.instance.order = 0;
    }
    public void BtnCancel()
    {
        DayPanel.SetActive(false);
    }
    public void ClickBed()
    {
        DayPanel.SetActive(true);
    }
}
