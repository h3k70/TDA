using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(PlayerSkills))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private GameObject _lefttHand;

    private PlayerSkills _skills;

    public PlayerSkills Skills => _skills;
    public GameObject RightHand => _rightHand;
    public GameObject LeftHand => _lefttHand;

    private void Awake()
    {
        _skills = GetComponent<PlayerSkills>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            _skills.TryUseSkill();
        }
        //------------------------------
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _skills.TrySetActiveSkill(0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _skills.TrySetActiveSkill(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            _skills.TrySetActiveSkill(2);
        }
        //------------------------------
    }
}
