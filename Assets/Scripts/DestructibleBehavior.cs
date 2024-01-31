using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBehavior : MonoBehaviour
{
    public float destructionTime = 1f;

    public float powerupSpawnChance = 0.1f;
    public int[] spawnLimits = {2,3};
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
            if (spawnLimits[random] > 0)
            {
                Instantiate(spawnableItems[random], transform.position, Quaternion.identity);
                spawnLimits[random]--;
            }
        }
    }
}
