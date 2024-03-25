using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static event Action onShuffleDeck;


    public static void InvokeShuffleDeck()
    {
        onShuffleDeck?.Invoke();
    }
}
