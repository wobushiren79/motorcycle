using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class GameBean 
{
    public GameStateEnum gameState = GameStateEnum.Idle;

    public GameStateEnum GetGameState()
    {
        return gameState;
    }

    public void SetGameState(GameStateEnum gameState)
    {
        this.gameState = gameState;
    }
}