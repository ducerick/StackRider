using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextObject : MonoBehaviour
{
    private float _destinyPosition;
    // Start is called before the first frame update
    void Start()
    {
        _destinyPosition = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y == _destinyPosition)
        {
            gameObject.SetActive(false);
        }
    }

    
}
