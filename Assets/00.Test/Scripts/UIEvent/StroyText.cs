using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;
public class StroyText : MonoBehaviour
{

    
    public TMP_Text tutorialTxt;

    //public Text text;

    string dialogue;

    public String[] tutorialDialogue;

    public String[] dialogues;

    void Start()
    {
        StartTalk(tutorialDialogue);
    }

   IEnumerator Typing (String talk)
   {
    //텍스트를 null 값으로 설정
    tutorialTxt.text = null;
    tutorialTxt.DOText(talk, 3f); // 모든 문자가 출력되는데 걸리는 시간 = 1초 

    // 다음 대사 딜레이 
    yield return new WaitForSeconds(4.0f);
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

    public void EndTalk()
    {
        //tutoNum 초기화 
        talkNum = 0;
        Debug.Log("자막 끝!");
        SceneManager.LoadScene("DreamScene");

        
    }

    /*public static void TMPDOText(TextMeshProUGUI text, float duration) //듀레이션은 대사가 출력되는데 걸리는 시간 
    {
        text.maxVisibleCharacters = 0;
        DOTween.To(X => text.maxVisibleCharacters = (int)X, 0f, text.text.Length, duration);
    }
    
    public TextMeshProUGUI text2;
    */
   
}
