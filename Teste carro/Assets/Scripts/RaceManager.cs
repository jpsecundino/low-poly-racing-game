using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    //it will calculate:
    //calculate the player position
    //if is the last/start checkpoint and it's not the race beginning, calculate player's laptime

    public int numberCheckpoints;
    public int numberLaps;
    public int raceTime;
    public List<Player> playersList;
    private Dictionary<int, Player> colliderToPlayer;


    private void Start()
    {
        Checkpoint.OnCheckpointEntered += Checkpoint_OnCheckpointEntered;

        colliderToPlayer = new Dictionary<int, Player>();
        foreach(Player p in playersList)
        {
            BoxCollider carCollider = p.car.GetComponentInChildren<BoxCollider>();
            colliderToPlayer[carCollider.GetHashCode()] = p;
           
        }
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
            CalculateLapTime(player);
            player.actualLap++;
        }

        if (player.actualLap > numberLaps) FinishRace();

    }

    private void CalculateLapTime(Player player)
    {
        //player.bestTimeLap = 
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
