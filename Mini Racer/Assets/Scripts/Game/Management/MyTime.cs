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

    public MyTime(String s)
    {
        string[] _initTime = s.Split(':'); 
        MilliCount = float.Parse(_initTime[2]);
        SecondCount = int.Parse(_initTime[1]);
        MinuteCount = int.Parse(_initTime[0]);
        
    }

    public MyTime(int min, int sec, float mil)
    {
        MinuteCount = min;
        SecondCount = sec;
        MilliCount = mil;
    }


    public override string ToString()
    {
        return "" + MinuteCount + "\"" + SecondCount + "\'" + (int) MilliCount + ".";
    }

    public bool Equals(MyTime otherTime)
    {
        bool minEq = MinuteCount.Equals(otherTime.MinuteCount);
        bool secEq = SecondCount.Equals(otherTime.MinuteCount);
        bool milliEq = MilliCount.Equals(otherTime.MinuteCount);

        return minEq && secEq && milliEq;
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

    public static bool isZeroed(MyTime time)
    {
        bool milliZeroed = (int) time.MilliCount * 10 <= 0;
        bool secondZeroed = time.SecondCount <= 0;
        bool minZeroed = time.MinuteCount <= 0;

        return milliZeroed && secondZeroed && minZeroed;
    }
}
