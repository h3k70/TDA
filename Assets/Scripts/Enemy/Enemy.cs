using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(EnemyStateMachine))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _body;
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _stunFromDamage = 1f;
    [SerializeField] private ParticleSystem _liqued;
    [SerializeField] private float _destroyDeley = 10f;

    private Animator _animator;
    private Collider _collider;
    private EnemyStateMachine _stateMachine;
    private HashAnimationZombi _hashAnimation = new HashAnimationZombi();
    private int _health;
    private bool _isAlive = true;
    private bool _isStunned = false;
    private Building _target;
    private Coroutine _cooldownAtStunnedJob;

    public GameObject Body => _body;
    public bool IsStunned => _isStunned;
    public Building Target => _target;
    public bool IsAlive => _isAlive;

    public event UnityAction<Enemy> Dying;

    private void Start()
    {
        _health = _maxHealth;
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        _stateMachine = GetComponent<EnemyStateMachine>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Weapon>(out Weapon weapon))
        {
            TakeDamage(weapon.Damage);
        }
    }

    public void Init(Building target)
    {
        _target = target;
        gameObject.transform.LookAt(_target.transform);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        TakeStun(_stunFromDamage);

        if (_health <= 0 )
        {
            _stateMachine.Stop();
            StopCoroutine(_cooldownAtStunnedJob);
            _isAlive = false;
            _collider.enabled = false;
            _animator.SetTrigger(_hashAnimation.Dead);
            Dying?.Invoke(this);
        }
    }

    public void TakeStun(float duration)
    {
        _isStunned = true;

        if (_cooldownAtStunnedJob != null)
            StopCoroutine(_cooldownAtStunnedJob);

        _cooldownAtStunnedJob = StartCoroutine(CooldownAtStunned(duration));
    }

    public void Dissolve()
    {
        _liqued.Play();
        Destroy(gameObject, _destroyDeley);
    }

    private IEnumerator CooldownAtStunned(float duration)
    {
        yield return new WaitForSeconds(duration);
        _isStunned = false;
    }
}
