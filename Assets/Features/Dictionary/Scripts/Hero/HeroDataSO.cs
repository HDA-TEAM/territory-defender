using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hero", menuName = "Hero", order = 2)]
public class HeroDataSO : ScriptableObject
{
    public Sprite _heroImage;
    
    public SkillsDataAsset _heroSkills;
    public Stats _stats;
}
