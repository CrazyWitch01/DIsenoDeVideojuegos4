using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMain : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;

    private Rigidbody2D rb;
    private Vector2 inputMovimiento;
    private InputSystem_Actions input;

    // Update is called once per frame
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new InputSystem_Actions();
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
        if (inputMovimiento.x < 0)
        {
          
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (inputMovimiento.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
