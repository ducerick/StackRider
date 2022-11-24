using UnityEngine;

public enum GameState
{
    Idle,
    Playing,
    Failed,
    Success
}

public class GameStateController : MonoBehaviour
{
    private GameState _gameState = GameState.Idle;

    public static GameStateController Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public GameState GetState()
    {
        return _gameState;
    }

    public void SetState(GameState gameState)
    {
        _gameState = gameState;
    }
}
