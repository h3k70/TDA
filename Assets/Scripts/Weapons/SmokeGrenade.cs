using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class SmokeGrenade : Weapon
{
    [SerializeField] private float _duration = 5f;

    private float _stunPeriod = 1f;
    private float _flyDuration = 1f;
    private Vector3 _direction;
    private SphereCollider _collider;
    private float _radius;

    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
    }

    public void Init(Vector3 position, float radius)
    {
        var particleDuration = ParticleSystem.main;
        particleDuration.duration = _duration + _flyDuration;
        ParticleSystem.Play();

        _radius = radius;

        StartCoroutine(FlyToPosition(position));
    }

    private float GetDistanceToPosition(Vector3 position)
    {
        _direction = position - transform.position;
        return _direction.magnitude;
    }

    private IEnumerator FlyToPosition(Vector3 position)
    {
        float distance = GetDistanceToPosition(position);

        while (transform.position != _direction)
        {
            _direction = position - transform.position;
            _direction = transform.position + _direction;

            transform.position = Vector3.MoveTowards(transform.position, _direction, (distance / _flyDuration) * Time.deltaTime);

            yield return null;
        }
        StartCoroutine(StunPeriod());

        yield break;
    }

    private IEnumerator StunPeriod()
    {
        float time = 0;

        _collider.radius = _radius;

        while (time <= _duration)
        {
            time += _stunPeriod;

            _collider.enabled = true;
            yield return new WaitForSeconds(_stunPeriod / 2);
            _collider.enabled = false;
            yield return new WaitForSeconds(_stunPeriod / 2);
        }
        ParticleSystem.gameObject.transform.SetParent(null);
        Destroy(gameObject);

        yield break;
    }
}
