using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Jugador/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string nombreItem = "Nuevo item";
    public Sprite spritePlayer; // Sprites por si acaso (servirá para el Animator espero)
    public GameObject prefab; // Un prefab a instanciar(?) cuál será loco
    public int vidaPlayer = 5; // Vida del jugador
    public float velPlayer = 4; // Velocidad de movimiento del jugador
    public int atqPlayer = 1; // Ataque del jugador
    public int vidaMaxima = 5;
    public int experienciaActual = 0;

   
    //////////////////////////////////////////////////////////////////////////////////////////////
    public void AumentarVidaJugador()
    {
        vidaPlayer++;
        vidaMaxima++;
        Debug.Log("El jugador ganó vida");
    }
    public void RestarVidaJugador()
    {
        vidaPlayer--;
        Debug.Log("El jugador perdió vida");
    }

    public void AumentarVelJugador()
    {
        velPlayer++;
        Debug.Log("El jugador ganó velocidad");
    }

    public void RestarVelJugador()
    {
        velPlayer--;
        Debug.Log("El jugador perdió velocidad");
    }

    public void AumentarATQJugador()
    {
        atqPlayer++;
        Debug.Log("El jugador ganó ataque");
    }

    public void RestarATQJugador()
    {
        atqPlayer--;
        Debug.Log("El jugador perdió ataque");
    }

    public void ReiniciarStats()
    {
        atqPlayer = 1;
        velPlayer = 4;
        vidaPlayer = 5;
        vidaMaxima = 5;
        Debug.Log("El jugador perdió ataque");
    }
}
