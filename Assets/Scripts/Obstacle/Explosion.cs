using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem Particle;
    [SerializeField] Transform EndTransform;

    private List<ParticleSystem> _list = new List<ParticleSystem>();
    private int _amout = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _amout; i++)
        {
            ParticleSystem pts = Instantiate(Particle);
            pts.gameObject.SetActive(false);
            pts.transform.SetParent(EndTransform);
            pts.transform.localPosition = new Vector3(0, 0, 0);
            _list.Add(pts);
        }
        GameEventController.Instance.OnExplosion += OnExplosion;
    }

    private ParticleSystem GetParticleSystem()
    {
        for (int i = 0; i < _list.Count; i++)
        {
            if (!_list[i].gameObject.activeInHierarchy)
            {
                return _list[i];
            }
        }
        return null;
    }

    private void OnExplosion(Color color)
    {
        ParticleSystem pts = GetParticleSystem();
        if (pts != null)
        {
            var main = pts.main;
            main.startColor = color;
            pts.gameObject.SetActive(true);
            pts.Play();
        }
    }
}
