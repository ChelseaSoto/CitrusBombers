using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateEnemies : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject goldPrefab;
    public GameObject redPrefab;
    public GameObject greenPrefab;
    
    public enum SceneName
    {
        level1,
        level2,
    }

    public SceneName name;

    [Header("Destructibles")]
    public Tilemap grid;
    public Tile castle;

    [Header("Indestructibles")]
    public Tilemap ground;
    public Tile umbrella;

    private GameManager gms;
    private int goldCount, redCount, greenCount;

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
        StartCoroutine(DelayForCastle());
        SpawnEnemyType();
        
    } 
    
    void SpawnEnemyType()
    { 
        //For each tile on game board
        for (int x = -14; x < 15; x++)
        {
            for (int y = -6; y < 5; y++)
            {  
                //Make sure tile isn't start corner or an indestructible 
                if ((x >= -12 || y <= 2))
                {
                    Vector3Int groundPosition = ground.WorldToCell(new Vector3Int(x,y));
                    TileBase groundCheck = ground.GetTile(groundPosition);
                    Vector3Int castlePosition = ground.WorldToCell(new Vector3Int(x,y));
                    TileBase castleCheck = ground.GetTile(castlePosition);

                    if (groundCheck != umbrella && castleCheck != castle)
                    {
                        int random = Random.Range(0,15);
                        if (random == 1 && goldCount > 0)
                        {
                            Debug.Log("No obstacles at "+x+" "+y);
                            var spawnedEnemy = Instantiate(goldPrefab, new Vector3(x,y), Quaternion.identity);
                            spawnedEnemy.name = $"Enemy Gold {x} {y}";
                            goldCount--;
                        }

                        if (x >= 0)
                        {
                            if (random == 2 && greenCount > 0)
                            {
                                Debug.Log("No obstacles at "+x+" "+y);
                                var spawnedEnemy = Instantiate(greenPrefab, new Vector3(x,y), Quaternion.identity);
                                spawnedEnemy.name = $"Enemy Green {x} {y}";
                                greenCount--;
                            }
                        }

                        if (x >= 10)
                        {
                            if (random == 3 && redCount > 0)
                            {
                                Debug.Log("No obstacles at "+x+" "+y);
                                var spawnedEnemy = Instantiate(redPrefab, new Vector3(x,y), Quaternion.identity);
                                spawnedEnemy.name = $"Enemy Red {x} {y}";
                                redCount--;
                            }
                        }
                    }
                    
                }

            }
        }
        if (redCount > 0 || greenCount > 0 || goldCount > 0)
        {
            SpawnEnemyType();
        }
    }

    private IEnumerator DelayForCastle()
    {
        yield return new  WaitForSeconds(2f);
    }
}
