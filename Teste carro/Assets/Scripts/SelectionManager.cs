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

    public Camera chooseCamera;
    public float smoothedSpeed = 0.125f;
    public Transform[] cameraPoints;

    public GameObject[] cars;

    private int actualCar;

    // Start is called before the first frame update
    void Start()
    {
        actualCar = 0;

        focusOnCar(actualCar);
    }
    void LateUpdate()
    {
        focusOnCar(actualCar);
    }

    private void focusOnCar(int carID)
    {
        moveCameraTo(carID);
        writeStats(carID);
    }

    private void moveCameraTo(int carID)
    {
        Vector3 desiredPositon = cameraPoints[carID].position;
        Vector3 smoothedPosition = Vector3.Lerp(chooseCamera.transform.position, desiredPositon, smoothedSpeed);
        chooseCamera.transform.position = smoothedPosition;

    }

    private void writeStats(int carID)
    {
        SimpleCarController simpleCarController = cars[carID].GetComponent<SimpleCarController>();
        speedDisplay.text ="Max speed: " + simpleCarController.maxSpeed.ToString();
        torqueDisplay.text = "Torque: " + simpleCarController.motorForce.ToString();
        tracDisplay.text = simpleCarController.traction;

    }

    public void nextCar() 
    {
        actualCar = (actualCar + 1) % cars.Length;
    }

    public void prevCar()
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

    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void prevScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

