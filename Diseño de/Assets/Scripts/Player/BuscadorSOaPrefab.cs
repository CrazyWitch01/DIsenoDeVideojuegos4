using UnityEngine;

public class BuscadorSOaPrefab : MonoBehaviour
{
    private PlayerMain playerMain;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameObject playerControllerGO = GameObject.Find("PlayerController");

        if (playerControllerGO != null)
        {
            playerMain = playerControllerGO.GetComponent<PlayerMain>();

            if (playerMain != null)
            {
                Debug.Log("PlayerController encontrado y componente PlayerMain asignado correctamente.");
                // Aqu� puedes acceder a playerMain.vidaJugador, etc.
            }
            else
            {
                Debug.LogWarning("El GameObject 'PlayerController' no tiene el componente PlayerMain.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontr� ning�n GameObject llamado 'PlayerController' en la escena.");
        }
    }
}
