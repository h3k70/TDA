using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public abstract class Skill : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMaskClick;
    [SerializeField] private float _calldownTime = 10;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description;

    protected const float ClickDistance = 100;

    private Animator _animator;
    private bool _isReady = true;

    public bool IsReady => _isReady;
    public Sprite Icon => _icon;
    public string Description => _description;
    protected Animator Animator => _animator;
    protected LayerMask LayerMaskClick => _layerMaskClick;

    public UnityAction Recharged;
    public UnityAction Used;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
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
