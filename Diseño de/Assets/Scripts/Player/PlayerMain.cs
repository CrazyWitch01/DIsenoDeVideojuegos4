using System.Collections;
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
    private Rigidbody2D rb;
    private Vector2 inputMovimiento;
    private InputSystem_Actions input;


    public CameraEffects PostManager;
    [SerializeField] PlayerData playerData;
    [SerializeField] ItemData itemData;
    public bool Invulnerable = false;
    public float DuracionInvulnerabilidad = 1f;
    private float tiempoDesdeUltimoDaño = 0f;
    private bool regenerando = false;
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

        playerData.ReiniciarStats();
    }

    void Awake()
    {
        PostManager = Camera.main.GetComponent<CameraEffects>();
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

    public void PerderVida()
    {
        if (Invulnerable)
        {
            return;
        }

        Invulnerable = true;
        StartCoroutine(Invulnerabilidad());
        //Resta vida al jugador
        tiempoDesdeUltimoDaño = 0f; 
        regenerando = false;
        playerData.RestarVidaJugador();

        //Activa el efecto de sangre xd
        PostManager.ActivarEfectoSangre();

        //Verifica la vida para destruir al jugador en caso llegue a 0
        if (playerData.vidaPlayer <= 0)
        {
            //Destroy(this);
        }
    }

    private IEnumerator Invulnerabilidad()
    {
        yield return new WaitForSeconds(DuracionInvulnerabilidad);
        Invulnerable = false;

    }

    private IEnumerator RegenerarVida()
    {
        regenerando = true;
        while (playerData.vidaPlayer < playerData.vidaMaxima)
        {
            playerData.vidaPlayer++;
            yield return new WaitForSeconds(1f);

            if(tiempoDesdeUltimoDaño<5f)
            {
                regenerando = false;
                yield break;
            }
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
            tiempoDesdeUltimoDaño += Time.deltaTime;
            if (tiempoDesdeUltimoDaño >= 5f && !regenerando && playerData.vidaPlayer < playerData.vidaMaxima)
            {
                StartCoroutine(RegenerarVida());
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
               playerData.AumentarVidaJugador();
               PostManager.ActivarEfectoCura();
                Destroy(collision.gameObject);
        } else if (collision.CompareTag("Item2"))
        {
                playerData.AumentarVelJugador();
                PostManager.ActivarEfectoCura();
                Destroy(collision.gameObject);
        }
        if (collision.CompareTag("DañoPlayer"))
        {
            PerderVida();
        }
    }
}
