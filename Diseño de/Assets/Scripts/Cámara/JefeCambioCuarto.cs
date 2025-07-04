using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class JefeCambioCuarto : MonoBehaviour
{
    public float moveDuration = 1f;
    [SerializeField] private GameObject enemiesSpawners;
    private List<GameObject> puertas = new List<GameObject>();
    public GameObject _cinemachineCamera;
    public AudioSource MusicaSource;
    public AudioClip AudioMusicaBoss;

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
    void Start()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("EnemySpawner");

        foreach (GameObject spawner in spawners)
        {
            spawner.SetActive(false);
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
                MusicaSource.clip = AudioMusicaBoss;
                MusicaSource.Play();


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
        Vector3 startPos = camTransform.position;
        Vector3 endPos = new Vector3(transform.position.x, transform.position.y, startPos.z);

        float t = 0f;
        while (t < moveDuration)
        {
            float progress = t / moveDuration;
            camTransform.position = Vector3.Lerp(startPos, endPos, progress);
            t += Time.deltaTime;
            yield return null;
        }

        camTransform.position = endPos;

        var cineCam = camTransform.GetComponent<CinemachineCamera>();
        float zoomDuration = 1f;
        float zoomStart = cineCam.Lens.OrthographicSize;
        float zoomEnd = 10f;

        t = 0f;
        while (t < zoomDuration)
        {
            float progress = t / zoomDuration;
            cineCam.Lens.OrthographicSize = Mathf.Lerp(zoomStart, zoomEnd, progress);
            t += Time.deltaTime;
            yield return null;
        }

        cineCam.Lens.OrthographicSize = zoomEnd;
    }
}
