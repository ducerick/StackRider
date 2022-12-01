using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] Transform _wall; // wall obstacle
    [SerializeField] AudioClip _coinSound; // coin sound

    public static bool OnLyingLava;

    private float _scaleOfWall;

    public static PlayerCollisionController Instance; // Singleton Pattern

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _scaleOfWall = _wall.localScale.y;
        OnLyingLava = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.transform.tag)
        {
            case "Ball":
                Transform otherTransform = other.transform;
                bool isHave = GameStackController.Instance.PickUp(otherTransform);
                if (GameStackController.Instance.NumberOfBall > 1 && !isHave) 
                {
                    OnePlusMove(1, 2);
                    Vibrator.Vibrate(50);
                }
                break;
            case "Wall":
                Transform otherTranform = other.transform.parent;
                GameStackController.Instance.DropBall(_scaleOfWall, otherTranform);
                Vibrator.Vibrate(100);
                break;
            case "Coin":
                AudioSource.PlayClipAtPoint(_coinSound, transform.position);
                Vibrator.Vibrate(50);
                GameScoreController.Instance.SetScore(1);
                other.transform.GetComponent<BoxCollider>().enabled = false;
                other.transform.localPosition = Vector3.Lerp(other.transform.localPosition, new Vector3(other.transform.localPosition.x, other.transform.localPosition.y + 10, other.transform.localPosition.z - 10), 3 * Time.deltaTime);
                Destroy(other.gameObject, 10 * Time.deltaTime);
                break;
            case "End":
                GameStateController.Instance.SetState(GameState.Success);
                GameEventController.Instance.OnFinishLineMethod();
                break;
            case "Lava":
                OnLyingLava = true;
                GameEventController.Instance.OnLyingLavaMethod(other.transform);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Wall"))
        {
            int walls = other.transform.parent.childCount;
            OnePlusMove(walls, 1);
        }

        if (other.transform.CompareTag("Lava"))
        {
            OnLyingLava = false;
            foreach (var ball in GameStackController.Instance._stackBall)
            {
                ball.GetComponent<Rigidbody>().isKinematic = false;
            }
            GameStackController.Instance.RemoveLastBall();
            GameStackController.Instance.KillBallTween();
        }

    }

    public void OnePlusMove(int i, float duration)
    {
        GameObject score = CoinPool.Instance.GetPooledObject();
        if (score != null)
        {
            score.GetComponent<UnityEngine.UI.Text>().text = "+" + i.ToString();
            score.SetActive(true);
            score.transform.localPosition = new Vector3(score.transform.localPosition.x, 0, score.transform.localPosition.z);
            score.transform.DOLocalMoveY(score.transform.localPosition.y + 200, duration);
            GameScoreController.Instance.SetScore(1);
        }
    }

}
