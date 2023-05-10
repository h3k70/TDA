using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutStunTransition : Transition
{
    private Enemy _self;

    private void Awake()
    {
        _self = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (_self.IsStunned == false)
            NeedTransit = true;
    }
}
