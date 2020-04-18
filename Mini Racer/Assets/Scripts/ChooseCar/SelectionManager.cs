using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI speedDisplay;
    public TMPro.TextMeshProUGUI torqueDisplay;
    public TMPro.TextMeshProUGUI tracDisplay;

    public GameObject chooseCarPanel;
    public GameObject chooseDifficultPanel;


    public SimpleCameraController chooseCamera;
    public Transform[] cameraPoints;

    public GameObject[] cars;
    private enum difficulty
    {
        easy,
        medium,
        hard
    };

    private int actualCar;

    // Start is called before the first frame update
    void Start()
    {
        actualCar = 0;
        FocusOnCar(actualCar);
    }

    private void Update()
    {
        if (chooseCarPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                PrevCar();    
            }
        
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                NextCar();    
            }
        }
    }

    void LateUpdate()
    {
        FocusOnCar(actualCar);
    }

    private void FocusOnCar(int carID)
    {
        chooseCamera.objectiveTranf = cameraPoints[carID];
        WriteStats(carID);
    }

    private void WriteStats(int carID)
    {
        SimpleCarController simpleCarController = cars[carID].GetComponent<SimpleCarController>();
        speedDisplay.text ="Max speed: " + simpleCarController.maxSpeed.ToString();
        torqueDisplay.text = "Torque: " + simpleCarController.motorForce.ToString();
        tracDisplay.text = simpleCarController.traction;

    }

    public void NextCar() 
    {
        actualCar = (actualCar + 1) % cars.Length;
    }

    public void PrevCar()
    {
        if(actualCar - 1 < 0)
        {
            actualCar = cars.Length - 1;
        }
        else
        {
            actualCar = (actualCar - 1) % cars.Length;
        }
    }

    public void SelectCar()
    {
        GameInfo.Instance.carSelected = actualCar;
        ActivateChooseDifficultyPanel();
    }

    public void ActivateChooseDifficultyPanel()
    {
        chooseCarPanel.SetActive(false);
        chooseDifficultPanel.SetActive(true);
    }

    public void ActivateChooseCarPanel()
    {
        chooseCarPanel.SetActive(true);
        chooseDifficultPanel.SetActive(false);
    }

    public void SelectDifficulty(Button b)
    {
        
        switch (b.gameObject.name)
        {
            case "EasyButton":
                GameInfo.Instance.actualDifficulty = (int) difficulty.easy;
                break;
            case "MedButton":
                GameInfo.Instance.actualDifficulty = (int) difficulty.medium;
                break;
            case "HardButton":
                GameInfo.Instance.actualDifficulty = (int) difficulty.hard;
                break;
        }

        NextScene();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

