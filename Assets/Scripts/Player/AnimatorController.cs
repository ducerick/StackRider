using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator _animator;

    public static AnimatorController Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameStateController.Instance.GetState())
        {
            case GameState.Playing:
                RunBackWard();
                break;
            case GameState.Failed:
                Lose();
                break;
        }
    }

    public void Idle()
    {
        
    }

    public void RunBackWard()
    {
        _animator.SetInteger("StackRiderStatus", 1);
    }

    public void Lose()
    {
        _animator.SetInteger("StackRiderStatus", 2);
    }

    public void Cheer()
    {
        
    }

    public void Dance()
    {
        
    }
}
