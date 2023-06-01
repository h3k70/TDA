using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeThrow : AreaSkill
{
    [SerializeField] private SmokeGrenade _grenadeTeamplate;

    private RaycastHit _raycastHit;
    private Transform _hand;

    protected override void Awake()
    {
        _hand = GetComponent<Player>().LeftHand.transform;
        base.Awake();
    }

    public void SpawnSmokeGrenade()
    {
        Instantiate(_grenadeTeamplate, _hand.position, _grenadeTeamplate.transform.rotation, null)
            .Init(_raycastHit.point, RadiusDefeat);
    }

    protected override bool Use(RaycastHit raycastHit)
    {
        _raycastHit = raycastHit;

        transform.LookAt(_raycastHit.point);
        Animator.SetTrigger(HashAnimationNinja.SmokeThrow);

        return true;
    }
}
