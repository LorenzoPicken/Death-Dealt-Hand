using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static event Action onShuffleDeck;

    //Player Deck
    public static event Action onPlayerPickUp;
    public static event Action onPlayerLoseCard;

    //Enemy Deck
    public static event Action onEnemyPickUp;
    public static event Action onEnemyLoseCard;


    public static event Action onRoundEnd;

    #region Game
    public static void InvokeShuffleDeck()
    {
        onShuffleDeck?.Invoke();
    }

    public static void InvokeRoundEnd()
    {
        onRoundEnd?.Invoke();
    }

    #endregion



    #region Player
    public static void InvokePlayerPickup()
    {
        onPlayerPickUp?.Invoke();
    }

    public static void InvokePlayerLoseCard()
    {
        onPlayerLoseCard?.Invoke();
    }

    #endregion






    #region Enemy
    public static void InvokeEnemyPickup()
    {
        onEnemyPickUp?.Invoke();
    }

    public static void InvokeEnemyLoseCard()
    {
        onEnemyLoseCard?.Invoke();
    }

    #endregion
}
