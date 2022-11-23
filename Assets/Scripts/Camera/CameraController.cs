using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;        //Public variable to store a reference to the player game object
    public Transform EndTranform;
    public float _speedMoveAround;

    private Vector3 _offset;            //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        _offset = transform.position - Player.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (GameStateController.Instance.GetState() == GameState.Playing)
        {
            SetPosition(Player);
        }
    }

    public static CameraController Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);

    }

    private void Update()
    {
        if (GameStateController.Instance.GetState() == GameState.Success)
        {
            transform.RotateAround(EndTranform.position, Vector3.up, Time.deltaTime * _speedMoveAround);
        }
    }

    public void SetPosition(Transform tranform)
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = tranform.position + _offset;
    }
}
