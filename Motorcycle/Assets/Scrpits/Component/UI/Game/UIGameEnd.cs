using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIGameEnd : BaseUIComponent
{
    public Button ui_Reset;

    public override void Awake()
    {
        base.Awake();
        if (ui_Reset != null)
            ui_Reset.onClick.AddListener(OnClickForReset);
    }

    public void OnClickForReset()
    {
        GameHandler.Instance.ResetGame();
    }
}