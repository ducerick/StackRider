using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStackController : MonoBehaviour
{
    [SerializeField] Transform _player; //This is a transform of player
    [SerializeField] Transform _initBall; //This is a transform of initialize Ball under the Player
    [SerializeField] Transform _stackPosition; // This is a transform of Stack that contains any balls player pick up
    [SerializeField] Transform _mainPlayer; // The List Stack contains ball
    [SerializeField] float _ballRotationSpeed; // Speed of ratation ball
   
    private float _scaleOfBall; // Scale of ball object
    public static int _checkMainPosition;   // check position of player object follow y axis
    public static int _numberOfBallRemain;  // number of ball remain when attack End Transform

    public Stack<Color> _listColorSuccess = new Stack<Color>();  // List color of stack ball when player successfull
    public List<Transform> _stackBall = new List<Transform>();  // List transform of ball at present

    public int NumberOfBall
    {
        get { return _stackBall.Count; }
        private set { }
    }

    public static GameStackController Instance; // Singleton Pattern

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _scaleOfBall = _initBall.localScale.y;
        _checkMainPosition = -1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameStateController.Instance.GetState())
        {
            case GameState.Playing:  // rotate ball on List Stack at frame time
                RotateBallOnStack();
                _checkMainPosition = -1;
                break;
            case GameState.Success:
                if (Math.Round(_mainPlayer.localPosition.y, 1) == (float)_checkMainPosition + 0.5)  // check position of player equal to _checkMainPosition value
                {
                    PlayerCollisionController.Instance.OnePlusMove(5 * (-_checkMainPosition), 2); // Move "One Plus" text using DOTwen
                    GameScoreController.Instance.SetScore(5 * (-_checkMainPosition) - 1);   // Add value to score
                    GameEventController.Instance.OnExplosionMethod(_listColorSuccess.Pop()); // Start event explosion using Particle System effect that have color is pop of Stack color
                    _checkMainPosition -= 1; // Set check position less than 1 value (follow y axis)
                    Vibrator.Vibrate(50);
                }
                if (_checkMainPosition == -_numberOfBallRemain - 1) // Explosiotn all of remain ball
                {
                    PlayerPrefsController.Instance.AddScore(); // write score to file
                    CanvasStaticController.instance.GamePopupController.gameObject.SetActive(true);
                }
                break;
        }
    }

    /// <summary>
    ///     Rotate all ball of the stack in present.
    /// </summary>
    private void RotateBallOnStack()
    {
        int rotationDirection = 1;
        for (int i = _stackBall.Count - 1; i >= 0; i--)
        {
            _stackBall[i].GetChild(0).Rotate(rotationDirection * _ballRotationSpeed * Time.deltaTime, 0f, 0f);
            rotationDirection *= -1;
        }
    }

    /// <summary>
    ///     Push a ball to List Stack when collider
    /// </summary>
    /// <param name="ball">
    ///     Transform of ball that player pick up
    /// </param>
    public bool PickUp(Transform ball)
    {
        bool isHave = true;
        ball.SetParent(_stackPosition);
        if (!_stackBall.Contains(ball))
        {
            isHave = false;
            _stackBall.Add(ball);
        }
        PushStack();
        return isHave;
    }

    /// <summary>
    ///     Sorting position of all ball and position of Player.
    /// </summary>
    public void PushStack()
    {
        if (GameStateController.Instance.GetState() == GameState.Playing)
        {
            for (int i = 0; i < _stackBall.Count; i++)
            {
                _stackBall[i].localPosition = new Vector3(0f, (_stackBall.Count - i - 1) * _scaleOfBall, 0f);
            }
            _player.localPosition = new Vector3(0f, _initBall.localPosition.y + _scaleOfBall, 0f);
        }
    }

    /// <summary>
    ///     Drop some of ball in stack when collision with the wall
    /// </summary>
    /// <param name="obstacleSize">
    ///     The hight of Wall equal to number ball will drop
    /// </param>
    public void DropBall(float obstacleSize, Transform collision)
    {
        int numberBallDrop = (int)(obstacleSize / _scaleOfBall) * collision.childCount;
        if (numberBallDrop >= NumberOfBall)
        {
            GameFailed(collision);
        }
        else
        {
            int removeIndex = _stackBall.Count - 1;
            for (int i = 0; i < numberBallDrop; i++)
            {
                _stackBall[removeIndex].SetParent(null);
                _stackBall.RemoveAt(removeIndex);
                removeIndex--;
            }
        }
    }

    /// <summary>
    ///     Pop all ball of stack
    /// </summary>
    /// <param name="endtranform">
    ///     Camera follow end transform
    /// </param>
    public void PopStack(Transform endtranform)
    {
        foreach (var ball in _stackBall)
        {
            var color = ball.GetComponentInChildren<Renderer>().material.color;
            _listColorSuccess.Push(color); // push all color of remain stack
        }

        _numberOfBallRemain = _stackBall.Count;

        for (int i = 0; i <= _numberOfBallRemain - 1; i++)
        {
            Destroy(_stackBall[i].gameObject, _numberOfBallRemain - i + Time.deltaTime); // destroy color after time value equal high of ball
        }
        CameraController.Instance.SetPosition(endtranform);
    }

    /// <summary>
    ///     Set some of value game object when player failed
    /// </summary>
    /// <param name="collision">
    ///     A wall object when played failed
    /// </param>
    private void GameFailed(Transform collision)
    {
        GameStateController.Instance.SetState(GameState.Failed); // Set state equal failed
        PlayerControllerStackRider._isPlaying = false;
        PlayerPrefsController.Instance.AddScore(); // write score to file .txt
        CanvasStaticController.instance.GamePopupController.gameObject.SetActive(true);
        GamePopup.Instance.SetText("TRY AGAIN"); // Start Game Popup
        GamePopup.Instance.DeActivateButtonAdv();
        _stackPosition.GetChild(0).gameObject.SetActive(false); // Deactive smoke effect
        for (int i = 0; i < NumberOfBall; i++) // Set remain ball follow parent is collision variable
        {
            _stackBall[i].SetParent(collision);
            _player.SetParent(_stackBall[i]);
            _stackPosition.GetComponent<SphereCollider>().enabled = false;
            CameraController.Instance.Player = _stackBall[i];
        }
    }
}
