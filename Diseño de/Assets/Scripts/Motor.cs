using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


public class Motor : MonoBehaviour
{

    [MenuItem("Herramientas/Generar Cuartos")]
    public static void GenerarCuartos()
    {
        // Asegura que el contenedor existe o lo crea
        GameObject contenedor = GameObject.Find("Cuartos");
        if (contenedor == null)
        {
            contenedor = new GameObject("Cuartos");
        }

        // Encuentra todos los objetos con AccionCuarto
        AccionCuarto[] cuartos = GameObject.FindObjectsOfType<AccionCuarto>();

        foreach (var cuarto in cuartos)
        {
            cuarto.GenerarCuartoCorrespondiente(contenedor.transform); 
        }
    }

    

    [MenuItem("Motor/Generar Malla")]
    static void GenerarMalla()
    {
        int Filas = 10;
        int Columnas = 10;
        int Total = Filas * Columnas;

        GameObject padreMalla = new GameObject("Malla");


        for (int i = 0; i < Filas; i++)
        {
            for (int j = 0; j < Columnas; j++)
            {

                GameObject gameObject = new GameObject();
                gameObject.name = $"Cell_{i}_{j}";
                gameObject.transform.position = new Vector2(i*18,j*10);
                gameObject.AddComponent<AccionCuarto>();
                gameObject.transform.parent = padreMalla.transform;

            }
        }

    }

    [MenuItem("Motor/Guardar Data")]
    static void GuardarData()
    {
        Mundo mundo = new Mundo();
        mundo.NombreMundo = "Nivel 1";

        string data = JsonUtility.ToJson(mundo);
        Debug.Log(data);
    }
}
