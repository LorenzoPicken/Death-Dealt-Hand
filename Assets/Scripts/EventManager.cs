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


    //Main Deck
    public static event Action onDrawCards;
    public static event Action onReturnCards;


    public static event Action onRoundEnd;
    public static event Action onFinalCardsDistributed;

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




    #region Main Deck

    public static void InvokeDrawCards()
    {
        onDrawCards?.Invoke();
    }

    public static void InvokeReturnCards()
    {
        onReturnCards?.Invoke();
    }

    public static void InvokeOnFinalCardsDistributed()
    {
        onFinalCardsDistributed?.Invoke();
    }




    #endregion
}
