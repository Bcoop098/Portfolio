using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
        return;
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
        return;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        return;
    }

    public void Next()
    {
        SceneManager.LoadScene("HowToPlay2");
        return;
    }

    public void Next2()
    {
        SceneManager.LoadScene("HowToPlay3");
        return;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}