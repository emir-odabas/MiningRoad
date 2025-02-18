using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //Oyun Baslangici
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    //Oyun cikisi
    public void QuitGame()
    {
        Application.Quit(); 
    }
}
