using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BaseAttack))]
public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private int _skillsCount = 3;
    
    private BaseAttack _baseAttack;
    private Skill[] _skills;
    private Skill _activeSkill;
    
    public int Count => _skillsCount;

    public event UnityAction<Skill, int> AddedNewSkill;
    public event UnityAction<Skill, int> DeletedSkill;

    private void Awake()
    {
        _skills = new Skill[_skillsCount];
        _baseAttack = GetComponent<BaseAttack>();
        _activeSkill = _baseAttack;
        _activeSkill.enabled = true;
    }

    public void TryAddOrDeleteSkill(Skill skill)
    {
        if (TryDeleteDuplicate(skill) == false)
        {
            for (int i = 0; i < _skills.Length; i++)
            {
                if (_skills[i] == null)
                {
                    _skills[i] = skill;
                    AddedNewSkill.Invoke(_skills[i], i);
                    break;
                }
            }
        }
    }

    public void TrySetActiveSkill(int index)
    {
        if (_skills[index].IsReady)
        {
            if (_skills[index] == _activeSkill)
            {
                ReplaceActiveSkill(_baseAttack);
                return;
            }
            ReplaceActiveSkill(_skills[index]);
        }
    }

    public void TryUseSkill()
    {
        if (_activeSkill != null)
        {
            _activeSkill.Use();
            ReplaceActiveSkill(_baseAttack);
        }
    }

    private bool TryDeleteDuplicate(Skill skill)
    {
        for (int i = 0; i < _skills.Length; i++)
        {
            if (_skills[i] == skill)
            {
                DeletedSkill?.Invoke(_skills[i], i);
                _skills[i] = null;
                return true;
            }
        }
        return false;
    }

    private void ReplaceActiveSkill(Skill skill)
    {
        _activeSkill.enabled = false;
        _activeSkill = skill;
        _activeSkill.enabled = true;
    }
}
