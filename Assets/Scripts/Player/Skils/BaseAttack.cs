using System.Collections;
using UnityEngine;

public class BaseAttack : Skill
{
    private float _stunDuration = 1f;
    private float _checkRadius = 2f;
    private float _rangeAttack = 2f;
    private float _distanceForJump = 15f;
    private float _durationFarJump = 1;
    private float _durationNearJump = 0.5f;
    private Sword _sword;
    private Enemy _target;
    private float _distance;
    private Vector3 _direction;
    private Coroutine _jumpToTargetJob;
    private Coroutine _attackTargetJob;

    protected override void Awake()
    {
        _sword = GetComponentInChildren<Sword>();
        base.Awake();
    }

    protected override bool Use(RaycastHit raycastHit)
    {
        _target = raycastHit.collider.GetComponent<Enemy>();

        _direction = _target.transform.position - transform.position;
        _distance = _direction.magnitude;

        if (_distance < _distanceForJump)
        {
            Animator.SetTrigger(HashAnimationNinja.SwitchTargetNear);
            _jumpToTargetJob = StartCoroutine(JumpToTarget(_durationNearJump));
        }
        else
        {
            Animator.SetTrigger(HashAnimationNinja.SwitchTargetFar);
            _jumpToTargetJob = StartCoroutine(JumpToTarget(_durationFarJump));
        }
        return true;
    }

    private void StunTarget()
    {
        _target.TakeStun(_stunDuration);
    }

    private bool IsTargetInAttackRadius()
    {
        _direction = _target.transform.position - transform.position;
        _distance = _direction.magnitude;

        return _distance <= _rangeAttack;
    }

    private IEnumerator JumpToTarget(float duration)
    {
        if (_attackTargetJob != null)
            StopCoroutine(_attackTargetJob);

        transform.LookAt(_target.transform);

        while (transform.position != _direction)
        {
            _direction = _target.transform.position - transform.position;
            _direction -= _direction.normalized * _rangeAttack;
            _direction = transform.position + _direction;

            transform.position = Vector3.MoveTowards(transform.position, _direction, (_distance / duration) * Time.deltaTime);

            yield return null;
        }
        StunTarget();
        _attackTargetJob = StartCoroutine(AttackTarget());

        yield break;
    }

    private IEnumerator AttackTarget()
    {
        while (_target.IsAlive && _target != null && IsTargetInAttackRadius())
        {
            while (_target.IsAlive && _target != null && IsTargetInAttackRadius())
            {
                transform.LookAt(_target.transform);

                if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    _sword.MakeAttacking();
                    Animator.Play(HashAnimationNinja.Combo2);
                }
                yield return null;
            }
            Animator.SetTrigger(HashAnimationNinja.TargetDead);
            _sword.MakeNotAttacking();

            Collider[] hits;
            hits = Physics.OverlapSphere(transform.position, _checkRadius, LayerMaskClick);

            if (hits.Length > 0)
            {
                _target = hits[0].GetComponent<Enemy>();
            }
            yield return null;
        }
        yield break;
    }
}
