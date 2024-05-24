using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3Camera : MonoBehaviour
{
    public GameObject subCam;
    public GameObject realCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            realCam.SetActive(false);
            subCam.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            realCam.SetActive(true);
            subCam.SetActive(false);
        }
    }
}
