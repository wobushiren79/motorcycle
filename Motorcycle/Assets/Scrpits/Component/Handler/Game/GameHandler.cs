using UnityEditor;
using UnityEngine;

public class GameHandler : BaseHandler<GameHandler, GameManager>
{
    public void EndGame()
    {
        manager.gameData.SetGameState(GameStateEnum.End);
        UIHandler.Instance.manager.OpenUIAndCloseOther<UIGameEnd>(UIEnum.GameEnd);
    }

    public void ResetGame()
    {
        UIHandler.Instance.manager.CloseAllUI();
        PersonHandler.Instance.manager.ClearAllPerson();
        BuildingHandler.Instance.manager.ClearAllBuilding();
        SceneUtil.SceneChange(ScenesEnum.Game);

    }
}