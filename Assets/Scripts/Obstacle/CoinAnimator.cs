using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimator : MonoBehaviour
{
    [SerializeField] List<Animator> ListCoinAnimator;

    public static CoinAnimator Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        ListCoinAnimator = new List<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameStateController.Instance.GetState())
        {
            case GameState.Playing:
                foreach(Animator animator in ListCoinAnimator)
                {
                    RotateCoin(animator);
                }
                break;
            case GameState.Failed:
               
                break;
        }
    }

    void RotateCoin(Animator _animator)
    {
        _animator.SetBool("CoinStatus", true);
    }
}
