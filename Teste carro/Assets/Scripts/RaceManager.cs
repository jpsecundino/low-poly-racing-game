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
    public TimerDisplayManager timeManager;
    public LapDisplayManager lapManager;
    public int gameMode;
    public string timeTrial = "01:50:00";

    public List<Player> playersList;
    private Dictionary<int, Player> colliderToPlayer;

    public GameObject wonScreen;
    public GameObject lostScreen;
    
    private void Start()
    {
        registerEvents();
        LinkCollidersToPlayers();
        SetTimer();
        SetLaps();
    }

    private void SetTimer()
    {
        timeManager.increasing = false;
        timeManager.SetTime(new MyTime(timeTrial));
    }

    private void SetLaps()
    {
        lapManager.SetTotalLap(numberLaps);
        lapManager.SetCurLap(1);
    }

    private void registerEvents()
    {
        Checkpoint.OnCheckpointEntered += Checkpoint_OnCheckpointEntered;
        TimerDisplayManager.OnTimerEnd += TimerDisplayManager_OnTimerEnd;
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

    private void TimerDisplayManager_OnTimerEnd()
    {
        PlayerLostRace();
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
            player.actualLap++;
            
            if (player.actualLap > numberLaps) PlayerWonRace(player);
            
            lapManager.IncreaseActualLap();

            if (calculateBestTime(player))
            {
              //  timeManager.updateBestTime(player.bestTimeLap);  this will be activated if another mode get implemented or a for new visual for HUD
            }
            
            timeManager.RestartTimer();
        }


    }

    private bool calculateBestTime(Player player)
    {
        if (player.bestTimeLap.CompareTo(timeManager.curTime) > 0)
        {
            player.bestTimeLap = timeManager.curTime;
            Debug.Log(player.bestTimeLap.ToString());
            return true;

        }

        return false;

    }

    private void PlayerLostRace()
    {
        foreach(Player p in playersList)
        {
            p.car.GetComponent<SimpleCarController>().enabled = false;
        }

        lapManager.DisableDisplay();
        timeManager.DisableDisplay();
        lostScreen.SetActive(true);

    }

    private void PlayerWonRace(Player player) {

        Debug.Log("Jogador venceu na corrida contra o tempo");      

        player.car.GetComponent<SimpleCarController>().enabled = false;

        lapManager.DisableDisplay();
        timeManager.DisableDisplay();
        wonScreen.transform.Find("TimeQuantityText").GetComponent<Text>().text = timeManager.curTime.ToString();
        //wonScreen.transform.FindChild("BestTimeQuantityText").GetComponent<Text>().text = player.bestTimeLap.ToString();
        wonScreen.SetActive(true);
    }
   
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
