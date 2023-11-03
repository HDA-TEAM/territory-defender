using UnityEngine;

[CreateAssetMenu(fileName = "SettingDataAsset", menuName = "ScriptableObject/DataAsset/SettingDataAsset")]
public class SettingDataAsset : ScriptableObject
{
    [SerializeField] private bool _isMusicOn;
    [SerializeField] private bool _isSoundOn;
}
