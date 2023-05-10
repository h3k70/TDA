using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    [SerializeField] private int _maxhealth = 20;
    [SerializeField] private GameObject _gem;
    [SerializeField] private ParticleSystem _effectOfDestruction;

    private Renderer _renderer;
    private Color _color = new Color(0, 1, 0, 1);
    private int _health;
    private bool _isAlive = true;

    public UnityAction<int, int> HealthChanged;
    public event UnityAction Dying;

    private void Start()
    {
        _renderer = _gem.GetComponent<Renderer>();
        _renderer.material.color = _color;
        _health = _maxhealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        HealthChanged?.Invoke(_health, _maxhealth);

        if (_color.r < 1)
            _color.r += (1f / (_maxhealth / 2f)) * damage;
        else
            _color.g -= (1f / (_maxhealth / 2f)) * damage;

        _renderer.material.color = _color;

        if (_health <= 0 && _isAlive == true)
        {
            _isAlive = false;
            _effectOfDestruction.Play();
            Dying?.Invoke();
        }
    }
}
