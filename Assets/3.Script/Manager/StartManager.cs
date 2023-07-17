using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class StartManager : MonoBehaviour
{
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnNew;
    [SerializeField] private GameObject ui;

    [SerializeField] bool isSaveFile = false;

    private void Start()
    {
        if (File.Exists(DataManager.instance.path))
        {
            isSaveFile = true;
            btnContinue.interactable = true;
        }
        else
        {
            isSaveFile = false;
            btnContinue.interactable = false;
        }
    }
    public void BtnCheckData()
    {
        SoundManager.instance.PlayEffect("btn");
        if (isSaveFile)
        {
            ui.SetActive(true);
        }
        else
        {
            BtnNewStart();
        }
    }

    public void BtnNewStart()
    {
        if (isSaveFile)
        {
            DataManager.instance.DataClear();
            isSaveFile = false;
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }
    }
    public void BtnContinue()
    {
        SoundManager.instance.PlayEffect("btn");
        DataManager.instance.LoadData();
        SceneManager.LoadScene(1);
    }
    public void BtnExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

}
