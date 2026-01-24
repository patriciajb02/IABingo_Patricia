using System;
using UnityEngine;
using UnityEngine.InputSystem;

//⚠️SI PONES ESTO es para que cuando acoples el script
    // dentro de un gameObject le obliga a poner el CharacterController
    //Si al lado de CharacterController una coma puedes seguir añadiendo requerimientos
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public static event Action OnHablar;
    
    [Header("Movement")]
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float detectionRadius;

    private CharacterController controller;
    private Camera cam;
    private PlayerInput playerInput;

    Vector2 movementInput;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

        //SE ACTIVA CUANDO SE ACTIVA EL GAMEOBJECT
    private void OnEnable()
    {
        playerInput.actions["Move"].performed += UpdateMovement;
        playerInput.actions["Move"].canceled += UpdateMovement;
        playerInput.actions["Interactuar"].performed += OnInteractuar;


    }

    private void OnInteractuar(InputAction.CallbackContext obj)
    {
        OnHablar?.Invoke();
        Debug.Log("Interactuar");
        
    }

    private void UpdateMovement(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
        
    }

    void Update()
    {
        MoveAndRotate(); 
        
    }

    private void MoveAndRotate()
    {
            //Se aplica al cuerpo la rotación que tenga la cámara.
        transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);

            //Si hay input...
        if (movementInput.sqrMagnitude > 0)
        {
                //Se calcula el ángulo en base a los inputs
                //Dame el ángulo al cual la cámara está mirando y ese ángulo va a ser mi frontal
            float angleToRotate = Mathf.Atan2(movementInput.x, movementInput.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

            //Se rota el vector (0, 0, 1) a dicho ángulo
            Vector3 movementVector = Quaternion.Euler(0, angleToRotate, 0) * Vector3.forward;
            
            controller.Move(movementVector * movementSpeed * Time.deltaTime);
            
        }
            //El enemigo va a detectar el sonido por la velocidad a la que vamos
            //Esta es la velocidad en el plano de movimiento, sin contar caídas o saltos (cargándote la y)
        Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
            //SE PONE MAGNITUDE PORQUE ES LA LONGITUD DEL VECTOR, o sea la cantidad de velocidad en este caso
        //Debug.Log(controller.velocity.magnitude);

    }
    
    private void OnDisable()
    {
            //SE PONE -= para que se "desuscriba" cuando se desabilite y así no genera errores cuando muera
            //❗⚠️ES MUY IMPORTANTE
        playerInput.actions["Move"].performed -= UpdateMovement;
        playerInput.actions["Move"].canceled -= UpdateMovement;
        playerInput.actions["Interactuar"].performed -= OnInteractuar;

        
    }
    
}
