using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackState : State
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _delay = 1;

    private float _lastAttacTime;

    private void OnEnable()
    {
        _animator.speed = 1 / _delay;
    }

    private void Update()
    {
        if (_lastAttacTime <= 0)
        {
            Attack(Target);
            _lastAttacTime = _delay;
        }
        _lastAttacTime -= Time.deltaTime;
    }

    public void CauseDamage()
    {
        if (Target != null)
        {
            Target.TakeDamage(_damage);
        }
    }

    private void Attack(Building target)
    {
        _animator.SetTrigger(_hashAnimation.GetAttack);
    }
}
