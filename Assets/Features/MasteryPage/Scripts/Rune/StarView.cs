using System;
using TMPro;
using UnityEngine;

public class StarView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtStarNumber;
    [SerializeField] private StarSO _star;

    private void Awake()
    {
        _txtStarNumber.text = _star._starNumber.ToString("");
    }
}
