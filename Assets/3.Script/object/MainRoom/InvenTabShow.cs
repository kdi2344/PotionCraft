using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenTabShow : MonoBehaviour
{
    [SerializeField] GameObject invWhole;
    [SerializeField] GameObject invIngre;
    [SerializeField] GameObject invPotion;
    [SerializeField] GameObject invEtc;
    public void BtnWhole()
    {
        invWhole.SetActive(true);
        invIngre.SetActive(false);
        invPotion.SetActive(false);
        invEtc.SetActive(false);
    }
    public void BtnIngre()
    {
        invWhole.SetActive(false);
        invIngre.SetActive(true);
        invPotion.SetActive(false);
        invEtc.SetActive(false);
    }
    public void BtnPotion()
    {
        invWhole.SetActive(false);
        invIngre.SetActive(false);
        invPotion.SetActive(true);
        invEtc.SetActive(false);
    }
    public void BtnEtc()
    {
        invWhole.SetActive(false);
        invIngre.SetActive(false);
        invPotion.SetActive(false);
        invEtc.SetActive(true);
    }
}
