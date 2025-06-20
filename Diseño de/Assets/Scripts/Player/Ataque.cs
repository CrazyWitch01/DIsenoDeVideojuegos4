using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ataque : MonoBehaviour
{
    private InputSystem_Actions input;
    public GameObject espadaContainer;
    public Animator EspadaAnimator;
    public GameObject Espada;

    public float tiempoEspera = 1.5f;
    private Coroutine DisableEspadaCoroutine;

    void Awake()
    {
        input = new InputSystem_Actions();
    }

    void OnEnable()
    {
        input.NewPlayer.Enable();
        input.NewPlayer.Ataques.performed += OnAtaques;
    }

    void OnDisable()
    {
        input.NewPlayer.Ataques.performed -= OnAtaques;
        input.NewPlayer.Disable();
    }

    void OnAtaques(InputAction.CallbackContext context)
    {
        espadaContainer.SetActive(true);
        Espada.SetActive(true);

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
}
