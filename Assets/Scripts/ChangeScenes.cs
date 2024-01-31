using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadSceneAsync(1);
    }

    public void Title(){
        SceneManager.LoadSceneAsync(0);
    }

    public void Controls(){
        SceneManager.LoadSceneAsync(3);
    }

    public void Credits(){
        SceneManager.LoadSceneAsync(4);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
