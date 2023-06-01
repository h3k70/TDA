using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _startState;

    private Building _target;
    private State _currentState;

    public State CurrentState => _currentState;

    private void Start()
    {
        _target = GetComponent<Enemy>().Target;
        Reset(_startState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);
    }

    public void Stop()
    {
        if (_currentState != null)
            _currentState.Exit();
    }

    private void Reset(State StartState)
    {
        _currentState = StartState;

        if (_currentState != null)
            _currentState.Enter(_target);
    }

    private void Transit(State nextstate)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextstate;

        if (_currentState != null)
            _currentState.Enter(_target);
    }
}
