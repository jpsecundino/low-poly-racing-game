using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyTime : IComparable
{
    public int MinuteCount;
    public int SecondCount;
    public float MilliCount;

    public MyTime()
    {
        MinuteCount = SecondCount = 0;
        MilliCount = 0;
    }

    public MyTime(String[] initTime)
    {

        MilliCount = float.Parse(initTime[2]);
        SecondCount = int.Parse(initTime[1]);
        MinuteCount = int.Parse(initTime[0]);
        
    }

    public MyTime(int min, int sec, float mil)
    {
        MinuteCount = min;
        SecondCount = sec;
        MilliCount = mil;
    }


    public override string ToString()
    {
        return "" + MinuteCount + ":" + SecondCount + ":" + MilliCount;
    }

    // -1 if this < otherTime, 0 if its equal, 1 otherwise
    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        MyTime otherTime = obj as MyTime;

        if (obj == null)
        {
            throw new ArgumentException("Object is not of type MyTime");
        }
        else
        {
            int minCmp = MinuteCount.CompareTo(otherTime.MinuteCount),
                secCmp;

            if (minCmp == 0)
            {

                secCmp = SecondCount.CompareTo(otherTime.SecondCount);

                if (secCmp == 0)
                    return MilliCount.CompareTo(otherTime.MilliCount);
                else
                    return secCmp;
            }
            else
            {
                return minCmp;
            }
        }
    }
}
