using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveToTargetState : State
{
    [SerializeField] private float _speed = 1f;

    private void OnEnable()
    {
        _animator.speed = _speed;
        _animator.SetTrigger(_hashAnimation.Walk);
    }

    private void Update()
    {
        if (Target != null)
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
    }
}
