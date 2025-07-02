using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "Jefe/BossData")]
public class BossData : ScriptableObject
{
    public string nombreJefe = "Demon";
    public Sprite spriteBoss; // Sprite(?)
    public GameObject prefab; // Prefab del jefe
    public int vidaJefe = 30; // Vida del jefe
    public int atqJefe = 1; // Ataque del jefe

    ///////////////////////////////////////////////////////////////////

    public void QuitarVidaJefe()
    {
        vidaJefe--;
        //Debug.Log("El jefe perdió 1 de vida");
    }

    public void AumentoATQJefe()
    {
        atqJefe = 2;
        //Debug.Log("El jefe aumentó en 1 su ataque");
    }

    public void RestablecerATQJefe()
    {
        atqJefe = 1;
        //Debug.Log("El jefe restableció su ataque");
    }

    public void RestablecerStatsJefe()
    {
        atqJefe = 1;
        vidaJefe = 30;
        //Debug.Log("Los datos del jefe se reiniciaron exitosamente");
    }
}
