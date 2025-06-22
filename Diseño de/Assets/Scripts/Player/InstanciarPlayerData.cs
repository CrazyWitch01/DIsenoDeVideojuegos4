using UnityEngine;

public class InstanciarPlayerData : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerData data;

    void Awake()
    {
        if (data.prefab != null)
        {
            GameObject obj = Instantiate(data.prefab, transform.position, Quaternion.identity);

            // Aplicar los datos del ScriptableObject al PlayerMain del prefab
            PlayerMain pm = obj.GetComponent<PlayerMain>();
            if (pm != null)
            {
                pm.AplicarDatos(data);
            }
            else
            {
                Debug.LogWarning("El prefab instanciado no tiene PlayerMain");
            }
        }
    }
}

