using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCheck3 : MonoBehaviour
{
    // 원하는 씬 이름을 설정합니다.
    public string targetSceneName = "TargetScene";

    void Start()
    {
        // 현재 씬의 이름을 가져옵
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 현재 씬이 원하는 씬인지 확인
        if (currentSceneName == targetSceneName)
        {
           GetComponent<AudioSource>().Play();          
            // 원하는 씬이라면 A 함수를 실행
            //ExecuteFunctionA();
        }
        else
        {
             GetComponent<AudioSource>().Stop();       
        }
        
    }



    /*
    // 실행하고자 하는 함수 A를 정의
    void ExecuteFunctionA()
    {
        Debug.Log("Function A is executed because the current scene is the target scene.");
        // 여기에 실행할 코드를 작성
    }
    */
}
