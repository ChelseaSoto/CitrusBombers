using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehavior : MonoBehaviour
{
    //If all enemies defeated activate trigger to go to next stage or win screen
    private GameManager gms;
    public Collider2D collider;
    int remaining;

    public enum SceneName
    {
        level1,
        level2,
    }
    public SceneName name;

    void Start()
    {
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();
        collider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        remaining = gms.enemyCount;
        Debug.Log("Remaining Enemies:" + remaining);

        if (remaining == 0)
        {
            collider.enabled = true;
        }
        
        if(other.tag == "Player")
        {
           switch(name)
            {
                case SceneName.level1:
                    Debug.Log("Going to level 2");
                    SceneManager.LoadScene("level2");
                    break;
                
                case SceneName.level2:
                    Debug.Log("Going to Win Screen");
                    SceneManager.LoadScene("Win Screen");
                    break;
            } 
        }
    }
}
