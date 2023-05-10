using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb)
            {
                Vector3 pos = _collisionEvents[i].intersection;
                Instantiate(_template, pos, _template.transform.rotation, null).Play();
            }
            i++;
        }
    }
}
