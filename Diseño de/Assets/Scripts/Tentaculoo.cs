using UnityEngine;
using System.Collections;

public class Tentaculoo : MonoBehaviour
{
    private Collider2D col;
    public float tempo;
    public float tempo2;

    void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }

    IEnumerator Colision()
    {
        col.enabled = false;
        yield return new WaitForSeconds(tempo);
        col.enabled = true;
        yield return new WaitForSeconds(tempo);
        Destroy(col.gameObject);
    }

}
