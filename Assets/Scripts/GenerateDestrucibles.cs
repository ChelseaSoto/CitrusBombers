using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateDestrucibles : MonoBehaviour
{
    public Tilemap grid;
    public Tile castle; 
    public GameObject doorPrefab;
    private int count = 1;

    void Start()
    {
        //For each tile on game board
        for (int x = -14; x < 14; x++)
        {
            for (int y = -7; y < 4; y++)
            {   
                //Make sure tile isn't start corner or an indestructible 
                if ((x >= -13 || y <= 1) && (x % 2 != 0 || y % 2 != 0))
                {
                    int random = Random.Range(0,2);
                    if (random == 1)
                    {
                        grid.SetTile(new Vector3Int(x,y), castle);
                        Debug.Log("Castle placed at: "+x+" "+y);
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
                    Vector3Int position = new Vector3Int(x,y);

                    Debug.Log("Checking for castle");
                    Vector3Int cell = grid.WorldToCell(position);
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
