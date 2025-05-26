using System;
using Unity.Cinemachine;
using UnityEngine;

public enum TIPOS_CUARTO
{
    CUARTO_INICIO,
    CUARTO_FACIL_1,
    CUARTO_VACIO,
    CUARTO_UP,
    CUARTO_DOWN,
    CUARTO_LEFT,
    CUARTO_RIGHT,
    CUARTO_DOUBLE_VERTICAL,
    CUARTO_DOUBLE_HORIZONTAL,
    CUARTO_DOWN_RIGHT,
    CUARTO_DOWN_LEFT,
    CUARTO_TOP_RIGHT,
    CUARTO_TOP_LEFT,
    CUARTO_ALL,
    CUARTO_LRD,
    CUARTO_LUR,
    CUARTO_LUD,
    CUARTO_URD,
}

public class AccionCuarto : MonoBehaviour
{
    [SerializeField] TIPOS_CUARTO cuarto_actual;


    public void GenerarCuartoCorrespondiente(Transform padre = null)
    {
        GameObject prefab = null;

        switch (cuarto_actual)
        {
            case TIPOS_CUARTO.CUARTO_UP:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoUp");
                break;
            case TIPOS_CUARTO.CUARTO_DOWN:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoDown");
                break;
            case TIPOS_CUARTO.CUARTO_VACIO:
                break;
            case TIPOS_CUARTO.CUARTO_RIGHT:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoRight");
                break;
            case TIPOS_CUARTO.CUARTO_LEFT:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoLeft");
                break;
            case TIPOS_CUARTO.CUARTO_DOUBLE_VERTICAL:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoDoubleVertical");
                break;
            case TIPOS_CUARTO.CUARTO_DOUBLE_HORIZONTAL:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoDoubleHorizontal");
                break;
            case TIPOS_CUARTO.CUARTO_DOWN_RIGHT:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoDownRight");
                break;
            case TIPOS_CUARTO.CUARTO_DOWN_LEFT:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoDownLeft");
                break;
            case TIPOS_CUARTO.CUARTO_TOP_LEFT:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoUpLeft");
                break;
            case TIPOS_CUARTO.CUARTO_TOP_RIGHT:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoUpRight");
                break;
            case TIPOS_CUARTO.CUARTO_ALL:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoAll");
                break;
            case TIPOS_CUARTO.CUARTO_LRD:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoLRD");
                break;
            case TIPOS_CUARTO.CUARTO_LUR:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoLUR");
                break;
            case TIPOS_CUARTO.CUARTO_LUD:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoLUD");
                break;
            case TIPOS_CUARTO.CUARTO_URD:
                prefab = Resources.Load<GameObject>("Cuartos/ContenedorCuartoURD");
                break;
        }

        if (prefab != null)
        {
            GameObject instancia = Instantiate(prefab, transform.position, Quaternion.identity, padre);
            instancia.name = gameObject.name;

            BoxCollider2D col = instancia.AddComponent<BoxCollider2D>();
            col.isTrigger = true;
            col.size = new Vector2(18, 10);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(18, 10, 1));
    }
}