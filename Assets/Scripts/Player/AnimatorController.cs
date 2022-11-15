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
        Instance.RunBackWard();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        
    }

    public void Cheer()
    {
        
    }

    public void Dance()
    {
        
    }
}
