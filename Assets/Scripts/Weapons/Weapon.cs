using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] protected ParticleSystem ParticleSystem;

    public int Damage => _damage;
}
