using UnityEngine;
using UnityEngine.InputSystem;


public class CabezaRotacion : MonoBehaviour
{
    private InputSystem_Actions input;
    private Camera cam;

    [Header("Sensibilidad del stick derecho")]
    public float stickDeadZone = 0.2f;

    void Awake()
    {
        input = new InputSystem_Actions();
    }

    void OnEnable()
    {
        input.NewPlayer.Enable();
    }

    void OnDisable()
    {
        input.NewPlayer.Disable();
    }

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector2 stickInput = input.NewPlayer.Look.ReadValue<Vector2>();

        if (stickInput.magnitude > stickDeadZone)
        {
            float anguloZ = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, anguloZ - 90f);
        }
        else
        {
            Vector2 mousePantalla = Mouse.current.position.ReadValue();
            Vector3 mouseMundo = cam.ScreenToWorldPoint(mousePantalla);
            Vector2 direccion = (Vector2)(mouseMundo - transform.position);

            float anguloZ = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, anguloZ - 90f);
        }
    }
}
 
