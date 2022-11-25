using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] Transform _wall;

    [SerializeField] AudioClip _coinSound;

    private float _scaleOfWall;

    public static PlayerCollisionController Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _scaleOfWall = _wall.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Ball"))
        {
            Transform otherTransform = other.transform;
            bool isHave = GameStackController.Instance.PickUp(otherTransform);
            Vibrator.Vibrate(2000);
            if (GameStackController.Instance.NumberOfBall > 1 && !isHave)
            {
                OnePlusMove(1, 2);
            }
        }

        if (other.transform.CompareTag("Wall"))
        {
            Transform otherTranform = other.transform.parent;
            GameStackController.Instance.DropBall(_scaleOfWall, otherTranform);
            Vibrator.Vibrate(2000);
        }

        if (other.transform.CompareTag("Coin"))
        {
            AudioSource.PlayClipAtPoint(_coinSound, transform.position);
            Vibrator.Vibrate(1000);
            GameScoreController.Instance.SetScore(1);
            other.transform.GetComponent<BoxCollider>().enabled = false;
            other.transform.localPosition = Vector3.Lerp(other.transform.localPosition, new Vector3(other.transform.localPosition.x, other.transform.localPosition.y + 10, other.transform.localPosition.z - 10), 3 * Time.deltaTime);
            Destroy(other.gameObject, 10 * Time.deltaTime);
        }

        if (other.transform.CompareTag("End"))
        {
            GameStateController.Instance.SetState(GameState.Success);
            GameEventController.Instance.OnFinishLineMethod();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Wall"))
        {
            int walls = other.transform.parent.childCount;
            OnePlusMove(walls, 1);
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
            score.transform.DOLocalMoveY(score.transform.localPosition.y + 100, duration);
            GameScoreController.Instance.SetScore(1);
        }
    }

}
