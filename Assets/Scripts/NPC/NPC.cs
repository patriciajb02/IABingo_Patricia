using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Cada NPC tiene su propio diálogo
    [SerializeField] private DialogoSO miDialogo;

    // Canvas con la "F"
    [SerializeField] private Canvas localCanvasF;

    // Evento global para avisar al sistema de diálogo
    public static event Action<DialogoSO> OnEntrarEnRango;

    private bool jugadorEnRango = false;
    private bool noDejarInteractuarDeNuevo = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // 🔴 PROTECCIÓN: si no hay diálogo, avisamos
        if (miDialogo == null)
        {
            return;
        }

        jugadorEnRango = true;

        // Avisamos al sistema de diálogo QUÉ diálogo usar
        OnEntrarEnRango?.Invoke(miDialogo);

        // Mostramos la F
        localCanvasF.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        jugadorEnRango = false;
        localCanvasF.enabled = false;
    }

    private void OnEnable()
    {
        // Escuchamos la tecla F
        FirstPersonController.OnHablar += IntentarHablar;
    }

    private void OnDisable()
    {
        FirstPersonController.OnHablar -= IntentarHablar;
    }

    // =================================================
    // SE LLAMA CUANDO EL JUGADOR PULSA F
    // =================================================
    private void IntentarHablar()
    {
        if (!jugadorEnRango)
            return;

        if (noDejarInteractuarDeNuevo == false)
        {
            // Iniciamos el diálogo SOLO cuando se pulsa F
            SistemaDialogo.instance.IniciarDialogo();
            
            noDejarInteractuarDeNuevo = true;
            
        }
        
    }
    
}