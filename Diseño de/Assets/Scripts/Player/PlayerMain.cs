using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMain : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;

    public PlayerData dataPlayer;
    public int vidaJugador;
    public float velJugador;
    public int atqJugador;
    public int experienciaJugador;
    
    private int _lastExp;
    private int _lastVida;
    private float _lastVel;
    private int _lastAtq;

    public GameObject Capa;
    private Animator animatorCapa;

    public CameraEffects PostManager;

    private Rigidbody2D rb;
    private Vector2 inputMovimiento;
    private InputSystem_Actions input;


    // Update is called once per frame
    void Start()
    {
        if (dataPlayer != null)
        {
            AplicarDatos(dataPlayer);

            // Guardar valores iniciales
            _lastVida = dataPlayer.vidaPlayer;
            _lastVel = dataPlayer.velPlayer;
            _lastAtq = dataPlayer.atqPlayer;
        }
    }

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
        if (PostManager != null)
        {
            PostManager.ActivarEfectoSangre();
        }
        if (vidaJugador <= 0)
        {
            //Destroy(this);
        }
    }
    void Update()
    {
        if (dataPlayer != null)
        {
            if (_lastVida != dataPlayer.vidaPlayer ||
                _lastVel != dataPlayer.velPlayer ||
                _lastAtq != dataPlayer.atqPlayer ||
                _lastExp != dataPlayer.experienciaActual)
            {
                Debug.Log("Datos cambiaron, se actualiza jugador");
                AplicarDatos(dataPlayer);

                // Actualizar valores cacheados
                _lastVida = dataPlayer.vidaPlayer;
                _lastVel = dataPlayer.velPlayer;
                _lastAtq = dataPlayer.atqPlayer;
                _lastExp = dataPlayer.experienciaActual;
            }
        }
    }

    public void AplicarDatos(PlayerData data)
    {
       vidaJugador = data.vidaPlayer;
       velJugador = data.velPlayer;
       atqJugador = data.atqPlayer;
       experienciaJugador = data.experienciaActual;

        velocidad = data.velPlayer; // si estás usando `velocidad` para moverse

       Debug.Log("Datos del jugador aplicados desde PlayerData");
     }
}
