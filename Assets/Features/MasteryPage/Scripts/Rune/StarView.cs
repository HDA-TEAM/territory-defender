using System;
using TMPro;
using UnityEngine;

public class StarView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtStarNumber;

    public StarComposite StarComposite;
    public Action _onDataUpdated;

    public void Setup(StarComposite starComposite)
    {
        _txtStarNumber.text = starComposite.StarNumber.ToString("");
        //_onDataUpdated?.Invoke();
    }

    
}
