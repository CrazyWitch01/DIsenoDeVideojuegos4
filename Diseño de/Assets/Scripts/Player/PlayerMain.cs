using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMain : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;

    public int vidaJugador;

    public GameObject Capa;
    private Animator animatorCapa;


    private Rigidbody2D rb;
    private Vector2 inputMovimiento;
    private InputSystem_Actions input;

    // Update is called once per frame
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new InputSystem_Actions();
        animatorCapa = Capa.GetComponent<Animator>();
    }

    void OnEnable()
    {
        input.NewPlayer.Enable();
        input.NewPlayer.Move.performed += OnMove;
        input.NewPlayer.Move.canceled += OnMove;
    }
    void OnDisable()
    {
        input.NewPlayer.Move.performed -= OnMove;
        input.NewPlayer.Move.canceled -= OnMove;
        input.NewPlayer.Disable();
    }

    void OnMove(InputAction.CallbackContext context)
    {
        inputMovimiento = context.ReadValue<Vector2>().normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + inputMovimiento * velocidad * Time.fixedDeltaTime);
        if (inputMovimiento.x > 0)
        {
          
            Capa.transform.rotation = Quaternion.Euler(0,0,-90f);
        }
        else if (inputMovimiento.x < 0)
        {
            Capa.transform.rotation = Quaternion.Euler(0,0,90f);
        }
        if (inputMovimiento.y > 0)
        {

            Capa.transform.rotation = Quaternion.Euler(0, 0, 0f);
        }
        else if (inputMovimiento.y < 0)
        {
            Capa.transform.rotation = Quaternion.Euler(0, 0, 180f);
        }

        bool IsMoving = inputMovimiento != Vector2.zero;
        animatorCapa.SetBool("IsMoving", IsMoving);
    }

    public void PerderVida(int Valor)
    {
        vidaJugador -= Valor;
        if (vidaJugador <= 0)
        {
            //Destroy(this);
        }
    }
}
