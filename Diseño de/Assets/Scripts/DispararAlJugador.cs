using UnityEngine;
using UnityEngine.AI;

public class DispararAlJugador : MonoBehaviour
{
    public float radioDeteccion;
    public LayerMask capaJugador;
    public Transform player;
    public bool jugadorDetectado;
    public GameObject balaEnemigo;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        GameObject bullet = GameObject.FindWithTag("Bullets");
        if (bullet != null)
        {
            balaEnemigo = bullet;
        }
    }

    private void Update()
    {
        jugadorDetectado = Physics2D.Raycast(this.transform.position, player.position, radioDeteccion, capaJugador);

        if (jugadorDetectado) { Disparar(); }
    }

    private void Disparar()
    {
        Instantiate(balaEnemigo, this.transform.position, this.transform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, radioDeteccion);
    }
}
