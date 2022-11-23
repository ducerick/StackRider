using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerStackRider : MonoBehaviour
{
    [SerializeField] float _moveForwardSpeed;
    public static bool _isPlaying;
    private Rigidbody _myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _myRigidBody = GetComponent<Rigidbody>();
        _isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateController.Instance.GetState() == GameState.Playing)
        {
            MoveForward();
        }

        if (Input.GetMouseButton(0) && _isPlaying)
        {
            GameStateController.Instance.SetState(GameState.Playing);
        }
    }

    private void MoveForward()
    {
        _myRigidBody.velocity = Vector3.forward * _moveForwardSpeed;
    }
}
