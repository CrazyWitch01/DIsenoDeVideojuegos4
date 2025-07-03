using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class BotonLogica : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject botonSiguiente; // Asigna este desde el Inspector

    public void CambiarVista()
    {
        
        StartCoroutine(SeleccionarBotonAlFrameSiguiente());
    }

    private IEnumerator SeleccionarBotonAlFrameSiguiente()
    {
        yield return null; // espera 1 frame
        EventSystem.current.SetSelectedGameObject(null); // limpia selección actual
        EventSystem.current.SetSelectedGameObject(botonSiguiente); // asigna el nuevo
    }
}

