using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Jugador/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string nombreItem = "Nuevo �tem";
    public Sprite spritePlayer; // Sprites por si acas�n (servir� para el Animator espero)
    public GameObject prefab; // Un prefab a instanciar(?) cu�l ser� loco
    public int vidaPlayer = 2; // Vida del jugador
    public float velPlayer = 2; // Velocidad de movimiento del jugador
    public int atqPlayer = 1; // Ataque del jugador
    public int experienciaActual = 0;
}
