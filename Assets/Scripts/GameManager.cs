using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int enemyCount;
    public bool timerOn;

    public GameObject player;

    public static float timeRemaining;
    public TextMeshProUGUI timerTxt;

    private void Start()
    {

        enemyCount = 1;
        timerOn = true;
        timeRemaining = 300f;

    }

    private void Update()
    {
        if (timerOn)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                updateTimer(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerOn = false;
                StartCoroutine(player.GetComponent<PlayerMovement>().DeathSequence());
            } 
        }
         
    }

    public void CheckLoseState()
    {
        bool alive = false;
        
        if (player.activeSelf) {
            alive = true;
        }

        if (alive == false)
        {
            SceneManager.LoadSceneAsync("Game Over");
        }
    }

    private void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


}
