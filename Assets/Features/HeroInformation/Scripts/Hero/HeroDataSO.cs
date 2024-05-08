using Common.Scripts;
using GamePlay.Scripts.Character.Stats;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Hero", menuName = "Hero", order = 2)]
public class HeroDataSO : ScriptableObject
{
    public UnitId.Hero _heroId;
    public Sprite _imageHero;
    public Sprite _imageHeroChoose;
    public Sprite _imageHeroOwned;
    
    public SkillsDataConfig _heroSkills;
    public Stats _stats;
}
