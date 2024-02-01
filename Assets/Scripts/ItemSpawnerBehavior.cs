using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerBehavior : MonoBehaviour
{
    public float destructionTime = 1f;
    [Range(0f, 1f)]
    public float powerupSpawnChance;
    //public int[] spawnLimits = { 2, 3 };
    public GameObject[] spawnableItems;

    void Start()
    {
        Destroy(gameObject, destructionTime);
    }

    private void OnDestroy()
    {
        if (spawnableItems.Length > 0 && Random.value < powerupSpawnChance)
        {
            int random = Random.Range(0, spawnableItems.Length);
            Instantiate(spawnableItems[random], transform.position, Quaternion.identity);
        }
    }
}
