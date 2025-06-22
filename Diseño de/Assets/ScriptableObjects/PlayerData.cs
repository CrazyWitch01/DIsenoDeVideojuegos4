using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Jugador/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string nombreItem = "Nuevo Ítem";
    public Sprite spritePlayer; // Sprites por si acasín (servirá para el Animator espero)
    public GameObject prefab; // Un prefab a instanciar(?) cuál será loco
    public int vidaPlayer = 2; // Vida del jugador
    public float velPlayer = 2; // Velocidad de movimiento del jugador
    public int atqPlayer = 1; // Ataque del jugador
    public int experienciaActual = 0;
}
