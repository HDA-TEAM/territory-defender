using UnityEngine;

[CreateAssetMenu(fileName = "Rune", menuName = "Rune", order = 2)]
public class RuneDataSO : ScriptableObject
{
    public RuneId _runeId;
    public string _name;
    public string _additionalValue;
    public string _operate;
    public float _currentStacks;
    public float _stacks;
    public float _starNeedToUpgrade;
    public Sprite _avatarSelected;
    public Sprite _avatarStarted;
}
