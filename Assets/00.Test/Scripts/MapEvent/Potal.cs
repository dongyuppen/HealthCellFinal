
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Potal : MonoBehaviour
{
    public Image fadePanel;
    float time = 0f;
    float Fadetime = 1f;
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
            StartCoroutine(FadeOutIn());
        }
    }
    IEnumerator FadeOutIn()
    {
        fadePanel.gameObject.SetActive(true);
        Color alpha = fadePanel.color;
        while(alpha.a < 1f)
        {
            time += Time.deltaTime / Fadetime;
            alpha.a = Mathf.Lerp(0, 1, time);
            fadePanel.color = alpha;
            yield return null;
        }
        time = 0f;
        Debug.Log("2��ó���Ϸ�");

        StageMgr.Instance.NextStage();
        // StageMgr ������Ʈ�� NextStage �Լ� ȣ��
        yield return new WaitForSeconds(1f);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / Fadetime;
            alpha.a = Mathf.Lerp(1, 0, time);
            fadePanel.color = alpha;
            yield return null;
        }
        fadePanel.gameObject.SetActive(false);
        Debug.Log("3��ó���Ϸ�");
        yield return null;
    }
}
