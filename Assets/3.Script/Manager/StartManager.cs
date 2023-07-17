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

    public void BtnNewStart()
    {
        if (isSaveFile)
        {
            //이전에 존재한 파일 날라감 경고
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
