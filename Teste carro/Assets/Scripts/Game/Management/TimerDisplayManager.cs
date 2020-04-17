using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerDisplayManager : MonoBehaviour
{
 
    public static event Action OnTimerEnd;
    

    public string startTime = "00:00:00";
    public bool increasing = false;
    public static string milliFormat;
    public MyTime curTime;

    public GameObject timePanel;
    public GameObject MinuteBox;
    public GameObject SecondBox;
    public GameObject MilliBox;
    public GameObject BestMinuteBox;
    public GameObject BestSecondBox;
    public GameObject BestMilliBox;

    // Update is called once per frame
    void Update()
    {
        if (increasing) increaseClock();
        else decrease();

        if (MyTime.isZeroed(curTime)) OnTimerEnd();
        
    }

    private void increaseClock()
    {
        curTime.MilliCount += Time.deltaTime * 10;


        if (curTime.MilliCount > 9)
        {
            curTime.MilliCount = 0;
            curTime.SecondCount++;
        }

        WriteMilli(MilliBox, curTime.MilliCount);
            

        if (curTime.SecondCount > 59)
        {
            curTime.SecondCount = 0;
            curTime.MinuteCount++;
        }

        WriteSec(SecondBox, curTime.SecondCount);

        WriteMin(MinuteBox, curTime.MinuteCount);

    }

    private void decrease()
    {
        curTime.MilliCount -= Time.deltaTime * 10;

        if (curTime.MilliCount < 0)
        {
            curTime.MilliCount = 9;
            curTime.SecondCount--;
        }

        WriteMilli(MilliBox, curTime.MilliCount);

        if (curTime.SecondCount < 0)
        {
            curTime.SecondCount = 59;
            curTime.MinuteCount--;
        }

        WriteSec(SecondBox, curTime.SecondCount);

        WriteMin(MinuteBox, curTime.MinuteCount);
    }

    private void WriteMilli(GameObject _milliDisplay, float _mili)
    {
        milliFormat = _mili.ToString("F0");
        _milliDisplay.GetComponent<Text>().text = "" + milliFormat + ".";
    }
    
    private void WriteSec(GameObject _secDisplay, int _sec)
    {
        if (curTime.SecondCount < 10)
        {
            _secDisplay.GetComponent<Text>().text = "0" + _sec + "\"";
        }
        else
        {
            _secDisplay.GetComponent<Text>().text = "" + _sec + "\"";
        }
    }

    private void WriteMin(GameObject _minDisplay, int _min)
    {
        if (_min < 10)
        {
            _minDisplay.GetComponent<Text>().text = "0" + _min + "\'";
        }
        else
        {
            _minDisplay.GetComponent<Text>().text = "" + _min + "\'";
        }
    }

    public MyTime GetCurTime()
    {
        return curTime;
    }

    public void RestartTimer()
    {
        curTime = new MyTime(0,0,0);
    }

    public void updateBestTime(MyTime _time)
    {
        WriteMilli(BestMilliBox, _time.MilliCount);
        WriteSec(BestSecondBox, _time.SecondCount);
        WriteMin(BestMinuteBox, _time.MinuteCount);

    }
     
    public void SetTime(MyTime time)
    {
        curTime = time;
        WriteMilli(MinuteBox, curTime.MilliCount);
        WriteSec(SecondBox, curTime.SecondCount);
        WriteMin(MilliBox, curTime.MinuteCount);
    }

    public void DisableDisplay()
    {
        timePanel.SetActive(false);
    }

    public void EnableDisplays()
    {
        timePanel.SetActive(true);
    }

}
