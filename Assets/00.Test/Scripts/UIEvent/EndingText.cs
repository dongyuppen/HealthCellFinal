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


public class EndingText : MonoBehaviour
{
public TMP_Text tutorialTxts;

    //public Text text;

    string dialogue;

    public String[] tutorialDialogues;

    public String[] dialoguess;

    void Start()
    {
        StartTalk(tutorialDialogues);
    }


    

   //GameObject TypingObject = new GameObject("TypingPlayer");
   IEnumerator Typing (String talks)
   {
    //텍스트를 null 값으로 설정
    tutorialTxts.text = null;
    tutorialTxts.DOText(talks, 4f, true, ScrambleMode.Numerals).SetDelay(2); // 모든 문자가 출력되는데 걸리는 시간 = N초 
    
    // 다음 대사 딜레이 
    yield return new WaitForSeconds(4.0f); // 딜레이 N초
    NextTalk();
   }

   //다음 대사 출력을 위한 정수 
    public int talkNums;

    public void StartTalk(String[] talks)
    {
        dialoguess = talks;
        //tutoNum 번째 대사 출력 
        StartCoroutine(Typing(dialoguess[talkNums]));
    }

    public void NextTalk()
    {
        tutorialTxts.text = null;
        // 다음 배열 출력하기 위해 +1
        talkNums++;

        //tutoNum이 배열의 길이랑 같으면 끝내기
        if(talkNums == dialoguess.Length)
        {
            EndTalk();
            return; 
        }
        StartCoroutine(Typing(dialoguess[talkNums]));
    }

    public bool isOn = false;
    public GameObject TBCPanel;
    public void EndTalk()
    {
        //tutoNum 초기화 
        talkNums = 0;
        Debug.Log("자막 끝!");
        isOn = true;
        //Invoke("LoadScene", 3.5f);  

            //isOn = !isOn;
            if(isOn)
            {
                TBCPanel.SetActive(true);
                //theAudio.Play(call_sound);
            }
    }
      /* public void LoadScene()  
    {
       // AudioManager.instance.PlayBgm(true);
        SceneManager.LoadScene("ToBeContinue"); 
    }*/
}
        
