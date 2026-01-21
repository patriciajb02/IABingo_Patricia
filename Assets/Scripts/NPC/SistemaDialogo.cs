using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SistemaDialogo : MonoBehaviour
{
       [SerializeField] private GameObject marcoDialogo;
    [SerializeField] private TMP_Text texto;
        //Ya no se necesita xd [SerializeField] private DialogoSO dialogo;
        
        //OMG un evento hi 👋
    public event Action OnDialogoTerminado;

    private int fraseActual;
    private bool escribiendo = false;
    private DialogoSO dialogoActual;
    private bool patatudo = false; //⚠️⚠️⚠️CAMBIAR NOMBRE
    
        //PATRÓN SINGLETON (meme de Spiderman señalando)
                //Único en tooodo el proyecto y accesible desde cualquier punto
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
    
    private void EntrarEnRango(DialogoSO dialogo)
    {
        patatudo =  true;
        IniciarDialogo(dialogo);
        
    }

    //[ContextMenu("Inicio")] //Para poder probarlo dentro de la ejecución sin necesidad de quien te vaya a soltar la chapa
    public void IniciarDialogo(DialogoSO dialogo)
    {
        dialogoActual = dialogo;
        marcoDialogo.SetActive(true);
        StartCoroutine(EscribirFrase());

    }
    
    [ContextMenu("Completar Frase")]
    public void SiguienteFrase()
    {
            //Si no estoy escribiendo me escribes la siguiente frase
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
            //Sino pues que complete la frase
        else
        {
            CompletarFrase();
            
        }
        
    }

    private void CompletarFrase()
    {
        StopAllCoroutines();
            //Pongo en el texto la frase completa
        texto.text = dialogoActual.frases[fraseActual];
        escribiendo = false;
    }

    //CORRUTINA (semáforo, me voy a turbomatar)
    private IEnumerator EscribirFrase()
    {
        escribiendo = true;
            //Va caracter a caracter
        string fraseAEscribir = dialogoActual.frases[fraseActual];
        //     //POR SI SE QUIERE QUE SALGA POR PALABRAS Y NO POR LETRAS
        // string[] palabras = fraseAEscribir.Split(" ");
        //     //Y en el foreach en vez de letra sería palabra y poner palabras en vez de fraseTroceada
        char[] fraseTroceada = fraseAEscribir.ToCharArray();
            
            //Color del dialogo
        texto.color = dialogoActual.color;
            //Limpiar la caja de texto por si acaso
        texto.text = "";
        
        foreach (var letra in fraseTroceada)
        {
            texto.text += letra;
                //Espera
            yield return new WaitForSeconds(dialogoActual.velocidadTexto);
            
        }
        
        escribiendo = false;

    }

    private void TerminarDialogo()
    {
            //Se ponía interrogación para saber si hay alguien interesado en el evento, para que no reviente el código
        OnDialogoTerminado?.Invoke();
        
        marcoDialogo.SetActive(false);
            //Para que cuando hables con el NPC de nuevo te lo repita
        fraseActual = 0;
        
    }
    
}
