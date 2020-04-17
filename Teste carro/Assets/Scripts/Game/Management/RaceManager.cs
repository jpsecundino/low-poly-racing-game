using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private bool gameIsPaused = false;
    private bool gameplayActive = true;


    
    private Dictionary<int, Player> colliderToPlayer;

    public Transform startingPoint;
    public SimpleCameraController raceCameraController;

    public static event Action OnLost;
    public static event Action OnWon;
    public static event Action OnPause;
    public static event Action OnResume;

    public Player player;
    public GameObject[] cars;
    public GameObject[] checkPoints;
    public GameObject wonScreen;
    public GameObject lostScreen;
    public GameObject pauseScreen;
    public GameObject endScreen;

    private void Awake()
    {
        registerEvents();
    }
    void Start()
    {
        PutPlayersOnMap();
        LinkCollidersToPlayers();        
        SetDifficulty();
        OnResume();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameplayActive)
        {
            if (gameIsPaused)
            {
                gameIsPaused = false;
                OnResume() ;
            }
            else
            {
                gameIsPaused = true;
                OnPause();
            }
        }

 //       if(Input.GetKeyDown(KeyCode.R) && gameplayActive && !gameIsPaused)
   //     {
     //       player.car.GetComponent<Rigidbody>().velocity = -player.car.GetComponent<Rigidbody>().velocity;
       //     player.car.transform.position = checkPoints[player.nextCheckpoint - 1].transform.position;
        //}
    }
    
    private void OnDestroy()
    {
        UnregisterEvents();   
    }

    private void registerEvents()
    {
        Checkpoint.OnCheckpointEntered += Checkpoint_OnCheckpointEntered;
        TimerDisplayManager.OnTimerEnd += PlayerLostRace;
    }

    private void UnregisterEvents()
    {
        Checkpoint.OnCheckpointEntered -= Checkpoint_OnCheckpointEntered;
        TimerDisplayManager.OnTimerEnd -= PlayerLostRace;
    }
    
    private void Checkpoint_OnCheckpointEntered( int _checkpointID, Collider _carBodyCollider )
    {
        Player _curPlayer = GetPlayerByCollider( _carBodyCollider );

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
   
    private Player GetPlayerByCollider(Collider carBodyCollider)
    {
        int _carObjectHash = carBodyCollider.GetHashCode();

        return colliderToPlayer.ContainsKey(_carObjectHash) 
               ? colliderToPlayer[_carObjectHash]
               : null;
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
  
        }


    }
    
    private void PlayerLostRace()
    {
        player.car.GetComponent<SimpleCarController>().enabled = false;
        gameplayActive = false;
        OnLost();
    }
    
    private void PutPlayersOnMap()
    {
        int _carSelected = GameInfo.Instance.carSelected;

        GameObject _car = Instantiate(cars[_carSelected], startingPoint.transform.position, startingPoint.rotation);
        player.car = _car;
        _car.transform.parent = player.transform;
        raceCameraController.objectiveTranf = _car.transform;

    }
    
    private void LinkCollidersToPlayers()
    {
        colliderToPlayer = new Dictionary<int, Player>();
        
        BoxCollider carCollider = player.car.GetComponentInChildren<BoxCollider>();
        colliderToPlayer[carCollider.GetHashCode()] = player;
        
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
                break;
        }
        
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

    private void PlayerWonRace(Player player) 
    {
        player.car.GetComponent<SimpleCarController>().enabled = false;
        gameplayActive = false;
        OnWon();
    }
   
    public void GoMainMenuScene()
    {
        
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void GoChangeCarAndDifficultyScene()
    {
        SceneManager.LoadScene("ChooseCar", LoadSceneMode.Single);
    }

    public void RestartRace()
    {
        Destroy(player.car);
        PutPlayersOnMap();
        LinkCollidersToPlayers();
        SetDifficulty();
        gameplayActive = true;
        OnResume();
    }

}
