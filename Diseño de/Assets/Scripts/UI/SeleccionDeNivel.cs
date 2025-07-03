using UnityEngine;

public class SeleccionDeNivel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SeleccionarSpawn(int spawnIndex)
    {
        PlayerPrefs.SetInt("SpawnSeleccionado", spawnIndex);
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
