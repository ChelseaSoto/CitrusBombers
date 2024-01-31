using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateDestrucibles : MonoBehaviour
{
    public Tilemap grid;
    public Tile castle; 

    void Start()
    {
        //For each tile on game board
        for (int x = -15; x < 14; x++)
        {
            for (int y = -7; y < 4; y++)
            {

                //Make sure tile isn't start corner or an indestructible 
                if ((x >= -13 || y <= 1) && (x % 2 != 0 || y % 2 != 0))
                {
                    float spawnRate = .4f;
                    if (Random.value < spawnRate)
                    {
                        grid.SetTile(new Vector3Int(x, y, 0), castle);
                    }
                }
            }
        }
    }
}
