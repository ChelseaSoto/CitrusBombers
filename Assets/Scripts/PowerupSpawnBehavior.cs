using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawnBehavior : MonoBehaviour
{
    public float destructionTime = 1f;

    [Range (0f, 1f)]
    public float powerupSpawnChance = 0.1f;
    
    public GameObject[] spawnableItems;

    private void Start()
    {
        Destroy(gameObject, destructionTime);
    }

    private void OnDestroy()
    {
        if (PlayerMovement.lives > 0)
        {
            if (spawnableItems.Length > 0 && Random.value < powerupSpawnChance)
            {
                int random = Random.Range(0, spawnableItems.Length);
                Instantiate(spawnableItems[random], transform.position, Quaternion.identity);
            }
        }
    }
}
