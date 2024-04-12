using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; // 이걸 써야 씬 전환 가능한 코드 사용가능

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(instance == null)
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }
    /*else
    {
        Destroy(This.gmaeObject);
            
        
    }*/
    public GameObject go;
   
    public AudioClip theAudio;

    public String call_sound;
    public String cancel_sound;  
    private bool activated;
    public void Exit()
    {
        Application.Quit();  //게임 종료
    }

    public void Continue() //계속
    {
        activated = false;
        go.SetActive(false);
        //theAudio.Play(cancel_sound);
    }

    public void GoTitle() //타이틀로
    {
        SceneManager.LoadScene("IntroScene"); 
        activated = false;
        go.SetActive(false);
    }
    

    public GameObject pauseMenu;
    public static bool isPause;
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

            activated = !activated;
            if(activated)
            {
                go.SetActive(true);
                //theAudio.Play(call_sound);
            }
            else
            {
                 go.SetActive(false);
                 //theAudio.Play(cancel_sound);
            }
        }
       
    }
}
