using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    START, PLAYERTURN, ENEMYTURN, WON, LOST
}
public class StateManager : MonoBehaviour
{
   
    public GameState state;

}
