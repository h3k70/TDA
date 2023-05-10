using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkillButton : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private Button _button;
    private int _index;

    public UnityAction<int> Clicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Init(Skill skill, int index)
    {
        _index = index;
        _icon.sprite = skill.Icon;
        skill.Recharged += OnSkillRecharged;
        skill.Used += OnSkillUsed;
    }

    public void Destroy(Skill skill)
    {
        skill.Recharged -= OnSkillRecharged;
        skill.Used -= OnSkillUsed;
        Destroy(gameObject);
    }

    private void OnSkillRecharged()
    {
        _button.interactable = true;
    }
    
    private void OnSkillUsed()
    {
        _button.interactable = false;
    }

    private void OnButtonClick()
    {
        Clicked?.Invoke(_index);
    }
}
