using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem Particle;

    // Start is called before the first frame update
    void Start()
    {
        Particle.gameObject.SetActive(false);
        GameEventController.Instance.OnExplosion += OnExplosion;
    }

    private void OnExplosion(Color color)
    {
        var main = Particle.main;
        main.startColor = color;
        Particle.gameObject.SetActive(true);
        Particle.Play();
        Debug.Log(Particle);
    }
}
