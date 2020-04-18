using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapDisplayManager : MonoBehaviour
{
    private int totalLaps = 0;
    private int curLap = 0;

    public Text lapText;

    public void IncreaseActualLap()
    {
        curLap++;
        lapText.text = "Lap: " + curLap + "/" + totalLaps;  
    }

    public void SetTotalLap(int _laps)
    {
        totalLaps = _laps;
        lapText.text = "Lap: " + curLap + "/" + totalLaps;
    }

    public void SetCurLap(int _curLap)
    {
        curLap = _curLap;
        lapText.text = "Lap: " + curLap + "/" + totalLaps;
    }

    public void DisableDisplay()
    {
        lapText.enabled = false;
    }

    public void EnableDisplays()
    {
        lapText.enabled = true;
    }
}
