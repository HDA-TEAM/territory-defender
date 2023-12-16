using System;
using TMPro;
using UnityEngine;

public class StarView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtStarNumber;
    [SerializeField] private StarSO _star;

    public Action _onDataUpdated;
    private void Awake()
    {
        //_txtStarNumber.text = _star._starNumber.ToString("");
    }

    public void Setup(float starNumber)
    {
        _txtStarNumber.text = starNumber.ToString("");
    }

    public void UpdateStar(float starNumber)
    {
        _star._starNumber -= starNumber;
        Debug.Log("Subtract star");
        
        _onDataUpdated?.Invoke();
    }

    public float GetStarNumber()
    {
        return _star._starNumber;
    }
}
