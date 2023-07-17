using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Data
{
    public int DayCount = 1;
    //public bool isFirstStart = true;
    public int Coin = 100;
    public int Success = 0;
    public int PopulLevel = 0;
    public int Karma = 0;

    public int[] IngreQuantity = { 4, 4, 4, 4, 4, 4, 4, 4, 4 };
    public int[] PotionQuantity = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public PotionDetail[] PotionDetails = new PotionDetail[9];
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;

    public Data nowData = new Data();

    public string path;
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

        path = Application.persistentDataPath + "/save";
    }

    public void SaveData()
    {
        string strData = JsonUtility.ToJson(nowData);
        File.WriteAllText(path, strData);
    }
    public void LoadData()
    {
        string strData = File.ReadAllText(path);
        nowData = JsonUtility.FromJson<Data>(strData);
    }
    public void DataClear()
    {
        nowData = new Data();
    }
}
