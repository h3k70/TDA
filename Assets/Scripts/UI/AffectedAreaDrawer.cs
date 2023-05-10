using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectedAreaDrawer : MonoBehaviour
{
    [SerializeField] private Canvas _affectedAreaTeamplate;

    private Canvas _affectedArea;
    private Vector3 _heightAffectedArea = new Vector3(0, 0.1f, 0);

    public void SetSize(float width, float height)
    {
        RectTransform rectTransform = _affectedArea.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(width * 2, height * 2);
    }

    public void Draw()
    {
        _affectedArea = Instantiate(_affectedAreaTeamplate);
    }
    
    public void StopDraw()
    {
        if(_affectedArea != null)
            Destroy(_affectedArea.gameObject);
    }

    public void SetPosition(Vector3 position)
    {
        _affectedArea.transform.position = position + _heightAffectedArea;
    }
}
