
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill", order = 2)]
public class SkillDataSO : ScriptableObject
{
    // public
    public string _skillName;
    public string _skillText;
    public Sprite _skillImage;
}
