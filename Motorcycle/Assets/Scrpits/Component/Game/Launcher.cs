using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : BaseMonoBehaviour
{

    void Start()
    {
        GameBean gameData = new GameBean();
        gameData.SetGameState(GameStateEnum.Init);
        GameHandler.Instance.manager.SetGameData(gameData);
        BuildingHandler.Instance.InitSceneData(1, () =>
        {
            gameData.SetGameState(GameStateEnum.Gaming);
        });
    }

}
