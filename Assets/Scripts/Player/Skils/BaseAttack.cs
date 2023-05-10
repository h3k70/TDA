using System.Collections;
using UnityEngine;

public class BaseAttack : Skill
{
    private float _stunDuration = 1f;
    private float _checkRadius = 2f;
    private float _rangeAttack = 2f;
    private float _distanceForJump = 15f;
    private Collider _swordCollider;
    private Enemy _target;
    private float _distance;
    private Vector3 _direction;
    private Coroutine _jumpToTargetJob;
    private Coroutine _attackTargetJob;

    protected override void Awake()
    {
        _swordCollider = GetComponentInChildren<Sword>().GetComponent<Collider>();
        SetLayerMaskClick(LayerMask.GetMask("Enemy"));
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
            _jumpToTargetJob = StartCoroutine(JumpToTarget(0.5f));
        }
        else
        {
            Animator.SetTrigger(HashAnimationNinja.SwitchTargetFar);
            _jumpToTargetJob = StartCoroutine(JumpToTarget(1));
        }
        return true;
    }

    private void StunTarget()
    {
        _target.TakeStun(_stunDuration);
    }

    private IEnumerator JumpToTarget(float duration)
    {
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
        while (_target.IsAlive && _target != null)
        {
            while (_target.IsAlive && _target != null)
            {
                transform.LookAt(_target.transform);

                if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    _swordCollider.enabled = true;
                    int chance = Random.Range(0, 100);
                    if(0 <= chance && chance < 40)
                        Animator.Play(HashAnimationNinja.Combo1);
                    else if (40 <= chance && chance < 80)
                        Animator.Play(HashAnimationNinja.Combo2);
                    else
                        Animator.Play(HashAnimationNinja.Combo3);
                }
                yield return null;
            }
            Animator.SetTrigger(HashAnimationNinja.TargetDead);
            _swordCollider.enabled = false;

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
