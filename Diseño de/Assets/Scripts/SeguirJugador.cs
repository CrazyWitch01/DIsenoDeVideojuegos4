using UnityEngine;
using UnityEngine.AI;

public class SeguirJugador : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public float fixedZPosition = -2f;

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
}
