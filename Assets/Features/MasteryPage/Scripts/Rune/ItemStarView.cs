using System;
using TMPro;
using UnityEngine;

public class ItemStarView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtStarNumber;

    public InventoryComposite InventoryComposite;

    public void Setup(InventoryComposite starComposite)
    {
        _txtStarNumber.text = starComposite.StarNumber.ToString("");
    }

    
}
