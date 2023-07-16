using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Data
{
    public int DayCount = 1;
    public bool isFirstCustomer = true;
    public int Coin = 100;
    public int Success = 0;
    public int PopulLevel = 0;
    public int Karma = 0;

    public int[] IngreQuantity = { 4, 4, 4, 4, 0, 0, 0, 0, 0 };
    public int[] PotionQuatnity = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public PotionDetail[] PotionDetails;
}

public class DataManager : MonoBehaviour
{

    public static DataManager instance = null;

    Data nowData = new Data();

    string path;
    string fileName = "save";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/";
    }

    private void Start()
    {
        
    }

    public void SaveData()
    {
        string strData = JsonUtility.ToJson(nowData);
        File.WriteAllText(path + fileName, strData);
    }
    public void LoadData()
    {
        string strData = File.ReadAllText(path + fileName);
        nowData = JsonUtility.FromJson<Data>(strData);
    }
}
