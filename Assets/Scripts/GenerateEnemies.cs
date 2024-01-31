using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateEnemies : MonoBehaviour
{
    public Tilemap destructibleTiles;


    public GameObject goldPrefab;
    public GameObject redPrefab;
    public GameObject greenPrefab;
    
    private int goldCount, redCount, greenCount;
    private float spawnRate;

    public enum SceneName
    {
        level1,
        level2,
    }

    public SceneName name;

    // Start is called before the first frame update
    void Start()
    {

        goldCount = 10;
        greenCount = 2;
        redCount = 1;

        switch(name)
        {
            case SceneName.level1:
                goldCount = 15;
                greenCount = 6;
                break;
            case SceneName.level2:
                goldCount = 20;
                redCount = 5;
                greenCount = 12;
                break;
        }
        
        SpawnEnemyType();
    } 
    
    void SpawnEnemyType()
    { 
        for (int x = -14; x < 14; x++)
        {
            for (int y = -6; y < 4; y++)
            {
                TileBase tile = destructibleTiles.GetTile(new Vector3Int(x,y));

                if (tile == null)
                {
                    if ((x >= 0) && (x % 2 == 0 || y % 2 == 0))
                    {
                        
                        spawnRate = 0.2f;
                        if (Random.value < spawnRate && greenCount > 0)
                        {
                            var spawnedEnemy = Instantiate(greenPrefab, new Vector3(x,y), Quaternion.identity);
                            spawnedEnemy.name = $"Enemy Green {x} {y}";
                            greenCount--;
                        }
                    }
                    if ((x >= 2) && (x % 2 == 0 || y % 2 == 0))
                    {
                        
                        
                        spawnRate = 0.1f;
                        if (Random.value < spawnRate && redCount > 0)
                        {
                            var spawnedEnemy = Instantiate(redPrefab, new Vector3(x,y), Quaternion.identity);
                            spawnedEnemy.name = $"Enemy Red {x} {y}";
                            redCount--;
                        }
                    }
                    if ((x >= -12 || y <= 0) && (x % 2 == 0 || y % 2 == 0))
                    {
                        
                        spawnRate = 0.4f;
                        if (Random.value < spawnRate && goldCount > 0)
                        {
                            var spawnedEnemy = Instantiate(goldPrefab, new Vector3(x,y), Quaternion.identity);
                            spawnedEnemy.name = $"Enemy Gold {x} {y}";
                            goldCount--;
                        }
                    }
                }
            }
        } 
        if (goldCount > 0 || redCount > 0 || greenCount > 0)
        {
            SpawnEnemyType();
        }
    }
}
