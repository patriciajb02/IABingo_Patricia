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
        //👋😔Fin del Singleton
        
    private void OnEnable()
    {
            //Para que avise si el jugador entra en rango
        NPC.OnEntrarEnRango += EntrarEnRango;
        
    }

    private void OnDisable()
    {
        NPC.OnEntrarEnRango -= EntrarEnRango;
        
    }

        //Para que solo pueda interactuar con el NPC si entra en su SphereCollider
    private void EntrarEnRango(DialogoSO dialogo)
    {
        enRango = true;
        dialogoActual = dialogo;
        
    }
    
    public void IniciarDialogo()
    {
        if (!enRango || dialogoActual == null) return;

        fraseActual = 0;
        marcoDialogo.SetActive(true);
        StartCoroutine(EscribirFrase());
        
    }
    
    public void SiguienteFrase()
    {
        if (!enRango || dialogoActual == null) return;

        if (!escribiendo)
        {
            fraseActual++;

            if (fraseActual >= dialogoActual.frases.Length)
                TerminarDialogo();
            
            else
                StartCoroutine(EscribirFrase());
            
        }
        else
        {
            CompletarFrase();
            
        }
        
    }
    
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
    
    private void CompletarFrase()
    {
            //Parar la corrutina q escribe palabra a palabra
        StopAllCoroutines();

            //Asigna directamente al texto la frase completa actual,
            // sin animación ni escritura progresiva.
        texto.text = dialogoActual.frases[fraseActual];

            //Indica que ya NO se está escribiendo la frase,
            // para permitir avanzar a la siguiente o evitar
            // que se vuelva a llamar a CompletarFrase.
        escribiendo = false;
        
    }
    
    private void TerminarDialogo()
    {
        marcoDialogo.SetActive(false);
        fraseActual = 0;
        dialogoActual = null;
        enRango = false;

        OnDialogoTerminado?.Invoke();
        
    }
}
