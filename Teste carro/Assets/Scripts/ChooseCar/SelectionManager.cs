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


    public Camera chooseCamera;
    public float smoothedSpeed = 0.125f;
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
    void LateUpdate()
    {
        FocusOnCar(actualCar);
    }

    private void FocusOnCar(int carID)
    {
        MoveCameraTo(carID);
        WriteStats(carID);
    }

    private void MoveCameraTo(int carID)
    {
        Vector3 desiredPositon = cameraPoints[carID].position;
        Vector3 smoothedPosition = Vector3.Lerp(chooseCamera.transform.position, desiredPositon, smoothedSpeed);
        chooseCamera.transform.position = smoothedPosition;

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

