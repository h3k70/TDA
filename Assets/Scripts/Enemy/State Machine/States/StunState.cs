using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    private void OnEnable()
    {
        _animator.speed = 0.7f;
        _animator.SetTrigger(_hashAnimation.Stunned);
    }
}
