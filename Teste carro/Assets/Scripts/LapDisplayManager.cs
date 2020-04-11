using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapDisplayManager : MonoBehaviour
{
    public int totalLaps = 0;
    public int curLap = 0;

    public GameObject curLapDisplay;
    public GameObject totalLapDisplay;

    private void Start()
    {
        curLapDisplay.GetComponent<Text>().text = "" + curLap;
        totalLapDisplay.GetComponent<Text>().text = "" + totalLaps;
    }

    public void IncreaseActualLap()
    {
        curLap++;
        curLapDisplay.GetComponent<Text>().text = "" + curLap;
    }

}
