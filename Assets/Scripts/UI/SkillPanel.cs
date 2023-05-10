using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SkillButton _buttonTemplate;

    private SkillButton[] _buttons;

    private void Start()
    {
        _player.Skills.AddedNewSkill += LinkButton;
        _player.Skills.DeletedSkill += UnLinkButton;
        _buttons = new SkillButton[_player.Skills.Count];
    }

    private void OnDestroy()
    {
        _player.Skills.AddedNewSkill -= LinkButton;
        _player.Skills.DeletedSkill -= UnLinkButton;
    }

    private void LinkButton(Skill skill, int index)
    {
        _buttons[index] = (Instantiate(_buttonTemplate, transform));
        _buttons[index].Init(skill, index);
        _buttons[index].Clicked += OnButtonClick;
    }

    private void UnLinkButton(Skill skill, int index)
    {
        _buttons[index].Clicked -= OnButtonClick;
        _buttons[index].Destroy(skill);
    }

    private void OnButtonClick(int index)
    {
        _player.Skills.TrySetActiveSkill(index);
    }
}
