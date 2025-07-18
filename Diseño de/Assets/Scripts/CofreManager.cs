using System.Collections.Generic;
using UnityEngine;

public class CofreManager : MonoBehaviour
{
    [Header("Configuraci�n del Loot")]
    [Tooltip("Lista de �tems que este cofre puede soltar, junto con sus probabilidades.")]
    public List<LootItem> possibleLoot = new List<LootItem>();
    [SerializeField] AudioSource AudioSourceItems;
    [SerializeField] AudioClip ItemSFX;

    private bool hasBeenOpened = false;

    [System.Serializable]
    public class LootItem
    {
        public ItemData item;
        [Range(0, 100)]
        public float dropChance = 50; // Probabilidad 0-100%
    }
    private void Start()
    {
        AudioSourceItems = GameObject.Find("ItemsSFX").GetComponent<AudioSource>();
    }
    public void OpenChest()
    {
        AudioSourceItems.PlayOneShot(ItemSFX);
        if (hasBeenOpened)
        {
            
            
            
            
            Debug.Log("Cofre abierto :)");
            return;
        }

        hasBeenOpened = true;

        GenerateLoot();
        Destroy(gameObject); // Cofre muere :(
    }

    void GenerateLoot()
    {
        for (int i = 0; i < 1; i++)
        {
            ItemData droppedItem = GetRandomLootItem();

            Vector3 itemLayer = transform.position;
            itemLayer.z = -5f;
            transform.position = itemLayer;

            Instantiate(droppedItem.prefab, itemLayer, Quaternion.identity);
            
        }
        Debug.Log("Cofre abierto");
    }

    ItemData GetRandomLootItem()
    {
        float totalChance = 0;
        foreach (var item in possibleLoot)
        {
            totalChance += item.dropChance;
        }

        float randomValue = Random.Range(0f, totalChance);
        float currentChance = 0;

        foreach (var item in possibleLoot)
        {
            currentChance += item.dropChance;
            if (randomValue <= currentChance)
            {
                return item.item;
            }
        }

        return null; // Debugeo - Balatreo
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Player")) 
        {
            OpenChest();
        }
    }
}
