using UnityEngine;
using UnityEngine.SceneManagement;

public class Perder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Perder"))
            SceneManager.LoadScene("Perder");
        
    }
    
}
