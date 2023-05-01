using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Reset()
    {
        PlayerPrefs.SetFloat("speed", 1f);
        PlayerPrefs.SetInt("level", 0);
        PlayerPrefs.SetInt("lastScore", 0);
        PlayerPrefs.SetInt("highscore", 0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
