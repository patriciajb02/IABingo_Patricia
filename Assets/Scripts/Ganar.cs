using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ganar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ganar"))
        {
            SceneManager.LoadScene("Ganar");

        }
        
    }
}
