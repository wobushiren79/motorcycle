using UnityEditor;
using UnityEngine;

public class GameManager : BaseManager
{
    protected Player _player;

    public Player player
    {
        get
        {
            if (_player == null)
            {
                _player = FindWithTag<Player>(TagInfo.Tag_Player);
            }
            return _player;
        }
    }


    public GameBean gameData;


    public GameBean GetGameData()
    {
        return gameData;
    }

    public void SetGameData(GameBean gameData)
    {
        this.gameData = gameData;
    }
}