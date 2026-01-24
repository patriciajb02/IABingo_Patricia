using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SistemaDialogo : MonoBehaviour
{
    [SerializeField] private GameObject marcoDialogo;
    [SerializeField] private TMP_Text texto;

    public event Action OnDialogoTerminado;

    private int fraseActual;
    private bool escribiendo = false;
    private DialogoSO dialogoActual;
    private bool enRango = false;

    //⚠️⚠️SINGLETON
    public static SistemaDialogo instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        NPC.OnEntrarEnRango += EntrarEnRango;
    }

    private void OnDisable()
    {
        NPC.OnEntrarEnRango -= EntrarEnRango;
    }

    // =================================================
    // SE LLAMA CUANDO EL JUGADOR ENTRA EN EL TRIGGER DEL NPC
    // =================================================
    private void EntrarEnRango(DialogoSO dialogo)
    {
        enRango = true;
        dialogoActual = dialogo;
    }

    // =================================================
    // INICIA EL DIÁLOGO (PRIMERA FRASE)
    // =================================================
    public void IniciarDialogo()
    {
        if (!enRango || dialogoActual == null)
            return;

        fraseActual = 0;
        marcoDialogo.SetActive(true);
        StartCoroutine(EscribirFrase());
        
    }

    // =================================================
    // AVANZA FRASES CUANDO PULSAS F
    // =================================================
    public void SiguienteFrase()
    {
        // 🔴 ESTA ES LA PROTECCIÓN QUE EVITA EL ERROR
        if (!enRango || dialogoActual == null)
            return;

        if (!escribiendo)
        {
            fraseActual++;

            if (fraseActual >= dialogoActual.frases.Length)
            {
                TerminarDialogo();
            }
            else
            {
                StartCoroutine(EscribirFrase());
            }
        }
        else
        {
            CompletarFrase();
        }
    }

    // =================================================
    // ESCRIBE LA FRASE CARÁCTER A CARÁCTER
    // =================================================
    private IEnumerator EscribirFrase()
    {
        escribiendo = true;

        texto.color = dialogoActual.color;
        texto.text = "";

        foreach (char letra in dialogoActual.frases[fraseActual])
        {
            texto.text += letra;
            yield return new WaitForSeconds(dialogoActual.velocidadTexto);
        }

        escribiendo = false;
    }

    // =================================================
    // COMPLETA LA FRASE INSTANTÁNEAMENTE
    // =================================================
    private void CompletarFrase()
    {
        StopAllCoroutines();
        texto.text = dialogoActual.frases[fraseActual];
        escribiendo = false;
    }

    // =================================================
    // TERMINA EL DIÁLOGO
    // =================================================
    private void TerminarDialogo()
    {
        marcoDialogo.SetActive(false);
        fraseActual = 0;
        dialogoActual = null;
        enRango = false;

        OnDialogoTerminado?.Invoke();
    }
}
