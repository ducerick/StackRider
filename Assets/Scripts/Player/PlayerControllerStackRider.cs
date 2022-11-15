using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerStackRider : MonoBehaviour
{
    [SerializeField] bool _isPlaying;
    [SerializeField] float _moveForwardSpeed;
    [SerializeField] float _sideLerpSpeed;

    private Rigidbody _myRigidBody;
    private float _movementBound;

    // Start is called before the first frame update
    void Start()
    {
        _myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlaying && GameStateController.Instance.GetState() == GameState.Playing)
        {
            MoveForward();
        }

        if (Input.GetMouseButton(0))
        {
            if (_isPlaying == false) _isPlaying = true;
            MoveSideWays();
            GameStateController.Instance.SetState(GameState.Playing);
        }
    }

    private void MoveForward()
    {
        _myRigidBody.velocity = Vector3.forward * _moveForwardSpeed;
    }

    private void MoveSideWays()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z), _sideLerpSpeed * Time
                .deltaTime);
        }
    }

}
