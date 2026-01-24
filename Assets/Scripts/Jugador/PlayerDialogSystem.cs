using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDialogSystem : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

    }

    private void OnEnable()
    {
        playerInput.actions["Interactuar"].started += LanzarInteraccion;
        SistemaDialogo.instance.OnDialogoTerminado += AvisoDialogoTerminado;
        
    }

    private void AvisoDialogoTerminado()
    {
        CambiarEstadoInputs("MainGame");
        
    }

    private void OnDisable()
    {
        playerInput.actions["Interactuar"].started -= LanzarInteraccion;
        SistemaDialogo.instance.OnDialogoTerminado -= AvisoDialogoTerminado;
    }

    private void LanzarInteraccion(InputAction.CallbackContext obj)
    {
        SistemaDialogo.instance.SiguienteFrase();
        
    }

    public void CambiarEstadoInputs(string nombreMapaInputs)
    {
        //Cambia el mapa que tengas activo a otro nuevo
        playerInput.SwitchCurrentActionMap(nombreMapaInputs);

        
    }
    
}
