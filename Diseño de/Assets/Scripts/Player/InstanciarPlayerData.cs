using UnityEngine;

public class InstanciarPlayerData : MonoBehaviour
{
    public Transform[] spawnPoints; // Asignar manualmente en Inspector
    public PlayerData data;
    public string camaraTag = "CineMachineMainCamera";

    void Awake()
    {
        int spawnIndex = PlayerPrefs.GetInt("SpawnSeleccionado", 0);
        spawnIndex = Mathf.Clamp(spawnIndex, 0, spawnPoints.Length - 1);

        Transform spawnTransform = spawnPoints[spawnIndex];

        // Instanciar jugador
        GameObject player = Instantiate(data.prefab, spawnTransform.position, Quaternion.identity);
        PlayerMain pm = player.GetComponent<PlayerMain>();
        if (pm != null)
        {
            pm.AplicarDatos(data);
        }

        // Mover cámara
        GameObject cam = GameObject.FindGameObjectWithTag(camaraTag);
        if (cam != null)
        {
            Vector3 camPos = new Vector3(spawnTransform.position.x, spawnTransform.position.y, cam.transform.position.z);
            cam.transform.position = camPos;
        }
    }
}

