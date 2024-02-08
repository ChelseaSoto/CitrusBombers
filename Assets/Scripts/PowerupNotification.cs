using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupNotification : MonoBehaviour
{   
    public GameObject powerupUI;
    public AudioSource buttonClickSound;
    public static int firstPickup = 0;

    private void Start()
    {
        buttonClickSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(firstPickup == 1)
        {
            PowerupPause();
        }
    }
    

    public void PowerupResume(){
        PlayButtonClickSound();
        powerupUI.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
    }

    private void PowerupPause(){
        firstPickup = 2;
        powerupUI.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.GameIsPaused = true;
    }

    private void PlayButtonClickSound(){
        if (buttonClickSound != null){
            buttonClickSound.Play();
        }
    }
}
