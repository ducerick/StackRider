using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayer : MonoBehaviour
{
    public float speed;
    public FloatingJoystick variableJoystick;
    public Rigidbody rb;

    public void Update()
    {
        if (GameStateController.Instance.GetState() == GameState.Playing)
        {
            Vector3 pos = new Vector3((float)1.75, 0, 0);
            Vector3 direction = pos * variableJoystick.Horizontal;
            transform.position = new Vector3(direction.x, transform.position.y, transform.position.z);
        }
    }
}
