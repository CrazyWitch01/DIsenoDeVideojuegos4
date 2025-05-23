using UnityEngine;

public class SerializacionJson : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cuarto cuarto = new Cuarto();
        cuarto.NombreCuarto = "SALA";

        string data = JsonUtility.ToJson(cuarto);
        Debug.Log(data);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
