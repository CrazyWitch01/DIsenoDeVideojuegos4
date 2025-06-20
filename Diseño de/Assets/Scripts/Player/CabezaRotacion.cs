using UnityEngine;
using UnityEngine.InputSystem;


public class CabezaRotacion : MonoBehaviour
{
    private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePantalla = Mouse.current.position.ReadValue();
        Vector3 mouseMundo = cam.ScreenToWorldPoint(mousePantalla);
        Vector2 direction = (Vector2)(mouseMundo - transform.position);

        float anguloz = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, anguloz - 90f);
    }
}
