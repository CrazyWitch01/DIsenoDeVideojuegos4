using UnityEngine;
using System.Collections;

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

    private Collider2D bossCollider;

    void Start()
    {
        bossData.RestablecerStatsJefe();
        bossCollider = GetComponent<Collider2D>();
    }

    void Update()
    {

    }
    public void DispararFuegoMorado()
    {
        for (int i = 0; i < numFuegoMorado; i++)
        {
            Vector2 spawnPosition = (Vector2)transform.position + spawnDireccion.normalized * (spawnOffset + i * espaciado);
            Instantiate(fuegoMoradoPrefab, spawnPosition, Quaternion.identity);
        }
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
}
