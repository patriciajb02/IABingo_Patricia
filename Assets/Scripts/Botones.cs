using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        
    }

    public void Reintentar()
    {
        SceneManager.LoadScene("Bingojuego");
        
    }
    
}
