using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehavior : MonoBehaviour
{
    private GameManager gms;
    public Collider2D collider;
    int remaining;
    string name;

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        name = scene.name;
        Debug.Log("This is Scene: "+name);
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        remaining = gms.enemyCount;
        if (remaining == 0)
        {
            collider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        
        if(other.tag == "Player")
        {
            if(name == "Level1")
            {
                SceneManager.LoadSceneAsync("level2");
            }
            else if (name == "Level2")
            {
                SceneManager.LoadSceneAsync("Win Screen");
            } 
        }
    }
}
