using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(AffectedAreaDrawer))]
public class ShurikenThrow : Throw
{
    [SerializeField] private Shuriken _shurikenTeamplate;

    private LayerMask _layerMaskTarget;
    private Collider[] _enemies;
    private Transform _hand;

    protected override void Awake()
    {
        _layerMaskTarget = LayerMask.GetMask("Enemy");
        _hand = GetComponent<Player>().RightHand.transform;
        base.Awake();
    }

    public void SpawnShuriken()
    {
        for (int i = 0; i < _enemies.Length; i++)
        {
            Instantiate(_shurikenTeamplate, _hand.position, _shurikenTeamplate.transform.rotation, null).Init(_enemies[i].GetComponent<Enemy>());
        }
    }

    protected override bool Use(RaycastHit raycastHit)
    {
        _enemies = Physics.OverlapSphere(raycastHit.point, RadiusDefeat, _layerMaskTarget);

        if (_enemies.Length > 0)
        {
            transform.LookAt(raycastHit.point);
            Animator.SetTrigger(HashAnimationNinja.DaggerThrow);
            return true;
        }
        return false;
    }
}