using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{

    public GameObject LoadingPanel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SceneChange(2));
    }

    IEnumerator SceneChange(float time)
    {
        yield return new WaitForSeconds(time);
        GoIntroScene();

    }

    // Update is called once per frame
    void GoIntroScene()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
