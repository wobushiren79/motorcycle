using UnityEditor;
using UnityEngine;
public class PlayerControl : BaseMonoBehaviour
{
    private void Update()
    {
        GameBean gameData = GameHandler.Instance.manager.GetGameData();
        if (gameData.GetGameState() != GameStateEnum.Gaming)
            return;
        HandleForMove();
        HandleForLR();
    }


    /// <summary>
    /// 处理-移动
    /// </summary>
    public void HandleForMove()
    {
        Player player = GameHandler.Instance.manager.player;
        player.transform.Translate(Vector3.forward * Time.deltaTime * 2);
    }

    /// <summary>
    /// 处理-左右移动
    /// </summary>
    public void HandleForLR()
    {
#if UNITY_EDITOR
        float h = Input.GetAxis("Horizontal");
        if (h != 0)
        {
            Player player = GameHandler.Instance.manager.player;
            player.transform.Translate(Vector3.right * Time.deltaTime * h * 10);
        }
#else

#endif
    }
}