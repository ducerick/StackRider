using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStackController : MonoBehaviour
{
    Transform _parentPickup;
    [SerializeField] Transform _player;
    [SerializeField] Transform _initBall;

    [SerializeField] Transform _stackPosition;
    private List<Transform> _stackBall = new List<Transform>();

    [SerializeField] float _ballRotationSpeed;
    [SerializeField] float _scaleOfBall;

    public int NumberOfBall
    {
        get { return _stackBall.Count; }
        private set { }
    }

    // Start is called before the first frame update
    void Start()
    {
        _initBall.SetParent(_stackPosition);
        _scaleOfBall = _initBall.localScale.y;
        _stackBall.Add(_initBall);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateController.Instance.GetState() == GameState.Playing)
        {
            RotateBallOnStack();
        }
    }

    void RotateBallOnStack()
    {
        int rotationDirection = 1;
        for (int i = _stackBall.Count - 1; i >= 0; i--)
        {
            _stackBall[i].Rotate(rotationDirection * _ballRotationSpeed * Time.deltaTime, 0f, 0f);
            rotationDirection *= -1;
        }
    }

    void PickUp(Transform ball)
    {
        ball.transform.SetParent(_stackPosition);
        _stackBall.Add(ball);
        PushStack();
    }

    void PushStack()
    {
        for (int i = 0; i < _stackBall.Count; i++)
        {
            _stackBall[i].localPosition = new Vector3(0f, (_stackBall.Count - i - 1) * _scaleOfBall, 0f);
        }
        _player.localPosition = new Vector3(0f, _initBall.localPosition.y + _scaleOfBall / 2, 0f);
    }

    void DropBall(float obstacleSize)
    {
        int numberBallDrop = (int)(obstacleSize / _scaleOfBall);
        if (numberBallDrop > NumberOfBall)
        {
            GameStateController.Instance.SetState(GameState.Failed);
        }
        else
        {

        }
    }
   
}
