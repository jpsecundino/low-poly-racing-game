using UnityEngine;
using System.Collections;

public class GameInfo
{
    
    public enum Difficulty
    {
        easy,
        medium,
        hard
    };

    public struct DifficultySet
    {
        public DifficultyParams easy;
        public DifficultyParams medium;
        public DifficultyParams hard;

        public DifficultySet(DifficultyParams _easy, DifficultyParams _medium, DifficultyParams _hard)
        {
            this.easy = _easy;
            this.medium = _medium;
            this.hard = _hard;
        }
    };

    public struct DifficultyParams
    {
        public MyTime totalTime;
        public int numLaps;

        public DifficultyParams(MyTime totalTime, int numLaps)
        {
            this.totalTime = totalTime;
            this.numLaps = numLaps;
        }
    }

    public int carSelected = 1;
    public int actualDifficulty = 0;    
    
    public static DifficultySet difficultySettings;

    private static GameInfo instance;

    public GameInfo()
    {
        SetDifficultyParams();
    }

    private static void SetDifficultyParams()
    {
        //change here the difficulty's parameters
        MyTime easyTime = new MyTime(3, 20, 0);
        int easyLaps = 3;

        MyTime medTime = new MyTime(2, 40, 0);
        int medLaps = 3;
        
        MyTime hardTime = new MyTime(2, 10, 0);
        int hardLaps = 3;

        DifficultyParams easyParams = new DifficultyParams(easyTime, easyLaps);
        DifficultyParams medParams = new DifficultyParams(medTime, medLaps);
        DifficultyParams hardParams = new DifficultyParams(hardTime, hardLaps);

        difficultySettings = new DifficultySet(easyParams, medParams, hardParams);
    }

    public static GameInfo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameInfo();
            }

            return instance;
        }
    }

}