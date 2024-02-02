using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public int score;
    public int lives;
    public int range;
    public int enemyCount;

    void Start()
    {
        lives = 3;
        score = 0;
        enemyCount = 1;
    }
}
