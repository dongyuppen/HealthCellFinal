using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Datas
{
    public int level = 0;
    public float time = 0;
    public bool ischeck = false;
    public string user = "";
}
public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public Datas datas;

    private string keyName = "Datas";
    private string fileName = "SaveFile.es3"; 

   
    void Start()
    {
        instance = this;
        DataLoad();
    }

    
    public void DataSave()
    {
        ES3.Save("Datas", datas);
    }
    public void DataLoad()
    {
        if(ES3.FileExists())
        {
            ES3.LoadInto(keyName, datas);
        }
        else
        {
            DataSave();
        }
    }

}
