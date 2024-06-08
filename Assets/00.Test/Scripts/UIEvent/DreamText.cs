using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;


public class DreamText : MonoBehaviour
{

    
    [Header("#Typing")]
    public AudioClip TypingClip;
    public float TypingVolume;
    public static AudioSource TypingPlayer;


    
public TMP_Text tutorialTxt;

    //public Text text;

    string dialogue;

    public String[] tutorialDialogue;

    public String[] dialogues;

    void Start()
    {
        StartTalk(tutorialDialogue);
    }


    

   //GameObject TypingObject = new GameObject("TypingPlayer");
   IEnumerator Typing (String talk)
   {
    //텍스트를 null 값으로 설정
    tutorialTxt.text = null;



    // 타이핑 소리를 재생
    if (TypingPlayer != null && TypingClip != null)
    {
        TypingPlayer.clip = TypingClip;
        TypingPlayer.volume = TypingVolume;
        TypingPlayer.Play();
    }


    tutorialTxt.DOText(talk, 4f, true, ScrambleMode.Numerals).SetDelay(2); // 모든 문자가 출력되는데 걸리는 시간 = N초 
    

    // 다음 대사 딜레이 
    yield return new WaitForSeconds(4.0f); // 딜레이 N초
    NextTalk();
   }

   //다음 대사 출력을 위한 정수 
    public int talkNum;

    public void StartTalk(String[] talks)
    {
        dialogues = talks;

        //tutoNum 번째 대사 출력 
        StartCoroutine(Typing(dialogues[talkNum]));
        
        
        
    }

    public void NextTalk()
    {
        tutorialTxt.text = null;
        // 다음 배열 출력하기 위해 +1
        talkNum++;

        //tutoNum이 배열의 길이랑 같으면 끝내기
        if(talkNum == dialogues.Length)
        {
            EndTalk();
            return;
            
            
        }

        StartCoroutine(Typing(dialogues[talkNum]));
    }

    public static bool isActing = false;
    public void EndTalk()
    {
        //tutoNum 초기화 
        talkNum = 0;
        Debug.Log("자막 끝!");
        
        Invoke("LoadScene", 4.0f);
        isActing = true;
    }

       public void LoadScene()  
    {
       // AudioManager.instance.PlayBgm(true);
        SceneManager.LoadScene("GamePlayScene");
       
        
    }
     

   
}
        
