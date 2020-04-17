using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseDifficultSceneManager: MonoBehaviour
{

    public Button easyButton; 
    public Button medButton; 
    public Button hardButton;

    public void changeDifficult (Button b)
    {
        switch (b.gameObject.name)
        {
            case "EasyButton":
                GameInfo.Instance.actualDifficulty = (int) GameInfo.Difficulty.easy;
                break;
            case "MedButton":
                GameInfo.Instance.actualDifficulty = (int) GameInfo.Difficulty.medium;
                break;
            case "HardButton":
                GameInfo.Instance.actualDifficulty = (int)GameInfo.Difficulty.hard;
                break;
        }

        nextScene();
    }

    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void prevScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
