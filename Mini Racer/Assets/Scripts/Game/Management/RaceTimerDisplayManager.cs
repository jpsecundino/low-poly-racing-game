using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RaceTimerDisplayManager : MonoBehaviour
{
 
    public static event Action OnTimerEnd;
    public static event Action OnTimerStart;


    public string startTime = "00:00:00";
    public bool increasing = false;
    public static string milliFormat;
    private float countdownTime = 3f;
    public bool isCountdownOn = true;

    public MyTime curTime;

    public Text timePanel;

    public TrafficLight trafficLight;
    
    public Text countdownText;


    private void Start()
    {
        countdownTime += 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isCountdownOn == true)
        {
            if(countdownTime < 1)
            {
                isCountdownOn = false;
                trafficLight.OnGreen();
                OnTimerStart();
            }
            else
            {
                countdownText.text = ( (int) countdownTime).ToString();
                countdownTime -= Time.deltaTime;

                switch((int)countdownTime)
                {
                    case 3:
                        trafficLight.OnRed();
                        break;
                    case 2:
                        trafficLight.OnYellow();
                        break;
                }

            }
        }
        else
        {
            if (increasing) increaseClock();
            else decrease();
            if (MyTime.isZeroed(curTime)) OnTimerEnd();

        }

        
    }

    private void increaseClock()
    {
        curTime.MilliCount += Time.deltaTime * 10;


        if (curTime.MilliCount > 9)
        {
            curTime.MilliCount = 0;
            curTime.SecondCount++;
        }         

        if (curTime.SecondCount > 59)
        {
            curTime.SecondCount = 0;
            curTime.MinuteCount++;
        }

        SetTime(curTime.MinuteCount, curTime.SecondCount, curTime.MilliCount);

    }

    private void decrease()
    {
        curTime.MilliCount -= Time.deltaTime * 10;

        if (curTime.MilliCount < 0)
        {
            curTime.MilliCount = 9;
            curTime.SecondCount--;
        }

        if (curTime.SecondCount < 0)
        {
            curTime.SecondCount = 59;
            curTime.MinuteCount--;
        }

        SetTime(curTime.MinuteCount, curTime.SecondCount, curTime.MilliCount);

    }

    public MyTime GetCurTime()
    {
        return curTime;
    }

    public void RestartTimer()
    {
        curTime = new MyTime(0,0,0);
    }
     
    public void SetTime(MyTime time)
    {
        curTime = time;
        WriteTime(curTime);
    }

    public void SetTime(int _min, int _sec, float _milli)
    {
        curTime = new MyTime(_min, _sec, _milli);
        WriteTime(curTime);
    }

    private void WriteTime(MyTime curTime)
    {
        timePanel.GetComponent<Text>().text = "Timer: " 
                                                + curTime.MinuteCount.ToString("00")
                                                + "\""
                                                + curTime.SecondCount.ToString("00")
                                                + "\'"
                                                + (int) curTime.MilliCount
                                                + ".";
    }

    public void DisableDisplay()
    {
        timePanel.enabled = false;
    }

    public void EnableDisplays()
    {
        timePanel.enabled = true;
    }

}
