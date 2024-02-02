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

    void Start()
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

        PlaceDoor();
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
}
