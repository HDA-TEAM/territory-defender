using UnityEngine;

[CreateAssetMenu(fileName = "Rune", menuName = "Rune", order = 2)]
public class RuneDataSO : ScriptableObject
{
    [SerializeField]
    private RuneId _runeId;
    
    public string _name;
    public string _additionalValue;
    public string _operate;
    public float _currentStacks;
    public float _stacks;
    public int _starNeedToUpgrade;
    public Sprite _avatarSelected;
    public Sprite _avatarStarted;
    
    public RuneId GetRuneId() => _runeId;
    public float GetCurrentStacks() => _currentStacks;
}
