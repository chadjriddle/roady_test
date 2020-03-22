using _GameLogic.Configuration;
using _GameLogic.Configuration.Generated;
using UnityEngine;
using WildbotLabs.Scriptables.GameEvents;
using WildbotLabs.Scriptables.References;

public class InGameManager : MonoBehaviour
{
    public BoolReference PlayerCarActive;
    public GameStateReference CurrentGameState;
    public GameEvent GameOverEvent;

    public bool StopGame;

    void Update()
    {
        if (StopGame && CurrentGameState.Value == GameState.Playing)
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        StopGame = false;
        PlayerCarActive.SetValue(true);
        CurrentGameState.SetValue(GameState.Playing);
    }

    public void GameOver()
    {
        CurrentGameState.SetValue(GameState.GameOver);
        GameOverEvent?.Raise();
        PlayerCarActive.SetValue(false);
    }
}
