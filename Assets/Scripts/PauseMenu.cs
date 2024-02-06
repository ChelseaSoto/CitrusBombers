using System.Collections;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    public GameObject pauseMenuUI;
    public AudioSource buttonClickSound;

    private void Start(){
        buttonClickSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)){
            if (GameIsPaused){
                Resume();
            }else{
                Pause();
            }
        }
    }

    public void Resume(){
        PlayButtonClickSound();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    private void PlayButtonClickSound(){
        if (buttonClickSound != null){
            buttonClickSound.Play();
        }
    }
}
