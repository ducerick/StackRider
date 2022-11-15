using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Play()
    {
        _gameState = GameState.Playing;
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
