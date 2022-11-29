using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventController : MonoBehaviour
{
    public static GameEventController Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public delegate void FinishLine();
    public event FinishLine OnFinishLine;  // Event that Player collision with Finish Line

    public void OnFinishLineMethod()
    {
        if (OnFinishLine != null)
            OnFinishLine();
    }

    public delegate void Explosion(Color color);
    public event Explosion OnExplosion;     // Event explosion ball

    public void OnExplosionMethod(Color color)
    {
        if (OnExplosion != null)
        {
            OnExplosion(color);
        }
    }

    public delegate void LyingLava(Transform transform);
    public event LyingLava OnLyingLava;

    public void OnLyingLavaMethod(Transform transform)
    {
        if (OnLyingLava != null)
        {
            OnLyingLava(transform);
        }
    }

}
