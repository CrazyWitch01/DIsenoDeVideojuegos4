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
    public GameObject Capa;
    public CameraEffects PostManager;
    public bool Invulnerable = false;
    public float DuracionInvulnerabilidad = 1f;
    public GameObject PlayerController;
    public GameObject UILose;
    private AudioSource Musica;
    public AudioClip ruidoBlanco;
    public GameObject UIPlayer;

    private int _lastExp;
    private int _lastVida;
    private float _lastVel;
    private int _lastAtq;
    private Animator animatorCapa;
    private Rigidbody2D rb;
    private Vector2 inputMovimiento;
    private InputSystem_Actions input;
    private float tiempoDesdeUltimoDaño = 0f;
    private bool regenerando = false;
    private bool envenenado = false;

    [SerializeField] AudioSource FuenteAudio;
    [SerializeField] AudioSource PasosSource;
    [SerializeField] AudioClip PasosAudio;
    [SerializeField] AudioClip SonidoHurt;
    [SerializeField] AudioClip SonidoVeneno;
    [SerializeField] PlayerData playerData;
    [SerializeField] ItemData itemData;
    /* Veneno */ [SerializeField] private SpriteRenderer[] spritesVeneno;
    private bool Caminando = false;
    private Coroutine pasosCoroutine;
    public float intervaloPasos = 0.4f;


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

        if (IsMoving && !Caminando)
        {
            Caminando = true;
            pasosCoroutine = StartCoroutine(ReproducirPasos());
        }
        else if (!IsMoving && Caminando)
        {
            Caminando = false;
            if (pasosCoroutine != null)
                StopCoroutine(pasosCoroutine);
        }

        animatorCapa.SetBool("IsMoving", IsMoving);


    }
    private IEnumerator ReproducirPasos()
    {
        while (true)
        {
            if (PasosSource != null && PasosAudio != null)
            {
                PasosSource.PlayOneShot(PasosAudio);
            }
            yield return new WaitForSeconds(intervaloPasos);
        }
    }
    public void PerderVida()
    {
        if (Invulnerable)
        {
            return;
        }

        
        Invulnerable = true;
        FuenteAudio.PlayOneShot(SonidoHurt);
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
            /*GameObject mainCameraObj = GameObject.Find("MainCamera");
            AudioListener listener = mainCameraObj.GetComponent<AudioListener>();
            mainCameraObj.enabled = false;
            */
            GameObject musicaObj = GameObject.Find("Musica");
            AudioSource Musica = musicaObj != null ? musicaObj.GetComponent<AudioSource>() : null;
            Musica = GameObject.Find("Musica")?.GetComponent<AudioSource>();

            Musica.Stop();
            Musica.PlayOneShot(ruidoBlanco);

            Transform[] allChildren = GameObject.Find("UIs").GetComponentsInChildren<Transform>(true); // true = incluye inactivos
            foreach (Transform t in allChildren)
            {
                if (t.name == "Lose")
                {
                    UILose = t.gameObject;
                    break;
                }
            }
            UILose.SetActive(true);

            GameObject UIPlayer = GameObject.Find("UIPlayer");
            UIPlayer.SetActive(false);

            
            Destroy(PlayerController);
            
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
        } 
        else if (collision.CompareTag("Item2"))
        {
                playerData.AumentarVelJugador();
                PostManager.ActivarEfectoCura();
                Destroy(collision.gameObject);
        }
       // else if (collision.CompareTag("DañoPlayer"))
       // {
            //PerderVida();
        //}
        else if (collision.CompareTag("VenenoStain") && !envenenado)
        {
            FuenteAudio.PlayOneShot(SonidoVeneno);
            StartCoroutine(EfectoVeneno());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("DañoPlayer"))
        {
            PerderVida();
        }
    }
    private IEnumerator EfectoVeneno()
    {
        envenenado = true;

        int ticks = 0;
        float parpadeoRate = 0.2f;
        float totalTime = 3f;
        float timer = 0f;
        bool verde = false;

        // Lanzar en paralelo dos rutinas: parpadeo y daño
        StartCoroutine(ParpadeoVeneno());

        while (ticks < 3)
        {
            if (playerData.vidaPlayer > 1)
            {
                PerderVida();
            }

            ticks++;
            yield return new WaitForSeconds(1f);
        }

        envenenado = false;
    }
    private IEnumerator ParpadeoVeneno()
    {
        float tiempo = 0f;
        bool verde = false;

        while (tiempo < 3f)
        {
            Color color = verde ? Color.white : Color.green;
            foreach (var sr in spritesVeneno)
            {
                if (sr != null)
                    sr.color = color;
            }
            verde = !verde;

            tiempo += 0.2f;
            yield return new WaitForSeconds(0.2f);
        }

        foreach (var sr in spritesVeneno)
        {
            if (sr != null)
                sr.color = Color.white;
        }
    }
}
