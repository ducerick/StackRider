using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            other.gameObject.SetActive(false);
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
           
        }
    }
}
