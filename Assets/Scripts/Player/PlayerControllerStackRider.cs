using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerStackRider : MonoBehaviour
{
    [SerializeField] bool _isPlaying;
    [SerializeField] float _moveForwardSpeed;

    private Rigidbody _myRigidBody;
    
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
            GameStateController.Instance.SetState(GameState.Playing);
        }
    }

    private void MoveForward()
    {
        _myRigidBody.velocity = Vector3.forward * _moveForwardSpeed;
    }

}
