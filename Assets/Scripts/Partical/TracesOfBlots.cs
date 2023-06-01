using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class TracesOfBlots : MonoBehaviour
{
    [SerializeField] private ParticleSystem _template;
    private ParticleSystem _particle;
    private List<ParticleCollisionEvent> _collisionEvents;

    void Start()
    {
        _particle = GetComponent<ParticleSystem>();
        _collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = _particle.GetCollisionEvents(other, _collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();

        for(int i = 0; i < numCollisionEvents; i++)
        {
            if (rb)
            {
                Vector3 pos = _collisionEvents[i].intersection;
                Instantiate(_template, pos, _template.transform.rotation, null).Play();
            }
        }
    }
}
