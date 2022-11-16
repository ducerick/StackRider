using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] Transform _ball;
    [SerializeField] Transform _coin;
    [SerializeField] Transform _wall;

    private float _scaleOfWall;
    private float _scaleOfCoin;
    private float _scaleOfBall;
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
            Transform otherTransform = other.transform.parent;
            GameStackController.Instance.PickUp(otherTransform);
        }
        
        if (other.transform.CompareTag("Wall"))
        {
            Transform otherTranform = other.transform;
            GameStackController.Instance.DropBall(_scaleOfWall, otherTranform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Wall"))
        {
            GameEventController.Instance.ThrowTHeWallMethod();
        }
    }
}
