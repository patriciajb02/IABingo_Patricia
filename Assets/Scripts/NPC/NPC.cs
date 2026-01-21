using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
        //Porque cada NPC tiene su diálogo
    [SerializeField] private DialogoSO miDialogo;
    
    public static event Action<DialogoSO> OnEntrarEnRango;

    public void IniciarConversacion(GameObject playerValue)
    {
            //❗LLAMADA AL SINGLETON
        SistemaDialogo.instance.IniciarDialogo(miDialogo);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnEntrarEnRango?.Invoke(miDialogo);
            
        }
        
    }
    
}
