using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMgr : MonoBehaviour
{
    public static StageMgr Instance // singlton     
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StageMgr>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("StageMgr");
                    instance = instanceContainer.AddComponent<StageMgr>();
                }
            }
            return instance;
        }
    }
    private static StageMgr instance;

    public GameObject Player;
    int StageNum = 0;

    [System.Serializable]
    public class StartPositionArray
    {
        public List<Transform> StartPosition = new List<Transform>();
    }

    public StartPositionArray[] startPositionArrays; // 0.1.2


    //startPosition[0] -> 1~10 stage 담당
    //startPosition[1] -> 11~20 stage 담당 하는 식으로 가능.

    //int LastStage = 9;

    void Strat()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void NextStage() {
            StageNum++;
            int arrayIndex = StageNum/10;
            int randomIndex = Random.Range(0, startPositionArrays[arrayIndex].StartPosition.Count);
            Player.transform.position = startPositionArrays[arrayIndex].StartPosition[randomIndex].position;
            startPositionArrays[arrayIndex].StartPosition.RemoveAt(randomIndex);
        }
}