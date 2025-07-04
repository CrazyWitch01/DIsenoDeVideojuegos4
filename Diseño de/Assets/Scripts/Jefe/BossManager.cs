using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugManager;

public class BossManager : MonoBehaviour
{
    [SerializeField] BossData bossData;
    [SerializeField] Animator animator;
    [SerializeField] GameObject fuegoMoradoPrefab;

    public int numFuegoMorado = 6; // Cantidad de bolas de fuego morado
    public float espaciado = 1f; // Espacio entre cada bola de fuego
    public float spawnOffset = 0f; // Posición inicial de la línea de bolas de fuego
    public Vector2 spawnDireccion = Vector2.right;
    public float duracionInvulnerabilidad = 1f;
    public GameObject Win;

    public GameObject trian;
    public GameObject tentaculo;
    public GameObject jugador;
    public float intervalo = 1f;

    private Collider2D bossCollider;

    void Start()
    {
        bossData.RestablecerStatsJefe();
        bossCollider = GetComponent<Collider2D>();
        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        if (bossData.vidaJefe <= 0)
        {
            animator.SetTrigger("Morido");
            Debug.Log("ACTIVADO");
            Transform[] allChildren = GameObject.Find("UIs").GetComponentsInChildren<Transform>(true); // true = incluye inactivos
            foreach (Transform t in allChildren)
            {
                if (t.name == "Win")
                {
                    Win = t.gameObject;
                    break;
                }
            }
            Win.SetActive(true);

            //animator.SetBool("muerte", false);

            // Debug.Log("DESACTIVADO");
        }
    }

    public void Muertado()
    {
        //animator.SetBool("muerte",false);
    }
    public void DispararFuegoMorado()
    {
        for (int i = 0; i < numFuegoMorado; i++)
        {
            Vector2 spawnPosition = (Vector2)transform.position + spawnDireccion.normalized * (spawnOffset + i * espaciado);
            Instantiate(fuegoMoradoPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void AtaqueEnArea()
    {
        StartCoroutine(InstanciarTentaculos());
    }

    public void TerminarAtaqueEnArea()
    {
        StopCoroutine(InstanciarTentaculos());
        animator.SetTrigger("terminacion");
        //animator.SetBool("terminarArea", false);
    }

    public void InstanciarTrian()
    {
        trian.SetActive(true);
    }

    public void DesactivarTrian()
    {
        trian.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (bossCollider.enabled && other.CompareTag("Espada"))
        {
            bossData.QuitarVidaJefe();
            StartCoroutine(ActivarCollider());
        } 
    }
    IEnumerator ActivarCollider()
    {
        if (bossCollider != null)
        {
            bossCollider.enabled = false;
            Debug.Log("Collider del jefe desactivado.");

            yield return new WaitForSeconds(duracionInvulnerabilidad);

            bossCollider.enabled = true;
            Debug.Log("Collider del jefe reactivado.");
        }
    }

    IEnumerator InstanciarTentaculos()
    {
        {
            Instantiate(tentaculo, jugador.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(intervalo);
            //animator.SetBool("terminarArea", true);
            StopCoroutine(InstanciarTentaculos());
        }
    }
}
