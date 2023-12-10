
using UnityEngine;

[CreateAssetMenu(fileName = "Rune", menuName = "Rune", order = 2)]
public class RuneDataSO : ScriptableObject
{
    public string _name;
    public string _additionalValue;
    public string _operate;
    public string _currentStacks;
    public string _stacks;
    public Sprite _avatarSelected;
    public Sprite _avatarStarted;
    
}
