
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    public static Potal Instance // singlton >> singlton call other script function
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Potal>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("Potal");
                    instance = instanceContainer.AddComponent<Potal>();
                }
            }
            return instance;
        }
    }
    private static Potal instance;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Potal"))
        {
            Debug.Log("ºÎµúÇû½À´Ï´Ù.");
            // StageMgr ÄÄÆ÷³ÍÆ®ÀÇ NextStage ÇÔ¼ö È£Ãâ
            StageMgr.Instance.NextStage();
            //GetComponent<StageMgr>().NextStage();
        }
    }
}
