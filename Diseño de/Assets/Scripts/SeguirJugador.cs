using UnityEngine;
using UnityEngine.AI;

public class SeguirJugador : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public float fixedZPosition = -2f;
    public GameObject[] bloodEffectParticlesPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        enemy.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && enemy != null)
        {
            // El enemigo sigue al jugador
            enemy.SetDestination(player.position);

            // Setear el eje Z al valor designado
            Vector3 newAgentPosition = transform.position;
            newAgentPosition.z = fixedZPosition;
            transform.position = newAgentPosition;
        }
    }

    
    void LateUpdate()
    {
        //transform.rotation = Quaternion.identity; esto era para que no girara xd
        Vector2 direccion = player.position - transform.position;
        float Ejez = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg -90f; //hace que el angulo de rotacion de z siempre sea mirando al player
        transform.rotation = Quaternion.Euler(0f, 0f, Ejez); //esto lo aplica
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Espada"))
        {
            foreach (GameObject bloodPrefab in bloodEffectParticlesPrefab)
            {
                Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            }
            
            Destroy(gameObject);
        }
    }

}
