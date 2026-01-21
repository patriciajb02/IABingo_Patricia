using UnityEngine;

[CreateAssetMenu(fileName = "DialogoSO", menuName = "Scriptable Objects/DialogoSO")]
public class DialogoSO : ScriptableObject
{
    public string hablador;
    [TextArea] public string[] frases;
    public float velocidadTexto;
    public Color color;
    
}
