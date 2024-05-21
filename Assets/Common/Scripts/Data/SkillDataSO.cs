using GamePlay.Scripts.Character.Stats;
using UnityEngine;

namespace Common.Scripts.Data
{
    [CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObject/Data/Skill", order = 2)]
    public class SkillDataSO : Stats
    {
        public Sprite _skillImage;
    }
}
