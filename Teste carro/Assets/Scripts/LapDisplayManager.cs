using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapDisplayManager : MonoBehaviour
{
    private int totalLaps = 0;
    private int curLap = 0;

    public GameObject lapPanel; 
    public GameObject curLapDisplay;
    public GameObject totalLapDisplay;

    public void IncreaseActualLap()
    {
        curLap++;
        curLapDisplay.GetComponent<Text>().text = "" + curLap;
    }

    public void SetTotalLap(int _laps)
    {
        totalLaps = _laps;
        totalLapDisplay.GetComponent<Text>().text = "" + _laps;
    }

    public void SetCurLap(int _curLap)
    {
        curLap = _curLap;
        curLapDisplay.GetComponent<Text>().text = "" + curLap;
    }

    public void DisableDisplay()
    {
        lapPanel.SetActive(false);
    }

    public void EnableDisplays()
    {
        lapPanel.SetActive(true);
    }
}
