using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChecker2 : MonoBehaviour
{
    // 원하는 씬 이름을 설정합니다.
    public string targetSceneName = "TargetScene";

    void Start()
    {
        // 현재 씬의 이름을 가져옵니다.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 현재 씬이 원하는 씬인지 확인합니다.
        if (currentSceneName == targetSceneName)
        {
         // AudioManager.instance.PlaySfx(AudioManager.sfx.Death);
    
        }
        else
        {
           //AudioManager.instance.StopSfx(AudioManager.sfx.Death);
        }
    }



    /*
    // 실행하고자 하는 함수 A를 정의합니다.
    void ExecuteFunctionA()
    {
        Debug.Log("Function A is executed because the current scene is the target scene.");
        // 여기에 실행할 코드를 작성합니다.
    }
    */
}
