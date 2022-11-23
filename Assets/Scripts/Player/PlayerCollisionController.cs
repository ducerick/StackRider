using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] Transform _wall;

    private float _scaleOfWall;

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
            GameStackController.Instance.PickUp(otherTransform);
        }

        if (other.transform.CompareTag("Wall"))
        {
            Transform otherTranform = other.transform.parent;
            GameStackController.Instance.DropBall(_scaleOfWall, otherTranform);
        }

        if (other.transform.CompareTag("Coin"))
        {
            other.transform.GetComponent<BoxCollider>().enabled = false;
            other.transform.localPosition = Vector3.Lerp(other.transform.localPosition, new Vector3(other.transform.localPosition.x, other.transform.localPosition.y + 10, other.transform.localPosition.z - 10), 3 * Time.deltaTime);
            Destroy(other.gameObject, 10 * Time.deltaTime);
            GameObject score = CoinPool.Instance.GetPooledObject();
            if (score != null)
            {
                score.SetActive(true);
                score.transform.DOMoveY(score.transform.localPosition.y + 20, 5);
            }
        }

        if (other.transform.CompareTag("End"))
        {
            GameStateController.Instance.SetState(GameState.Success);
            GameEventController.Instance.OnFinishLineMethod();
        }
    }

}
