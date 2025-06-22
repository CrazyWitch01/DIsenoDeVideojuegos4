using UnityEngine;
using UnityEngine.AI;

public class DispararAlJugador : MonoBehaviour
{
    public float radioDeteccion;
    public LayerMask capaJugador;
    public LayerMask capaObstaculos;
    public Transform player;
    public bool jugadorDetectado;

    //DISPARO
    public GameObject balaEnemigo;
    private float TiempoDisparo = 0f;
    public float DisparoCooldown = 1f;
    private float VelocidadBala = 7.5f;

    public Animator Animator;



    void Awake()
    {
        GameObject obj = GameObject.Find("PlayerHurtbox");
        if (obj != null)
        {
            player = obj.transform;
        }
        else
        {
            Debug.Log("No hay player Hurtbox en Scene");
        }
    }
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
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radioDeteccion, capaJugador); //nuevo metodo para que el lanzar el detectar al player funcione correctamente

        if (hit != null)
        {
            Vector2 direccion = (player.position - transform.position).normalized;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, direccion, radioDeteccion, capaObstaculos); //genera un rayo que toma en cuenta la posicion dentro del
                                                                                                                 //radio de deteeccion y si hay un objeto en la capa Obstaculos
            if (ray.collider == null)
            {
                jugadorDetectado = true;
            }


            if (jugadorDetectado)
            {
                TiempoDisparo += Time.deltaTime;

                if (TiempoDisparo >= DisparoCooldown)
                {
                    Disparar();
                    Debug.Log("JugadorDetectado");
                    TiempoDisparo = 0;
                }
            }
        }
    }

    private void Disparar()
    {
        Vector2 direccion = (player.position - transform.position).normalized;
        Animator.SetTrigger("IsAttack");
        Debug.Log("EstoyDisparando");
        float anguloBala = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        Quaternion rotacion = Quaternion.Euler(0, 0, anguloBala); //toma la diferencia de posiciones entre el player y
                                                                  //el gameobject padre y crea una rotacion con el angulo que sale

        GameObject balaActual = Instantiate(balaEnemigo, transform.position, rotacion);
        Destroy(balaActual, 2f); //declaro la bala como bala actual para luego poder destruirla

        // Agrega el componente MovimientoBala directamente y le pasa la dirección y velocidad
        MovimientoBala mover = balaActual.AddComponent<MovimientoBala>();
        mover.direccion = direccion;
        mover.velocidad = VelocidadBala;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, radioDeteccion);
    }

    // Script interno para mover la bala automáticamente
    public class MovimientoBala : MonoBehaviour
    {
        public Vector2 direccion;
        public float velocidad = 7.5f;

        void Update()
        {
            transform.Translate(direccion.normalized * velocidad * Time.deltaTime, Space.World);
        }
    }
}