using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] BossData bossData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Choca");
        if (other.CompareTag("Espada"))
        {
            bossData.QuitarVidaJefe();
            Debug.Log("El jefe está perdiendo vida");
        }
    }
}
