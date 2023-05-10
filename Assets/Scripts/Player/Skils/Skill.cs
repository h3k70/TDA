using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private float _calldownTime = 10;
    [SerializeField] private Sprite _icon;

    protected const float ClickDistance = 100;

    private Animator _animator;
    private bool _isReady = true;
    private LayerMask _layerMaskClick;

    public bool IsReady => _isReady;
    public Sprite Icon => _icon;
    protected Animator Animator => _animator;
    protected LayerMask LayerMaskClick => _layerMaskClick;

    public UnityAction Recharged;
    public UnityAction Used;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected void SetLayerMaskClick(LayerMask layerMaskClick)
    {
        _layerMaskClick = layerMaskClick;
    }

    public void Use()
    {
        if (enabled == false)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit RaycastHit;

        if (Physics.Raycast(ray, out RaycastHit, ClickDistance, _layerMaskClick))
        {
            if (Use(RaycastHit))
            {
                _isReady = false;
                this.enabled = false;
                Used?.Invoke();
                StartCoroutine(Calldown());
            };
        }
    }

    protected abstract bool Use(RaycastHit raycastHit);

    private IEnumerator Calldown()
    {
        yield return new WaitForSeconds(_calldownTime);

        _isReady = true;
        Recharged?.Invoke();
    }
}
