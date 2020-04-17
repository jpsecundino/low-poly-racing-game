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
    public int numberOfLaps;
    public TimerDisplayManager timeManager;
    public LapDisplayManager lapManager;
    public int gameMode;

    public GameObject[] cars;

    public List<Player> playersList;
    
    private Dictionary<int, Player> colliderToPlayer;

    public Transform startingPoint;

    public GameObject wonScreen;
    public GameObject lostScreen;
    
    private void Start()
    {
        registerEvents();
        PutPlayersOnMap();
        LinkCollidersToPlayers();        
        SetDifficulty();

    }

    private void SetDifficulty()
    {
        GameInfo.DifficultySet diffSettings = GameInfo.difficultySettings;
        
        switch (GameInfo.Instance.actualDifficulty)
        {
            case (int) GameInfo.Difficulty.easy:
                SetTimer(diffSettings.easy.totalTime, false);
                SetLaps(diffSettings.easy.numLaps);
                break;
            case (int) GameInfo.Difficulty.medium:
                SetTimer(diffSettings.medium.totalTime, false);
                SetLaps(diffSettings.medium.numLaps);
                break;
            case (int) GameInfo.Difficulty.hard:
                SetTimer(diffSettings.hard.totalTime, false);
                SetLaps(diffSettings.hard.numLaps);
                break;
            default:
                Debug.LogWarning("No difficulty found, setting to default: easy");
                SetTimer(diffSettings.easy.totalTime, false);
                SetLaps(diffSettings.easy.numLaps);
                break;
        }
        
    }

    private void PutPlayersOnMap()
    {
        int _carSelected = GameInfo.Instance.carSelected;

        GameObject _car = Instantiate(cars[_carSelected], startingPoint.transform.position, startingPoint.rotation);
        //fixed player because we only have 1
        playersList[0].car = _car;
        _car.transform.parent = playersList[0].transform;

    }

    private void SetTimer( MyTime t, bool increasing )
    {
        timeManager.increasing = increasing;
        timeManager.SetTime( t );
    }

    private void SetLaps( int _numberLaps )
    {
        numberOfLaps = _numberLaps;
        lapManager.SetTotalLap( _numberLaps );
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

    private void Checkpoint_OnCheckpointEntered( int _checkpointID, Collider _carBodyCollider )
    {
        Player _curPlayer = GetPlayerByCollider( _carBodyCollider );

        //Debug.Log("entrei com o player " + ((_curPlayer == playersList[0]) ? "correto" : "errado"));

        if( _curPlayer == null )
        {
            Debug.LogWarning("The player car collider " + _carBodyCollider.GetHashCode() + " wasn't found.");
            return;
        }

        if( _curPlayer.nextCheckpoint == _checkpointID )
        {
            //if the player passed by the start/finish line
            if (_checkpointID == 0) FinishLinePass(_curPlayer);

            _curPlayer.nextCheckpoint = ( _checkpointID + 1 ) % numberCheckpoints;
        }

    }   

    private void FinishLinePass(Player _player)
    {
  
        if (_player.actualLap == 0)
        {
            _player.actualLap++;
        }
        else
        {
            _player.actualLap++;
            
            if (_player.actualLap > numberOfLaps) PlayerWonRace(_player);

            lapManager.IncreaseActualLap();
            
            //timeManager.RestartTimer();
        }


    }

    private bool calculateBestTime( Player _player )
    {
        if (_player.bestTimeLap.CompareTo(timeManager.curTime) > 0)
        {
            _player.bestTimeLap = timeManager.curTime;
            Debug.Log(_player.bestTimeLap.ToString());
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
