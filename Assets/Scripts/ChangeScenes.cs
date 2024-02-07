using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public AudioSource buttonClickSound;

    private void Start(){
        buttonClickSound = GetComponent<AudioSource>();
    }

    public void PlayGame(){
        StartCoroutine(PlayAndLoad(1));
    }

    public void Title(){
        StartCoroutine(PlayAndLoad(0));
        StartCoroutine(DelayedResume());
    }

    public void Controls(){
        StartCoroutine(PlayAndLoad(3));
    }

    public void Credits(){
        StartCoroutine(PlayAndLoad(4));
    }

    public void QuitGame(){
        StartCoroutine(PlayAndQuit());
    }

    private IEnumerator PlayAndLoad(int sceneIndex){
        PlayButtonClickSound();
        yield return new WaitForSecondsRealtime(buttonClickSound.clip.length);
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    private IEnumerator PlayAndQuit(){
        PlayButtonClickSound();
        yield return new WaitForSecondsRealtime(buttonClickSound.clip.length);
        Application.Quit();
    }

    private void PlayButtonClickSound(){
        if (buttonClickSound != null){
            buttonClickSound.Play();
        }
    }

    private IEnumerator DelayedResume()
    {
        yield return new WaitForSecondsRealtime(buttonClickSound.clip.length);
        GetComponent<PauseMenu>().Resume();
    }
}
