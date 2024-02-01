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
        for (int x = -15; x < 14; x++)
        {
            for (int y = -7; y < 4; y++)
            {   
                //Make sure tile isn't start corner or an indestructible 
                if ((x >= -13 || y <= 1) && (x % 2 != 0 || y % 2 != 0))
                {
                    int random = Random.Range(0,3);
                    if (random == 1)
                    {
                        grid.SetTile(new Vector3Int(x, y, 0), castle);
                        if((x > 13) && (y <= 1) && count > 0)
                        {
                            Instantiate(doorPrefab, new Vector3(x,y), Quaternion.identity);
                            count--;
                        }
                    }
                }
            }
        }
    }
}
