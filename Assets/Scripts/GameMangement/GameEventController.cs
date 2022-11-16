using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameEventController Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public delegate void ThrowTheWall();
    public event ThrowTheWall OnThrowTheWall;

    public void ThrowTHeWallMethod()
    {
        if (OnThrowTheWall != null)
            OnThrowTheWall();
    }

}
