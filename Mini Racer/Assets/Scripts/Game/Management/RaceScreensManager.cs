using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceScreensManager : MonoBehaviour
{

    public GameObject wonScreen;
    public GameObject lostScreen;
    public GameObject endScreen;
    public GameObject pauseScreen;
    public GameObject countdownScreen;

    public RaceTimerDisplayManager timeManager;
    public LapDisplayManager lapManager;

    private void Awake()
    {
        RegisterEvents();
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }

    private void RegisterEvents()
    {
        RaceManager.OnWon += RaceManager_OnWon;
        RaceManager.OnLost += RaceManager_OnLost;
        RaceManager.OnPause += RaceManager_OnPause;
        RaceManager.OnResume += RaceManager_OnResume;
        RaceTimerDisplayManager.OnTimerStart += RaceTimerDisplayManager_OnTimerStart;
    }


    private void UnregisterEvents()
    {
        RaceManager.OnWon -= RaceManager_OnWon;
        RaceManager.OnLost -= RaceManager_OnLost;
        RaceManager.OnPause -= RaceManager_OnPause;
        RaceManager.OnResume -= RaceManager_OnResume;
        RaceTimerDisplayManager.OnTimerStart -= RaceTimerDisplayManager_OnTimerStart;
    }

    private void RaceTimerDisplayManager_OnTimerStart()
    {
        countdownScreen.SetActive(false);
    }

    public void RaceManager_OnWon()
    {
        lapManager.DisableDisplay();
        timeManager.DisableDisplay();
        wonScreen.transform.Find("TimeQuantityText").GetComponent<Text>().text = timeManager.curTime.ToString();
        wonScreen.SetActive(true);
    }
    
    public void RaceManager_OnLost()
    {
        lapManager.DisableDisplay();
        timeManager.DisableDisplay();
        lostScreen.SetActive(true);
    }
    
    public void RaceManager_OnPause()
    {
        timeManager.DisableDisplay();
        lapManager.DisableDisplay();
        pauseScreen.SetActive(true);
        countdownScreen.SetActive(false);   

        Time.timeScale = 0f;
    }
    
    public void RaceManager_OnResume()
    {
        pauseScreen.SetActive(false);
        endScreen.SetActive(false);
        timeManager.EnableDisplays();
        lapManager.EnableDisplays();
        Time.timeScale = 1f;
        if (timeManager.isCountdownOn)
            countdownScreen.SetActive(true);
    }

    public void EndSceen()
    {
        wonScreen.SetActive(false);
        lostScreen.SetActive(false);
        endScreen.SetActive(true);
    }

}
