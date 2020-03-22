using _GameLogic.Configuration;
using _GameLogic.Configuration.Generated;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameStateReference CurrentGameState;

    private GameState _previousState;

    public void ShowSettings()
    {
        _previousState = CurrentGameState.Value;
        CurrentGameState.SetValue(GameState.Settings);
    }

    public void BackToPreviousState()
    {
        CurrentGameState.SetValue(_previousState);
    }
}
