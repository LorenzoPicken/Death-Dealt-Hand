using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoundState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class GAMEMANAGER : MonoBehaviour
{
    public static GAMEMANAGER Instance;
    public static int numOfPlayers = 2;
    public static int currentTurn = 0;

    public RoundState currentRoundState;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        StartGame();
    }
    public int ChangeTurn()
    {
        if(currentTurn ==1) 
        {
            return currentTurn =2;
        }
        else
        {
            return (currentTurn =1);
        }
    }
    public int StartGame()
    {
        return currentTurn = 1;
    }
    
}
