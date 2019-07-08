using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public enum GameState
    {
        start,
        start_pressed,
        play
    }
    public GameState status;
    public GameStatus()
    {
        status = GameState.start;

    }
}
