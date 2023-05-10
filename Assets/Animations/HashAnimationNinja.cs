using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashAnimationNinja
{
    // triggers
    public static int SwitchTargetFar = Animator.StringToHash("SwitchTargetFar");
    public static int SwitchTargetNear = Animator.StringToHash("SwitchTargetNear");
    public static int DaggerThrow = Animator.StringToHash("DaggerThrow");
    public static int SmokeThrow = Animator.StringToHash("SmokeThrow");
    public static int TargetDead = Animator.StringToHash("IsTargetDead");

    //Animation Name
    public static int Combo1 = Animator.StringToHash("Combo 1");
    public static int Combo2 = Animator.StringToHash("Combo 2");
    public static int Combo3 = Animator.StringToHash("Combo 3");
    public static int Combo4 = Animator.StringToHash("Combo 4");
    public static int ComboAttack1 = Animator.StringToHash("ComboAttack1");
    public static int ComboAttack2 = Animator.StringToHash("ComboAttack2");
    public static int ComboAttack3 = Animator.StringToHash("ComboAttack3");
    public static int ComboAttack4 = Animator.StringToHash("ComboAttack4");
    public static int Idle = Animator.StringToHash("Idle");
}