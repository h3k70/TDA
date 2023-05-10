using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Shuriken : Weapon
{
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private float _flySpeed = 20f;

    private Enemy _target;
    private float _destroyDeley = 0.3f;
    private Vector3 _direction;

    private void Update()
    {
        if (_target == null || _target.IsAlive == false)
            Destroy(gameObject);

        _direction = _target.Body.transform.position - transform.position;
        _direction = transform.position + _direction;

        transform.position = Vector3.MoveTowards(transform.position, _direction, _flySpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            var splash = Instantiate(ParticleSystem, transform.position, ParticleSystem.transform.rotation, null);
            splash.Play();

            _trail.gameObject.transform.SetParent(null);

            Destroy(gameObject, _destroyDeley);
        }
    }

    public void Init(Enemy target)
    {
        _target = target;
    }
}
