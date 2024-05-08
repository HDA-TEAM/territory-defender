using GamePlay.Scripts.Character.Stats;
using System.Collections.Generic;
using UnityEngine;

public class HeroDataManager : SingletonBase<HeroDataManager>
{
    [Header("Data"), Space(12)]
    [SerializeField] private HeroDataConfig _heroDataConfig;
    public List<HeroComposite> HeroComposites { get; private set; }

    protected override void Awake()
    {
        base.Awake(); // Call the base to check singleton integrity
        LoadHeroData();
    }

    private void LoadHeroData()
    {
        // Ensure HeroComposites is initialized
        if (HeroComposites == null)
            HeroComposites = new List<HeroComposite>();
        
        else HeroComposites.Clear();
        
        if (_heroDataConfig == null)
            return;

        List<HeroDataSO> listHeroDataSo = _heroDataConfig.GetAllHeroData();

        foreach (var heroDataSo in listHeroDataSo)
        {
            HeroComposites.Add(new HeroComposite
            {
                HeroId = heroDataSo._heroId,
                Name = heroDataSo._stats.GetInformation(InformationId.Name),
                Level = heroDataSo._stats.GetStat(StatId.Level).ToString(),
                Hp = heroDataSo._stats.GetStat(StatId.MaxHeal).ToString(),
                Atk = heroDataSo._stats.GetStat(StatId.AttackDamage).ToString(),
                Def = heroDataSo._stats.GetStat(StatId.Armour).ToString(),
                Range = heroDataSo._stats.GetStat(StatId.AttackRange).ToString("F2"),
                Avatar = heroDataSo._imageHero,
                HeroChoose = heroDataSo._imageHeroChoose,
                HeroOwned = heroDataSo._imageHeroOwned,
                Skills = heroDataSo._heroSkills.GetAllSkillData()
            });
        }
    }
}

