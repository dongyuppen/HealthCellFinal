using System.Collections;
using System.Collections.Generic;
using UnityEngine; //유니티 엔진 밑에 네임 스페이스 밑에 있는 모노비헤이어 사용가능 
using UnityEngine.SceneManagement; // 이걸 써야 씬 전환 가능한 코드 사용가능

public class IntroManager : MonoBehaviour
{  
    public GameObject StartPanel;
    public GameObject IntroPanel;

    //public GameObject LoadingPanel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayTime(2));
        //초 정하기 
    }

    IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time); 
        //내가 정한 초(2초)를 기다린 다음 실행 
        IntroPanel.SetActive(false);
        StartPanel.SetActive(true); 

        //2초뒤에 인트로 패널 비활성화, 스타트 패널 활성화 
        

    }

    public void GoGameScene() 
    {
        SceneManager.LoadScene("CtlScene"); 
       // GameplayScene
    }

     public void GoGameScene2() 
    {
        SceneManager.LoadScene("TestScene0418_0005"); 
       // GameplayScene
    }

   
   
}
