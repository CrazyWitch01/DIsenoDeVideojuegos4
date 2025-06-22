using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI textoVida;
    public TextMeshProUGUI textoAtaque;
    public TextMeshProUGUI textoVelocidad;
    public TextMeshProUGUI textoExperiencia;

    private PlayerMain player;
    void Start()
    {
        player = FindFirstObjectByType<PlayerMain>();
    }

    // Update is called once per frame
    void Update()
    {
        textoVida.text = "x" + player.vidaJugador;
        textoAtaque.text = "x" + player.atqJugador;
        textoVelocidad.text = "x" + player.velJugador;
        textoExperiencia.text = "x" + player.experienciaJugador;
    }
}
