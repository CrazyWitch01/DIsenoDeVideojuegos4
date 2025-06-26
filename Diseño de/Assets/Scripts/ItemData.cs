using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventario/Item Data")]
public class ItemData : ScriptableObject
{
    public string nombreItem = "Nuevo �tem";
    public Sprite sprite; // Si tienes iconos para tu inventario
    public GameObject prefab; // El prefab real del �tem a instanciar
    public int valor = 1; // Un ejemplo de propiedad, puedes a�adir m�s

}