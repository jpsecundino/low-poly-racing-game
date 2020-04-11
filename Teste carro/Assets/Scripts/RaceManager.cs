using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    //it will calculate:
    //calculate the player position
    //if is the last/start checkpoint and it's not the race beginning, calculate player's laptime

    public int numberCheckpoints;
    public int numberLaps;

    public LapTimeDisplayManager lapTimeManager;
    //public LapTimeDisplayManager againstTheClockManager;
    public LapDisplayManager lapManager;

    public List<Player> playersList;
    private Dictionary<int, Player> colliderToPlayer;
   
    
    private void Start()
    {
        registerEvents();
        LinkCollidersToPlayers();

    }

    private void LinkCollidersToPlayers()
    {
        colliderToPlayer = new Dictionary<int, Player>();
        foreach (Player p in playersList)
        {
            BoxCollider carCollider = p.car.GetComponentInChildren<BoxCollider>();
            colliderToPlayer[carCollider.GetHashCode()] = p;

        }
    }

    private void registerEvents()
    {
        Checkpoint.OnCheckpointEntered += Checkpoint_OnCheckpointEntered;
        LapTimeDisplayManager.OnTimerEnd += LapTimeManager_OnTimerEnd;
        LapTimeDisplayManager.OnTimerStart += LapTimeManager_OnTimerStart;
    }

    private void LapTimeManager_OnTimerEnd(LapTimeDisplayManager ltm)
    {
        FinishRace();
    }

    private void LapTimeManager_OnTimerStart(LapTimeDisplayManager ltm)
    {

    }


    private void Checkpoint_OnCheckpointEntered( int checkpointID, Collider carBodyCollider )
    {
        Player _curPlayer = GetPlayerByCollider( carBodyCollider );

        if( _curPlayer == null )
        {
            Debug.LogWarning("The player car collider " + carBodyCollider.GetHashCode() + " wasn't found.");
            return;
        }

        if( _curPlayer.nextCheckpoint == checkpointID )
        {
            //if the player passed by the start/finish line
            if (checkpointID == 0) FinishLinePass(_curPlayer);

            _curPlayer.nextCheckpoint = ( checkpointID + 1 ) % numberCheckpoints;
        }

    }   

    private void FinishLinePass(Player player)
    {
  
        if (player.actualLap == 0)
        {
            player.actualLap++;
        }
        else
        {
            //CalculateLapTime(player);
            player.actualLap++;
            
            lapManager.IncreaseActualLap();
            
            if(calculateBestTime(player))
                lapTimeManager.updateBestTime(player.bestTimeLap);
            
            lapTimeManager.RestartTimer();
        }

        if (player.actualLap > numberLaps) FinishRace();

    }

    private bool calculateBestTime(Player player)
    {
        if (player.bestTimeLap.CompareTo(lapTimeManager.curTime) > 0)
        {
            player.bestTimeLap = lapTimeManager.curTime;
            return true;
        }

        return false;

    }

    private void FinishRace() { }
   
    private Player GetPlayerByCollider(Collider carBodyCollider)
    {
        int _carObjectHash = carBodyCollider.GetHashCode();

        return colliderToPlayer.ContainsKey(_carObjectHash) 
               ? colliderToPlayer[_carObjectHash]
               : null;
    }


    private void calculatePlayerPosition(Player player)
    {

    }


}
