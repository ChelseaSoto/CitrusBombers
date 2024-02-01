using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject goldPrefab;
    public GameObject redPrefab;
    public GameObject greenPrefab;

    private GameManager gms;

    private int goldCount, redCount, greenCount;
    
    public enum SceneName
    {
        level1,
        level2,
    }

    public SceneName name;

    // Start is called before the first frame update
    void Start()
    {
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();

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
        
        gms.enemyCount = goldCount + redCount + greenCount;
        SpawnEnemyType();
        
    } 
    
    void SpawnEnemyType()
    { 
        for (int x = -14; x < 14; x++)
        {
            for (int y = -6; y < 4; y++)
            {
                if((x >= 0) && (x % 2 == 0 || y % 2 == 0))
                {
                    
                    int random = Random.Range(0,10);
                    if (random == 1 && greenCount > 0)
                    {
                        var spawnedEnemy = Instantiate(greenPrefab, new Vector3(x,y), Quaternion.identity);
                        spawnedEnemy.name = $"Enemy Green {x} {y}";
                        greenCount--;
                    }
                }
                if((x >= 2) && (x % 2 == 0 || y % 2 == 0))
                {
                    
                    
                    int random = Random.Range(0,10);
                    if (random == 1 && redCount > 0)
                    {
                        var spawnedEnemy = Instantiate(redPrefab, new Vector3(x,y), Quaternion.identity);
                        spawnedEnemy.name = $"Enemy Red {x} {y}";
                        redCount--;
                    }
                }
                if ((x >= -12 || y <= 0) && (x % 2 == 0 || y % 2 == 0))
                {
                    
                    int random = Random.Range(0,10);
                    if (random == 1 && goldCount > 0)
                    {
                        var spawnedEnemy = Instantiate(goldPrefab, new Vector3(x,y), Quaternion.identity);
                        spawnedEnemy.name = $"Enemy Gold {x} {y}";
                        goldCount--;
                    }
                }
            }
        } 
        if (redCount > 0 || greenCount > 0 || goldCount > 0)
        {
            SpawnEnemyType();
        }
    }
}
