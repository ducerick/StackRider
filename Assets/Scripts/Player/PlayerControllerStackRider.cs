using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerStackRider : MonoBehaviour
{
    [SerializeField] float _moveForwardSpeed;
    public static bool _isPlaying;
    private Rigidbody _myRigidBody;
    public float _speedFinish;
    public Transform EndFlatform;

    // Start is called before the first frame update
    void Start()
    {
        _myRigidBody = GetComponent<Rigidbody>();
        _isPlaying = true;
        GameEventController.Instance.OnFinishLine += OnFinishLine;
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

        if (GameStateController.Instance.GetState() == GameState.Success)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3 (0, transform.position.y, EndFlatform.position.z), _speedFinish * Time.deltaTime);
        }

    }

    private void MoveForward()
    {
        _myRigidBody.velocity = Vector3.forward * _moveForwardSpeed;
    }

    private void OnFinishLine()
    {
        _isPlaying = false;
        _myRigidBody.GetComponent<Rigidbody>().velocity = Vector3.zero;
        GameStackController.Instance.PopStack(EndFlatform);
    }
}
