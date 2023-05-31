using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private Collider _collider;
    private TrailRenderer _trail;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _trail = GetComponentInChildren<TrailRenderer>();
    }

    public void MakeAttacking()
    {
        _collider.enabled = true;
        _trail.enabled = true;
    }

    public void MakeNotAttacking()
    {
        _collider.enabled = false;
        _trail.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            ParticleSystem.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            ParticleSystem.Play();
        }
    }
}
