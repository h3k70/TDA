using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashAnimationZombi
{
    public int Dead = Animator.StringToHash("Dead"); 
    public int Stunned = Animator.StringToHash("GotStunned");
    public int GetAttack = Animator.StringToHash("GetAttack");
    public int Walk = Animator.StringToHash("Walk");
}
