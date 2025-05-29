using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioCuartos : MonoBehaviour
{
    public float moveDuration = 0.5f;
    [SerializeField] private GameObject enemiesSpawners;
    private List<GameObject> puertas = new List<GameObject>();

    private void Awake()
    {
        // Buscar todas las puertas (activas e inactivas) en la escena
        Transform[] allTransforms = Resources.FindObjectsOfTypeAll<Transform>();
        foreach (Transform t in allTransforms)
        {
            if (t.CompareTag("Puertas") && t.hideFlags == HideFlags.None && t.gameObject.scene.IsValid())
            {
                puertas.Add(t.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject camObj = GameObject.FindGameObjectWithTag("CineMachineMainCamera");
            if (camObj != null)
            {
                StartCoroutine(MoveCamera(camObj.transform));

               
                if (enemiesSpawners != null)
                {
                    enemiesSpawners.SetActive(true);
                    StartCoroutine(CheckEnemiesAndDestroySpawner());
                }
            }
            else
            {
                Debug.Log("Camara no encontrada, tagearla como CineMachineMainCamera");
            }
        }
    }

    private IEnumerator CheckEnemiesAndDestroySpawner()
    {
        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");

            if (enemies.Length == 0)
            {
                foreach (GameObject puerta in puertas)
                {
                    if (puerta != null)
                        puerta.SetActive(false);
                }

                if (enemiesSpawners != null)
                {
                    Destroy(enemiesSpawners);
                    enemiesSpawners = null;
                    Debug.Log("puertas abiertas");
                }

                yield break;
            }
            else
            {
                foreach (GameObject puerta in puertas)
                {
                    if (puerta != null)
                    {
                        puerta.SetActive(true);
                        
                    }
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator MoveCamera(Transform camTransform)
    {
        Vector3 start = camTransform.position;
        Vector3 end = new Vector3(transform.position.x, transform.position.y, start.z);

        float t = 0f;
        while (t < moveDuration)
        {
            camTransform.position = Vector3.Lerp(start, end, t / moveDuration);
            t += Time.deltaTime;
            yield return null;
        }

        camTransform.position = end;
    }
}