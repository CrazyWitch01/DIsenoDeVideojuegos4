using UnityEngine;

public class Bala : MonoBehaviour
{
    public float vel;
    public int dmg;
    public Transform player;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * vel * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMain vidaJugador))
        {
            //vidaJugador.PerderVida(dmg);
        }
    }
}
