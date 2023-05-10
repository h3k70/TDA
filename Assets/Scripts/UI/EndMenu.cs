using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private Building _gem;
    [SerializeField] private Image _endMenu;

    private void OnEnable()
    {
        _gem.Dying += OnGemDestroed;
    }

    private void OnDisable()
    {
        _gem.Dying -= OnGemDestroed;
    }

    private void OnGemDestroed()
    {
        _endMenu.gameObject.SetActive(true);
        _endMenu.GetComponent<Animation>().Play();
    }
}
