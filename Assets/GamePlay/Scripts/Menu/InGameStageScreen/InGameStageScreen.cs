using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;

public class InGameStageScreen : Page
{
    [SerializeField] private Button _btnSetting;
    [SerializeField] private GameObject _puSetting;

    private void Awake()
    {
        _btnSetting.onClick.AddListener(() => _puSetting.SetActive(true));
    }
}

