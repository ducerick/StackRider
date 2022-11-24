using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStackController : MonoBehaviour
{
    /// <summary>
    ///     This is a transform of Player
    /// </summary>
    [SerializeField] Transform _player;
    /// <summary>
    ///     This is a transform of initialize Ball under the Player
    /// </summary>
    [SerializeField] Transform _initBall;
    /// <summary>
    ///     This is a transform of Stack that contains any balls player pick up
    /// </summary>
    [SerializeField] Transform _stackPosition;
    /// <summary>
    ///     The List Stack contains ball
    /// </summary>
    [SerializeField] Transform _mainPlayer;

    private List<Transform> _stackBall = new List<Transform>();

    [SerializeField] float _ballRotationSpeed;
    private float _scaleOfBall;
    public static int _checkMainPosition;
    public static int _numberOfBallRemain;

    public int NumberOfBall
    {
        get { return _stackBall.Count; }
        private set { }
    }

    public static GameStackController Instance;

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
        if (GameStateController.Instance.GetState() == GameState.Playing)
        {
            RotateBallOnStack();
        }

        if (GameStateController.Instance.GetState() == GameState.Success)
        {
           if ((int)_mainPlayer.localPosition.y == _checkMainPosition)
            {
                PlayerCollisionController.Instance.OnePlusMove(5 * (-_checkMainPosition), 2);
                GameScoreController.Instance.SetScore(5 * (-_checkMainPosition) - 1);
                _checkMainPosition -= 1;
            }

           if (_checkMainPosition == -_numberOfBallRemain -1)
            {
                GameScoreController.Instance.WrieFile();
                GamePopup.Instance.SetPopup();
            }
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

    public void PopStack(Transform endtranform)
    {
        _numberOfBallRemain = _stackBall.Count;
        
        for (int i = 0; i <= _numberOfBallRemain - 1; i++)
        {
            Destroy(_stackBall[i].gameObject, _numberOfBallRemain - i + Time.deltaTime);
        }
        CameraController.Instance.SetPosition(endtranform);
    }

    private void GameFailed(Transform collision)
    {
        GameStateController.Instance.SetState(GameState.Failed);
        PlayerControllerStackRider._isPlaying = false;
        GameScoreController.Instance.WrieFile();
        GamePopup.Instance.SetText("TRY AGAIN");
        GamePopup.Instance.SetPopup();
        for (int i = 0; i < NumberOfBall; i++)
        {
            _stackBall[i].SetParent(collision);
            _player.SetParent(_stackBall[i]);
            CameraController.Instance.Player = _stackBall[i];
            _stackPosition.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
