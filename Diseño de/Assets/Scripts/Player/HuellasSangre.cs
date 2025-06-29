using UnityEngine;

public class HuellasSangre : MonoBehaviour
{
    public GameObject PrefabHuella;
    public GameObject PrefabHuellaVeneno;
    public float huellasOffset = 0.4f;
    public int CuantasHuellas = 4;
    public float huellasOffsetLateral = 0.4f;

    private int huellasRestantes= 0;
    private Vector2 ultimaPosicion;
    private int pasoActual= 0;
    private Vector2 ultimaDireccion = Vector2.right;

    private GameObject prefabHuellaActual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (huellasRestantes > 0)
        {
            Vector2 posicionActual = transform.position;
            float distancia = Vector2.Distance(posicionActual, ultimaPosicion);

            if (distancia >= huellasOffset)
            {
                Vector2 direccion = (posicionActual -  ultimaPosicion).normalized;
                if (direccion.magnitude > 0.1f)
                {
                    ultimaDireccion = direccion;


                    ColocarHuella(posicionActual, ultimaDireccion);
                    ultimaPosicion = posicionActual;
                    huellasRestantes--;
                }
            }
        }
    }

    public void ActivarHuellas(GameObject prefab)
    {
        huellasRestantes = CuantasHuellas;
        ultimaPosicion = transform.position;
        prefabHuellaActual = prefab;
    }

    private void ColocarHuella(Vector2 posicion, Vector2 direccion)
    {
        Vector2 perpendicular = Vector2.Perpendicular(direccion).normalized;


        float lado = pasoActual == 0 ? -1f : 1f;
        pasoActual = 1 - pasoActual;

        Vector2 posicionHuella = posicion + perpendicular * huellasOffsetLateral * lado;

        Quaternion rotacion = Quaternion.LookRotation(Vector3.forward, direccion);

        Instantiate(prefabHuellaActual, posicionHuella, rotacion);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BloodStain"))
        {
            ActivarHuellas(PrefabHuella);
        }

        if(collision.CompareTag("VenenoStain"))
        {
            ActivarHuellas(PrefabHuellaVeneno);
        }
    }
}
