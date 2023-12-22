using System;
using TMPro;
using UnityEngine;

public class StarView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtStarNumber;

    public InventoryComposite InventoryComposite;
    public Action _onDataUpdated;

    public void Setup(InventoryComposite starComposite)
    {
        _txtStarNumber.text = starComposite.StarNumber.ToString("");
        //_onDataUpdated?.Invoke();
    }

    
}
