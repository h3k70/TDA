using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAddingSkill : MonoBehaviour
{
    [SerializeField] private Skill _skill;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _description;

    private void Awake()
    {
        _icon.sprite = _skill.Icon;
        _description.text = _skill.Description;
    }

    public void SetSkill(PlayerSkills skills)
    {
        skills.TryAddOrDeleteSkill(_skill);
    }
}
