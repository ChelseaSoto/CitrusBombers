using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public AudioSource buttonClickSound;

    private void Start(){
        buttonClickSound = GetComponent<AudioSource>();
    }

    public void PlayGame(){
        PlayButtonClickSound();
        SceneManager.LoadSceneAsync(1);
    }

    public void Title(){
        PlayButtonClickSound();
        SceneManager.LoadSceneAsync(0);
    }

    public void Controls(){
        PlayButtonClickSound();
        SceneManager.LoadSceneAsync(3);
    }

    public void Credits(){
        PlayButtonClickSound();
        SceneManager.LoadSceneAsync(4);
    }

    public void QuitGame(){
        PlayButtonClickSound();
        Application.Quit();
    }

    private void PlayButtonClickSound(){
        if (buttonClickSound != null)
        {
            buttonClickSound.Play();
        }
    }

    private IEnumerator LoadSceneAsync(int sceneIndex){
        yield return new WaitForSeconds(buttonClickSound.clip.length);
        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
