using UnityEngine;

public class FuegoMorado : MonoBehaviour
{
    public float vel = 5f; 
    public float tiempo = 5f;  

    void Start()
    {
        Destroy(gameObject, tiempo);
    }

    void Update()
    {
        transform.Translate(Vector2.down * vel * Time.deltaTime);
    }

   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Pared"))
         {
             Destroy(gameObject);
        }
    }
}