using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateDestrucibles : MonoBehaviour
{
    [Header("Destructibles")]
    public Tilemap grid;
    public Tile castle;

    [Header("Indestructibles")]
    public Tilemap ground;
    public Tile umbrella;

    [Header("Door")]
    public GameObject doorPrefab;
    private int count = 1;

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

    private GameManager gms;
    private int goldCount, greenCount, redCount;

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

        BuildBoard();
        PlaceDoor();
        SpawnEnemyType();
    }

    private void BuildBoard()
    {
        //For each tile on game board
        for (int x = -14; x < 15; x++)
        {
            for (int y = -6; y < 5; y++)
            {  
                //Make sure tile isn't start corner or an indestructible 
                if ((x >= -12 || y <= 2))
                {
                    Vector3Int position = ground.WorldToCell(new Vector3Int(x,y));
                    TileBase check = ground.GetTile(position);
                    
                    if (check != umbrella)
                    {
                        int random = Random.Range(0,2);
                        if (random == 1)
                        {
                            grid.SetTile(position, castle);
                        }
                        
                    }
                }
            }
        }
    }

    private void PlaceDoor()
    {
        
        for (int x = 12; x < 14; x++)
        {
            for (int y = -6; y < 4; y++)
            {   
                int random = Random.Range(0,3);
                if(count > 0 && random == 1)
                {   
                    Vector3Int cell = grid.WorldToCell(new Vector3Int(x,y));
                    TileBase tile = grid.GetTile(cell);

                    if (tile == castle)
                    {
                        Debug.Log("Castle found at:"+x+" "+y);
                        var Door = Instantiate(doorPrefab, new Vector3(x,y), Quaternion.identity);
                        Door.name = $"Door {x} {y}";
                        count--;
                    }
                }
            }
        }

        if (count > 0)
        {
            PlaceDoor();
        }
    }

    private void SpawnEnemyType()
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
                    Vector3Int castlePosition = grid.WorldToCell(new Vector3Int(x,y));
                    TileBase castleCheck = grid.GetTile(castlePosition);

                    if (groundCheck != umbrella && castleCheck == null)
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
}