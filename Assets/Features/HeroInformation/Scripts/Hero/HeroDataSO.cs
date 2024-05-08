using Common.Scripts;
using GamePlay.Scripts.Character.Stats;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroSO", menuName = "ScriptableObject/Data/Hero")]
public class HeroDataSO : ScriptableObject
{
    public UnitId.Hero _heroId;
    public Sprite _imageHero;
    public Sprite _imageHeroChoose;
    public Sprite _imageHeroOwned;
    
    public SkillDataConfig _heroSkill;
    public Stats _stats;
}
