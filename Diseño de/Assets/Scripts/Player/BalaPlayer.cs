using UnityEngine;

public class BalaPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float Duracion = 1.5f;

    void Start()
    {
     Destroy(gameObject, Duracion);    
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemies"))
        {
            Destroy(gameObject);
        }
    }
}
