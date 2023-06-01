using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(AffectedAreaDrawer))]
public abstract class AreaSkill : Skill
{
    [SerializeField] private float _radiusDefeat = 5f;

    private AffectedAreaDrawer _affectedArea;

    public float RadiusDefeat => _radiusDefeat;

    protected override void Awake()
    {
        _affectedArea = GetComponent<AffectedAreaDrawer>();
        base.Awake();
    }

    private void OnEnable()
    {
        _affectedArea.Draw();
        _affectedArea.SetSize(_radiusDefeat, _radiusDefeat);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, ClickDistance, LayerMaskClick))
        {
            _affectedArea.SetPosition(hit.point);
        }
    }

    private void OnDisable()
    {
        _affectedArea.StopDraw();
    }
}