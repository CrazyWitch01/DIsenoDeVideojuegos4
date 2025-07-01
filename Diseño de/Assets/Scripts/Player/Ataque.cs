using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ataque : MonoBehaviour
{
    private InputSystem_Actions input;
    public GameObject espadaContainer;
    public Animator EspadaAnimator;
    public GameObject Espada;
    public GameObject Pistola;
    public AudioSource AudiosAtaques;
    public AudioClip[] AudiosEspada;

    public float tiempoEspera = 1.5f;
    public float DisparoEnMano = 1f;
    private Coroutine DisableEspadaCoroutine;
    private Coroutine DisableDisparoCoroutine;


    //DISPARO

    public GameObject BulletPlayerPrefab;
    public Transform BalaOrigen;
    public Transform CabezaRotacion;
    public float BalaSpeed = 10f;
    public float _cooldownDisparo = 5f;
    private bool puedeDisparar = true;
    public AudioClip SonidoPistolaLista;

    void Awake()
    {
        input = new InputSystem_Actions();
    }

    void OnEnable()
    {
        input.NewPlayer.Enable();
        input.NewPlayer.Ataques.performed += OnAtaques; //melee
        input.NewPlayer.Disparo.performed += OnDisparo;//disparo
    }

    void OnDisable()
    {
        input.NewPlayer.Ataques.performed -= OnAtaques;
        input.NewPlayer.Disparo.performed -= OnDisparo;

        input.NewPlayer.Disable();
    }

    void OnAtaques(InputAction.CallbackContext context)
    {
        espadaContainer.SetActive(true);
        Espada.SetActive(true);
        int index = Random.Range(0,AudiosEspada.Length);
        AudiosAtaques.clip= AudiosEspada[index];
        AudiosAtaques.Play();
        EspadaAnimator.SetTrigger("Click");


        if (DisableEspadaCoroutine != null)
        {
            StopCoroutine(DisableEspadaCoroutine);
        }

        DisableEspadaCoroutine = StartCoroutine(DisableEspadaCooldown());
    }

    private IEnumerator DisableEspadaCooldown()
    {
        yield return new WaitForSeconds(tiempoEspera);

        if (Espada != null)
        {
            Espada.SetActive(false);
        }
    }

    public void ActivarEspada()
    {
        Espada.SetActive(true);
    }

    public void DesactivarEspada()
    {
        Espada.SetActive(false);
    }

    void OnDisparo(InputAction.CallbackContext context)
    {
        if (!puedeDisparar) return;
        puedeDisparar = false;

        Pistola.SetActive(true);
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;

        Vector2 direccion = (mouseWorldPos - BalaOrigen.position).normalized;


        Quaternion rotacion = Quaternion.Euler(0f, 0f, CabezaRotacion.eulerAngles.z);
        GameObject bala = Instantiate(BulletPlayerPrefab, BalaOrigen.position, rotacion);

        Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direccion*BalaSpeed;




        if (DisableDisparoCoroutine != null)
        {
            StopCoroutine(DisableDisparoCoroutine);
        }

        DisableDisparoCoroutine = StartCoroutine(DisableDisparoCooldown());
        StartCoroutine(CooldownDisparoCoroutine());
    }

    private IEnumerator DisableDisparoCooldown()
    {
        yield return new WaitForSeconds(DisparoEnMano);

        if (Pistola != null)
        {
            Pistola.SetActive(false);
        }
    }
    private IEnumerator CooldownDisparoCoroutine()
{
    yield return new WaitForSeconds(_cooldownDisparo);

    puedeDisparar = true;

    if (SonidoPistolaLista != null)
    {
        AudiosAtaques.PlayOneShot(SonidoPistolaLista); 
    }
}
    public void ActivarPistola()
    {
        Pistola.SetActive(true);
    }

    public void DesactivarPistola()
    {
        Pistola.SetActive(false);
    }
}

