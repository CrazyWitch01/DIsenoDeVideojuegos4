using UnityEngine;
using System.Collections;

public class CambioCuartos : MonoBehaviour
{
    public float moveDuration = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject camObj = GameObject.FindGameObjectWithTag("CineMachineMainCamera");
            if (camObj != null)
            {
                StartCoroutine(MoveCamera(camObj.transform));
            }
            else
            {
                Debug.Log("Camara no encontrada, tagearla como CineMachineMainCamera");
            }
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
