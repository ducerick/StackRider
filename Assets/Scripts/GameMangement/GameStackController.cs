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
    private List<Transform> _stackBall = new List<Transform>();

    [SerializeField] float _ballRotationSpeed;
    private float _scaleOfBall;

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
        _initBall.SetParent(_stackPosition);
        _scaleOfBall = _initBall.localScale.y;
        _stackBall.Add(_initBall);
    }

    private void OnEnable()
    {
        //GameEventController.Instance.OnThrowTheWall += PushStack;
    }

    private void OnDisable()
    {
        //GameEventController.Instance.OnThrowTheWall -= PushStack;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateController.Instance.GetState() == GameState.Playing)
        {
            RotateBallOnStack();
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
    public void PickUp(Transform ball)
    {
        ball.SetParent(_stackPosition);
        _stackBall.Add(ball);
        PushStack();
    }

    /// <summary>
    ///     Sorting position of all ball and position of Player.
    /// </summary>
    public void PushStack()
    {
        for (int i = 0; i < _stackBall.Count; i++)
        {
            //Vector3 oldPosition = _stackBall[i].localPosition;
            //Vector3 newPosition = new Vector3(0f, (_stackBall.Count - i - 1) * _scaleOfBall, 0f);
            //_stackBall[i].localPosition = Vector3.Lerp(oldPosition, newPosition, 50 * Time.deltaTime);
            _stackBall[i].localPosition = new Vector3(0f, (_stackBall.Count - i - 1) * _scaleOfBall, 0f);
        }
        _player.localPosition = new Vector3(0f, _initBall.localPosition.y + _scaleOfBall , 0f);
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
            GameStateController.Instance.SetState(GameState.Failed);
        }
        else
        {
            int removeIndex = _stackBall.Count - 1;
            for (int i = 0; i < numberBallDrop; i++)
            {
                _stackBall[removeIndex].SetParent(null);
                _stackBall[removeIndex].GetComponent<SphereCollider>().enabled = false;
                _stackBall.RemoveAt(removeIndex);
                removeIndex--;
            }
        }
    }
}
