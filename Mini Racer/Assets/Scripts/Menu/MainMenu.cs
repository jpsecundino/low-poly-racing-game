using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public GameObject MainMenuPanel;
    public GameObject HowToPlayPanel;


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ActivateHowToPanel()
    {
        MainMenuPanel.SetActive(false);
        HowToPlayPanel.SetActive(true);
    }

    public void ActivateMainMenuPanel()
    {
        MainMenuPanel.SetActive(true);
        HowToPlayPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
