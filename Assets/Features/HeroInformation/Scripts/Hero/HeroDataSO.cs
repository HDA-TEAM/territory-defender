using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Hero", menuName = "Hero", order = 2)]
public class HeroDataSO : ScriptableObject
{
    public Sprite _imageHero;
    public Sprite _imageHeroChoose;
    public Sprite _imageHeroOwned;
    
    public SkillsDataAsset _heroSkills;
    public Stats _stats;
}
