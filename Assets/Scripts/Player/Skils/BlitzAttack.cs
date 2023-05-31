using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BlitzAttack : AreaSkill
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private ParticleSystem _slashParticaleTemplate;

    private LayerMask _enemyLayerMask;

    protected override void Awake()
    {
        _enemyLayerMask = LayerMask.GetMask("Enemy");
        base.Awake();
    }

    protected override bool Use(RaycastHit raycastHit)
    {
        Collider[] hits;
        hits = Physics.OverlapSphere(raycastHit.point, RadiusDefeat, _enemyLayerMask);

        transform.LookAt(raycastHit.point);
        Animator.SetTrigger(HashAnimationNinja.BlitzAttack);
        transform.position = raycastHit.point;

        if (hits.Length > 0)
        {
            Enemy target;

            for(int i = 0; i < hits.Length; i++)
            {
                target = hits[i].GetComponent<Enemy>();
                target.TakeDamage(_damage);
                Instantiate(_slashParticaleTemplate, target.Body.transform.position, Quaternion.identity);
            }
        }
        return true;
    }
}
